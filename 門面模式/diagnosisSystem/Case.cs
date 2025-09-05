namespace diagnosisSystem;

public class Case
{
    public List<Symptom> Symptoms { get; set; }
    public Prescription Prescription { get; set; }
    public int CaseTime { get; set; }

    public Case(List<Symptom> symptoms, Prescription prescription, int caseTime)
    {
        Symptoms = symptoms;
        Prescription = prescription;
        CaseTime = caseTime;
    }
}