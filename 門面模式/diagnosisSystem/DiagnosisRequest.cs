namespace diagnosisSystem;

public class DiagnosisRequest
{
    public string PatientId { get; }
    public List<Symptom> Symptoms { get; }
    
    public TaskCompletionSource<Case?> Tcs { get; set; }

    public DiagnosisRequest(string patientId, List<Symptom> symptoms)
    {
        PatientId = patientId;
        Symptoms = symptoms;
        Tcs = new TaskCompletionSource<Case?>();// 建立兌換券
    }
}