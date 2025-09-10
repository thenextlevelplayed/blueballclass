using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;
using System.Text.Encodings.Web; 

namespace diagnosisSystem;

public class PatientDbFacade
{
    private readonly PatientDb _patientDb;
    private readonly Prescriber _prescriber;
    private readonly DiagnosisRequestProcessor _processor;

    public PatientDbFacade(string file,string diseaseTxt)
    {
        _patientDb = new PatientDb();
        ParseJsonToDb(file);
        _prescriber = new Prescriber(_patientDb);
        _processor = new DiagnosisRequestProcessor(_prescriber);
        var diseaseDiagnosis = new DiseaseDiagnosis(_prescriber);
        diseaseDiagnosis.TxtParser(diseaseTxt);
        diseaseDiagnosis.LoadDiseasesInfo();
    }

    private void ParseJsonToDb(string file)
    {
        try
        {
            var jsonString = File.ReadAllText(file);
            var options = new JsonSerializerOptions
            {
                // 讓屬性名稱不區分大小寫 (例如 "id" 可以對應到 "Id")
                // 雖然我們已經修改了JSON，但這是一個很好的習慣
                PropertyNameCaseInsensitive = true
            };

            // 2. **加入關鍵的 Enum 轉換器**
            // 這會告訴序列化器將 JSON 中的字串轉換為 Enum
            options.Converters.Add(new JsonStringEnumConverter());

            // --- 修改結束 ---

            // 3. 在反序列化時傳入設定好的選項
            var patients = JsonSerializer.Deserialize<List<Patient>>(jsonString, options);

            if (patients != null)
            {
                foreach (var patient in patients)
                {
                    _patientDb.AddPatient(patient);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("讀取檔案時發生錯誤：" + ex.Message);
        }

        Console.WriteLine(_patientDb);
    }

    public async Task JsonExporter(Case? newCase, string outputPath)
    {
        Console.WriteLine($"準備將 Case 匯出為 JSON 至: {outputPath}");

        // 設定序列化選項
        var options = new JsonSerializerOptions
        {
            // 讓輸出的 JSON 格式化，方便閱讀
            WriteIndented = true,
            // 將 Enum 成員轉換為字串 (例如 Symptom.Cough -> "Cough")
            Converters = { new JsonStringEnumConverter() },
            // 如果您希望屬性名稱是駝峰式 (camelCase)，可以取消註解下一行
            // PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
            Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
        };

        try
        {
            // 1. 將物件序列化為 JSON 字串
            string jsonString = JsonSerializer.Serialize(newCase, options);

            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                // 2. 使用非同步方式將字串寫入檔案
                await File.WriteAllTextAsync(outputPath, jsonString);

                Console.WriteLine("JSON 檔案匯出成功！");
            }
            else
            {
                Console.WriteLine("JSON 為空，匯出失敗！");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"匯出 JSON 時發生錯誤: {ex.Message}");
        }
    }

    public async Task CsvExporter(Case? newCase, string outputPath)
    {
        Console.WriteLine($"準備將 Cases 匯出為 CSV 至: {outputPath}");

        try
        {
            // 使用非同步方式寫入，並設定 StreamWriter
            await using var writer = new StreamWriter(outputPath);
            // 建立 CsvWriter 實例
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            // 1. 寫入表頭
            csv.WriteField("CaseTime");
            csv.WriteField("Symptoms");
            csv.WriteField("PrescriptionName");
            csv.WriteField("PotentialDisease");
            csv.WriteField("Medicines");
            csv.WriteField("Usage");
            await csv.NextRecordAsync(); // 換行

            // 2. 遍歷所有 case 並寫入資料
            if (newCase == null)
            {
                Console.WriteLine("JSON 為空，匯出失敗！");
                return;
            }

            csv.WriteField(newCase.CaseTime.ToString("yyyy-MM-dd HH:mm:ss"));

            // 將 Symptoms 列表合併為一個字串，用分號分隔
            var symptomsString = string.Join(";", newCase.Symptoms);
            csv.WriteField(symptomsString);

            // 寫入 Prescription 的屬性
            csv.WriteField(newCase.Prescription.Name);
            csv.WriteField(newCase.Prescription.PotentialDisease);

            // 將 Medicines 列表合併為一個字串
            var medicinesString = string.Join(";", newCase.Prescription.Medicines.Select(m => m.Name));
            csv.WriteField(medicinesString);
            csv.WriteField(newCase.Prescription.Usage);
            await csv.NextRecordAsync(); // 換行
            await writer.FlushAsync();
            Console.WriteLine("CSV 檔案匯出成功！");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"匯出 CSV 時發生錯誤: {ex.Message}");
        }
    }

    /// <summary>
    /// 提供一個方法來優雅地關閉背景處理
    /// </summary>
    public async Task ShutdownAsync()
    {
        await _processor.StopAsync();
    }

    /// <summary>
    /// 公開的 API，用來提交一個新的診斷請求，並非同步等待結果。
    /// </summary>
    public async Task<Case?> RequestDiagnosisAsync(string patientId, List<Symptom> symptoms)
    {
        // 1. 建立包含「兌換券」的請求物件
        var request = new DiagnosisRequest(patientId, symptoms);

        // 2. 將請求放入佇列
        _processor.EnqueueRequest(request);

        // 3. 非同步等待「兌換券」被兌換，並回傳結果
        //    這就是魔法發生的地方！
        Case? resultCase = await request.Tcs.Task;

        return resultCase;
    }
}