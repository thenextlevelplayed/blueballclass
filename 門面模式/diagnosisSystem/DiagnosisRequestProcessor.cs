// DiagnosisRequestProcessor.cs
using System.Collections.Concurrent;

namespace diagnosisSystem;

public class DiagnosisRequestProcessor : IDisposable
{
    private readonly Prescriber _prescriber;
    private readonly ConcurrentQueue<DiagnosisRequest> _requestQueue = new();
    
    // *** 關鍵點：將併發數設為 1，確保一次只處理一個請求 ***
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    
    private readonly CancellationTokenSource _cts = new();
    private readonly Task _consumerTask;

    public DiagnosisRequestProcessor(Prescriber prescriber)
    {
        _prescriber = prescriber;
        // 在建構式中啟動唯一的消費者任務，讓它在背景執行
        _consumerTask = Task.Run(() => ConsumeQueueAsync(_cts.Token));
    }

    /// <summary>
    /// 將一個新的診斷請求加入佇列
    /// </summary>
    public void EnqueueRequest(DiagnosisRequest request)
    {
        _requestQueue.Enqueue(request);
        Console.WriteLine($"[佇列] 新的診斷請求已加入。病人ID: {request.PatientId}。目前佇列大小: {_requestQueue.Count}");
    }

    /// <summary>
    /// 消費者邏輯：不斷從佇列中取出請求並處理
    /// </summary>
    private async Task ConsumeQueueAsync(CancellationToken token)
    {
        Console.WriteLine("[處理器] 背景消費者任務已啟動，等待請求...");

        while (!token.IsCancellationRequested)
        {
            // 如果佇列為空，非同步等待一小段時間，避免空轉消耗CPU
            if (_requestQueue.IsEmpty)
            {
                await Task.Delay(100, token);
                continue;
            }

            try
            {
                

                if (_requestQueue.TryDequeue(out var request))
                {
                    try
                    {
                        // 等待取得唯一的一個處理名額
                        await _semaphore.WaitAsync(token);
                        
                        Console.WriteLine($"--> [處理器] 開始診斷病人: {request.PatientId}。(佇列剩餘: {_requestQueue.Count})");
                        
                        // *** 執行真正的診斷工作 ***
                        // 這裡不再是 await，因為 Prescriber.Demand 是同步方法
                        // 但它是在一個非同步的 Task 中被執行的，所以不會阻塞主執行緒
                        var newCase = _prescriber.Demand(request.PatientId, request.Symptoms);

                        // 模擬診斷需要一些時間
                        await Task.Delay(1500, token); 

                        Console.WriteLine($"<-- [處理器] 完成診斷病人: {request.PatientId}。");
                        request.Tcs.SetResult(newCase);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"處理病人 {request.PatientId} 時發生錯誤: {ex.Message}");
                        // 根據需求決定是否要將 request 放回佇列
                    }
                    finally
                    {
                        // 確保處理完畢後，釋放名額，讓下一個請求可以進來
                        _semaphore.Release();
                        Console.WriteLine("[處理器] 處理名額已釋放。");
                    }
                }
                else
                {
                    // 雖然前面檢查過 IsEmpty，但多執行緒下仍可能 dequeue 失敗
                    // 如果失敗，也要釋放剛剛取得的信號量
                     _semaphore.Release();
                }
            }
            catch (OperationCanceledException)
            {
                // 收到取消訊號，正常結束迴圈
                break;
            }
        }
        Console.WriteLine("[處理器] 背景消費者任務已停止。");
    }

    /// <summary>
    /// 優雅地停止處理器
    /// </summary>
    public async Task StopAsync()
    {
        Console.WriteLine("[處理器] 正在停止...");
        await _cts.CancelAsync();
        await _consumerTask; // 等待背景任務完全結束
    }

    public void Dispose()
    {
        StopAsync().Wait();
        _cts.Dispose();
        _semaphore.Dispose();
    }
}