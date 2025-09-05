namespace diagnosisSystem;

public abstract class DiagnosisHandler
{
    protected DiagnosisHandler? Next { get; set; }

    public DiagnosisHandler(DiagnosisHandler? next)
    {
        Next = next;
    }

    public abstract void Diagnose(Patient patient, List<Symptom> symptoms, PatientDb patientDb);

    public void TemplateDiagnose(Patient patient, List<Symptom> symptoms, PatientDb patientDb)
    {
        if (CanHandle(patient, symptoms))
        {
            var currentCase = CaseHandle(patient, symptoms, patientDb);
            patientDb.AddCaseToPatient(patient, currentCase);
        }
        else if (Next != null)
        {
            Next.Diagnose(patient, symptoms, patientDb);
        }
    }

    protected abstract bool CanHandle(Patient patient, List<Symptom> symptoms);


    protected abstract Case CaseHandle(Patient patient, List<Symptom> symptoms, PatientDb patientDb);
}

public class CovidHandler(DiagnosisHandler? next) : DiagnosisHandler(next)
{
    public override void Diagnose(Patient patient, List<Symptom> symptoms, PatientDb patientDb)
    {
        if (symptoms.Contains(Symptom.Headache) && symptoms.Contains(Symptom.Cough))
        {
            var medicines = new List<Medicine>();
            medicines.Add(new Medicine("清冠一號"));
            var currentCase = new Case(symptoms, new Prescription("清冠一號", "新冠肺炎（專業學名：COVID-19）", medicines,
                "將相關藥材裝入茶包裡，使用500 mL 溫、熱水沖泡悶煮1~3 分鐘後即可飲用。"), 1);
            patientDb.AddCaseToPatient(patient, currentCase);
        }
        else if (Next != null)
        {
            Next.Diagnose(patient, symptoms, patientDb);
        }
    }

    protected override bool CanHandle(Patient patient, List<Symptom> symptoms)
    {
        return symptoms.Contains(Symptom.Headache) && symptoms.Contains(Symptom.Cough);
    }

    protected override Case CaseHandle(Patient patient, List<Symptom> symptoms, PatientDb patientDb)
    {
        var medicines = new List<Medicine>();
        medicines.Add(new Medicine("清冠一號"));
        return new Case(symptoms, new Prescription("清冠一號", "新冠肺炎（專業學名：COVID-19）", medicines,
            "將相關藥材裝入茶包裡，使用500 mL 溫、熱水沖泡悶煮1~3 分鐘後即可飲用。"), 1);
    }
}

public class AttractiveHandler(DiagnosisHandler? next) : DiagnosisHandler(next)
{
    public override void Diagnose(Patient patient, List<Symptom> symptoms, PatientDb patientDb)
    {
        if (symptoms.Contains(Symptom.Sneeze) && patient.Age == 18)
        {
            var medicines = new List<Medicine>();
            medicines.Add(new Medicine("假鬢角"));
            medicines.Add(new Medicine("臭味"));
            var currentCase = new Case(symptoms, new Prescription("青春抑制劑", "有人想你了 (專業學名：Attractive)", medicines,
                "把假鬢角黏在臉的兩側，讓自己異性緣差一點，自然就不會有人想妳了。"), 1);
            patientDb.AddCaseToPatient(patient, currentCase);
        }
        else if (Next != null)
        {
            Next.Diagnose(patient, symptoms, patientDb);
        }
    }

    protected override bool CanHandle(Patient patient, List<Symptom> symptoms)
    {
        return symptoms.Contains(Symptom.Sneeze) && patient.Age == 18;
    }

    protected override Case CaseHandle(Patient patient, List<Symptom> symptoms, PatientDb patientDb)
    {
        var medicines = new List<Medicine>();
        medicines.Add(new Medicine("假鬢角"));
        medicines.Add(new Medicine("臭味"));
        return new Case(symptoms, new Prescription("青春抑制劑", "有人想你了 (專業學名：Attractive)", medicines,
            "把假鬢角黏在臉的兩側，讓自己異性緣差一點，自然就不會有人想妳了。"), 1);
    }
}

public class SleepApneaHandler(DiagnosisHandler? next) : DiagnosisHandler(next)
{
    public override void Diagnose(Patient patient, List<Symptom> symptoms, PatientDb patientDb)
    {
        if (symptoms.Contains(Symptom.Snore) && patient.Weight / (patient.Height * patient.Height) > 26)
        {
            var medicines = new List<Medicine>();
            medicines.Add(new Medicine("一捲膠帶"));
            var currentCase = new Case(symptoms, new Prescription("打呼抑制劑", "睡眠呼吸中止症（專業學名：SleepApneaSyndrome）",
                medicines,
                "睡覺時，撕下兩塊膠帶，將兩塊膠帶交錯黏在關閉的嘴巴上，就不會打呼了。"), 1);
            patientDb.AddCaseToPatient(patient, currentCase);
        }
        else if (Next != null)
        {
            Next.Diagnose(patient, symptoms, patientDb);
        }
    }

    protected override bool CanHandle(Patient patient, List<Symptom> symptoms)
    {
        return symptoms.Contains(Symptom.Snore) && patient.Weight / (patient.Height * patient.Height) > 26;
    }

    protected override Case CaseHandle(Patient patient, List<Symptom> symptoms, PatientDb patientDb)
    {
        var medicines = new List<Medicine>();
        medicines.Add(new Medicine("一捲膠帶"));
        return new Case(symptoms, new Prescription("打呼抑制劑", "睡眠呼吸中止症（專業學名：SleepApneaSyndrome）",
            medicines,
            "睡覺時，撕下兩塊膠帶，將兩塊膠帶交錯黏在關閉的嘴巴上，就不會打呼了。"), 1);
    }
}