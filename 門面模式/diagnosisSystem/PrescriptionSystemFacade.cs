namespace diagnosisSystem;

public class PrescriptionSystemFacade
{
    public void RunCompleteDiagnosis(string file, string diseaseTxt, string patientId, List<Symptom> symptoms,
        string outputPath, string outputFormat)
    {
        var patientDb = new PatientDb();
        var prescriber = new Prescriber(patientDb);
        var diseaseDiagnosis = new DiseaseDiagnosis(prescriber);
        diseaseDiagnosis.TxtParser(diseaseTxt);
        diseaseDiagnosis.LoadDiseasesInfo();
        prescriber.Demand(patientId, symptoms);
        var patientDbFacade = new PatientDbFacade(file);
        patientDbFacade.RequestDiagnosis(patientId, symptoms);
        var 
        if (outputFormat == "json")
        {
            var jsonExporter = patientDbFacade.JsonExporter(outputPath);
            jsonExporter.Export(patientDb, patientId);
        }
        else if (outputFormat == "csv")
        {
            var csvExporter = patientDbFacade.CsvExporter(outputPath);
            csvExporter.Export(patientDb, patientId);
        }
    }
}