namespace diagnosisSystem;

public class PrescriptionSystemFacade
{
    public async Task RunCompleteDiagnosis(string file, string diseaseTxt, string patientId, List<Symptom> symptoms,
        string outputPath, string outputFormat)
    {
        var patientDbFacade = new PatientDbFacade(file,diseaseTxt);
        Case? finishedCase = await patientDbFacade.RequestDiagnosisAsync(patientId, symptoms);
        if (outputFormat == "json")
        {
            await patientDbFacade.JsonExporter(finishedCase,outputPath);
        }
        else if (outputFormat == "csv")
        {
            await patientDbFacade.CsvExporter(finishedCase,outputPath);
        }
    }
}