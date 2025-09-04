namespace diagnosisSystem;

public interface IDiagnosisStrategy
{
    Prescription Diagnose(Patient patient, List<Symptom> symptoms);
}

public class CovidStrategy : IDiagnosisStrategy
{
    public Prescription Diagnose(Patient patient, List<Symptom> symptoms)
    {
        var medicines = new List<Medicine>();
        medicines.Add(new Medicine("清冠一號"));
        return new Prescription("清冠一號", "新冠肺炎（專業學名：COVID-19）", medicines,
            "將相關藥材裝入茶包裡，使用500 mL 溫、熱水沖泡悶煮1~3 分鐘後即可飲用。");
    }
}

public class AttractiveStrategy : IDiagnosisStrategy
{
    public Prescription Diagnose(Patient patient, List<Symptom> symptoms)
    {
        throw new NotImplementedException();
    }
}

public class SleepApneaStrategy : IDiagnosisStrategy
{
    public Prescription Diagnose(Patient patient, List<Symptom> symptoms)
    {
        throw new NotImplementedException();
    }
}