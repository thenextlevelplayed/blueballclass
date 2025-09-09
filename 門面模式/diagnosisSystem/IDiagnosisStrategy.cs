namespace diagnosisSystem;

public interface IDiagnosisStrategy
{
    Prescription Prescription { get; }
    bool CanDiagnose(Patient? patient, List<Symptom> symptoms);
}

public class CovidStrategy : IDiagnosisStrategy
{
    public Prescription Prescription { get; }

    public CovidStrategy()
    {
        Prescription = new Prescription("清冠一號", "新冠肺炎（專業學名：COVID-19）",
            new List<Medicine> { new Medicine("清冠一號") },
            "將相關藥材裝入茶包裡，使用500 mL 溫、熱水沖泡悶煮1~3 分鐘後即可飲用。");
    }

    public bool CanDiagnose(Patient? patient, List<Symptom> symptoms)
    {
        return symptoms.Contains(Symptom.Headache) && 
               symptoms.Contains(Symptom.Cough) && 
               symptoms.Contains(Symptom.Sneeze);
    }
}

public class AttractiveStrategy : IDiagnosisStrategy
{
    public Prescription Prescription { get; }

    public AttractiveStrategy()
    {
        Prescription = new Prescription("青春抑制劑", "有人想你了 (專業學名：Attractive)",
            new List<Medicine> { new Medicine("假鬢角"), new Medicine("臭味道") },
            "把假鬢角黏在臉的兩側，讓自己異性緣差一點，自然就不會有人想妳了。");
    }

    public bool CanDiagnose(Patient? patient, List<Symptom> symptoms)
    {
        return patient?.Age == 18 && 
               patient.Gender == "female" && 
               symptoms.Contains(Symptom.Sneeze);
    }
}

public class SleepApneaStrategy : IDiagnosisStrategy
{
    public Prescription Prescription { get; }

    public SleepApneaStrategy()
    {
        Prescription = new Prescription("打呼抑制劑", "睡眠呼吸中止症（專業學名：SleepApneaSyndrome）",
            new List<Medicine> { new Medicine("一捲膠帶") },
            "睡覺時，撕下兩塊膠帶，將兩塊膠帶交錯黏在關閉的嘴巴上，就不會打呼了。");
    }

    public bool CanDiagnose(Patient? patient, List<Symptom> symptoms)
    {
        // BMI = 體重(kg) / (身高(m))^2
        var heightInMeters = patient?.Height / 100.0;
        var bmi = patient?.Weight / (heightInMeters * heightInMeters);
        
        return bmi > 26 && symptoms.Contains(Symptom.Snore);
    }
}