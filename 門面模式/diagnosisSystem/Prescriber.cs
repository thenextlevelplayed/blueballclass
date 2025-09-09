namespace diagnosisSystem;

public class Prescriber
{
    private PatientDb PatientDb { get; }
    public DiseaseDiagnosis DiseaseDiagnosis { get; set; }

    public List<IDiagnosisStrategy> DiagnosisStrategy { get; set; } = new List<IDiagnosisStrategy>();


    public Prescriber(PatientDb patientDb)
    {
        PatientDb = patientDb;
    }

    public Case? Demand(string id, List<Symptom> symptoms)
    {
        var patient = PatientDb.QueryPatientById(id);
        
        foreach (var strategy in DiagnosisStrategy)
        {
            if (strategy.CanDiagnose(patient, symptoms))
            {
                var prescription = strategy.Prescription;
                var newCase = new Case(symptoms, prescription, DateTime.Now);
                PatientDb.AddCaseToPatient(patient, newCase);
                return newCase;
            }
        }
        return null;
    }
}