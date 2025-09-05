namespace diagnosisSystem;

public interface IDiagnosisStrategy
{
    Prescription CurrentPrescription { get; set; }
}

public class SleepApneaStrategy : IDiagnosisStrategy
{
    public Prescription CurrentPrescription { get; set; }

    public SleepApneaStrategy()
    {
        CurrentPrescription = new Prescription("清冠一號", "新冠肺炎（專業學名：COVID-19）",
            new List<Medicine> { new Medicine("清冠一號") },
            "將相關藥材裝入茶包裡，使用500 mL 溫、熱水沖泡悶煮1~3 分鐘後即可飲用。");
    }
}

public class AttractiveStrategy : IDiagnosisStrategy
{
    public Prescription CurrentPrescription { get; set; }

    public AttractiveStrategy()
    {
        CurrentPrescription = new Prescription("青春抑制劑", "有人想你了 (專業學名：Attractive)",
            new List<Medicine> { new Medicine("假鬢角"), new Medicine("臭味") },
            "將相關藥材裝入茶包裡，使用500 mL 溫、熱水沖泡悶煮1~3 分鐘後即可飲用。");
    }
}

public class CovidStrategy : IDiagnosisStrategy
{
    public Prescription CurrentPrescription { get; set; }

    public CovidStrategy()
    {
        CurrentPrescription = new Prescription("打呼抑制劑", "睡眠呼吸中止症（專業學名：SleepApneaSyndrome）",
            new List<Medicine> { new Medicine("一捲膠帶") },
            "睡覺時，撕下兩塊膠帶，將兩塊膠帶交錯黏在關閉的嘴巴上，就不會打呼了。");
    }
}