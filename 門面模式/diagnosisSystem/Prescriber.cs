namespace diagnosisSystem;

public class Prescriber
{
    private PatientDb PatientDb { get; }
    private Case CurrentCase { get; set; }
    private Prescription CurrentPrescription { get; set; }
    public DiseaseDiagnosis DiseaseDiagnosis { get; set; }
    
    public List<IDiagnosisStrategy> DiagnosisStrategy { get; set; }
    

    public Prescriber(PatientDb patientDb, Case newCase, Prescription prescription)
    {
        PatientDb = patientDb;
        CurrentCase = newCase;
        CurrentPrescription = prescription;
    }

    public void Demand(string id, List<Symptom> symptoms)
    {
        var patient = PatientDb.QueryPatientById(id);
        // var medicines = new List<Medicine>();
        // if (symptoms.Contains(Symptom.Headache) && symptoms.Contains(Symptom.Cough))
        // {
        //     medicines.Add(new Medicine("清冠一號"));
        //     CurrentPrescription = new Prescription("清冠一號", "新冠肺炎（專業學名：COVID-19）", medicines,
        //         "將相關藥材裝入茶包裡，使用500 mL 溫、熱水沖泡悶煮1~3 分鐘後即可飲用。");
        //     CurrentCase = new Case(symptoms, CurrentPrescription, 1);
        //     PatientDb.AddCaseToPatient(patient, CurrentCase);
        // }
        // else if (symptoms.Contains(Symptom.Sneeze) && patient.Age == 18)
        // {
        //     medicines.Add(new Medicine("假鬢角"));
        //     medicines.Add(new Medicine("臭味"));
        //     CurrentPrescription = new Prescription("青春抑制劑", "有人想你了 (專業學名：Attractive)", medicines,
        //         "把假鬢角黏在臉的兩側，讓自己異性緣差一點，自然就不會有人想妳了。");
        //     CurrentCase = new Case(symptoms, CurrentPrescription, 1);
        //     PatientDb.AddCaseToPatient(patient, CurrentCase);
        // }
        // else if (symptoms.Contains(Symptom.Snore) && patient.Weight / (patient.Height * patient.Height) > 26)
        // {
        //     medicines.Add(new Medicine("一捲膠帶"));
        //     CurrentPrescription = new Prescription("打呼抑制劑", "睡眠呼吸中止症（專業學名：SleepApneaSyndrome）", medicines,
        //         "睡覺時，撕下兩塊膠帶，將兩塊膠帶交錯黏在關閉的嘴巴上，就不會打呼了。");
        //     CurrentCase = new Case(symptoms, CurrentPrescription, 1);
        //     PatientDb.AddCaseToPatient(patient, CurrentCase);
        // }
        // else
        // {
        //     // CurrentPrescription = new Prescription("無法診斷", "無法診斷", new List<Medicine>(), "無法診斷");
        // }
        foreach (var strategy in DiagnosisStrategy)
        {
            if (symptoms.Contains(Symptom.Headache) && symptoms.Contains(Symptom.Cough))
            {
                CurrentPrescription = strategy.CurrentPrescription;
            }
            strategy.CurrentPrescription = CurrentPrescription;
        }
        new CovidHandler(
            new AttractiveHandler(
                new SleepApneaHandler(null)
            )
        ).TemplateDiagnose(patient, symptoms, PatientDb);
    }
}