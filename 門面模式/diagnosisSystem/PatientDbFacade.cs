using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace diagnosisSystem;

public class PatientDbFacade
{
    private PatientDb _patientDb;
    private Prescriber _prescriber;
    private readonly DiagnosisRequestProcessor _processor;

    public PatientDbFacade(string file)
    {
        _patientDb = new PatientDb();
        ParseJsonToDb(file);
        _prescriber = new Prescriber(_patientDb);
        _processor = new DiagnosisRequestProcessor(_prescriber);
    }

    public void ParseJsonToDb(string file)
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

    public object JsonExporter(string outputPath)
    {
        throw new NotImplementedException();
    }

    public object CsvExporter(string outputPath)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// 公開的 API，用來提交一個新的診斷請求
    /// </summary>
    public void RequestDiagnosis(string patientId, List<Symptom> symptoms)
    {
        var request = new DiagnosisRequest(patientId, symptoms);
        _processor.EnqueueRequest(request);
    }

    /// <summary>
    /// 提供一個方法來優雅地關閉背景處理
    /// </summary>
    public async Task ShutdownAsync()
    {
        await _processor.StopAsync();
    }
}