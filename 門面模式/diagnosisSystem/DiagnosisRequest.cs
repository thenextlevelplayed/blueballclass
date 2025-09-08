namespace diagnosisSystem;

public class DiagnosisRequest
{
    public string PatientId { get; }
    public List<Symptom> Symptoms { get; }

    public DiagnosisRequest(string patientId, List<Symptom> symptoms)
    {
        PatientId = patientId;
        Symptoms = symptoms;
    }
}