namespace diagnosisSystem;

public class PatientDb
{
    private List<Patient> Patients { set; get; }

    public PatientDb(List<Patient> patients)
    {
        Patients = patients;
    }
    
    public Patient QueryPatientById(string id)
    {
        return Patients.FirstOrDefault(p => p.Id == id);
    }
    
    public void AddCaseToPatient(Patient patient, Case newCase)
    {
        patient.PatientCases.Add(newCase);
    }
}