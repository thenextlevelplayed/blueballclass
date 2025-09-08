using System.Text.Json.Serialization;

namespace diagnosisSystem;

public class Case
{
    public List<Symptom> Symptoms { get; set; }
    public Prescription Prescription { get; set; }
    public DateTime CaseTime { get; set; }

    [JsonConstructor]
    public Case(List<Symptom> symptoms, Prescription prescription, DateTime caseTime)
    {
        Symptoms = symptoms;
        Prescription = prescription;
        CaseTime = caseTime;
    }
}