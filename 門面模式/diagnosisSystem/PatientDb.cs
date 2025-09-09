namespace diagnosisSystem;

public class PatientDb
{
    private List<Patient> Patients { set; get; } = new List<Patient>();

    public PatientDb()
    {
    }

    public Patient? QueryPatientById(string id)
    {
        return Patients.FirstOrDefault(p => p.Id == id);
    }

    public void AddCaseToPatient(Patient patient, Case newCase)
    {
        patient.PatientCases.Add(newCase);
    }

    public void AddPatient(Patient patient)
    {
        Patients.Add(patient);
    }
}