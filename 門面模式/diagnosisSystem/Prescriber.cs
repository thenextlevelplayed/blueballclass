namespace diagnosisSystem;

public class Prescriber
{
    private PatientDb PatientDb{get;}
    private Case CurrentCase { get; set; }
    private Prescription CurrentPrescription { get; set; }
    private List<IDiagnosisStrategy> DiagnosisStrategies;

    public Prescriber(PatientDb patientDb, Case newCase, Prescription prescription)
    {
        PatientDb = patientDb;
        CurrentCase = newCase;
        CurrentPrescription = prescription;
    }
    
    private Patient GetPatient(string id)
    {
        return PatientDb.QueryPatientById(id);
    }

    public Prescriber Demand(string id, List<Symptom> symptoms)
    {
        var patient = GetPatient(id);
        foreach (var strategy in DiagnosisStrategies)
        {
            if (symptoms.Contains(Symptom.Headache) && symptoms.Contains(Symptom.Cough))
            {
                var prescription = strategy.Diagnose(patient, symptoms);
                if (prescription!=null)
                {
                    CurrentPrescription = prescription;
                    break;
                }
            }
            else
            {
                CurrentPrescription = new Prescription("無法診斷", "無法診斷", new List<Medicine>(), "無法診斷");
            }
            
        }
       
    }
    
    public void AddDiagnosisStrategy(IDiagnosisStrategy strategy)
    {
        DiagnosisStrategies.Add(strategy);
    }
}