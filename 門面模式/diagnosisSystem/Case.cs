namespace diagnosisSystem;

public class Case
{
    public List<Symptom> Symptoms { get; set; }
    public Prescriber Prescriber { get; set; }
    public int CaseTime { get; set; }

    public Case(List<Symptom> symptoms, Prescriber prescriber, int caseTime)
    {
        Symptoms = symptoms;
        Prescriber = prescriber;
        CaseTime = caseTime;
    }
}