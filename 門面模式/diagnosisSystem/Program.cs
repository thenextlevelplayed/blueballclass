// See https://aka.ms/new-console-template for more information
using diagnosisSystem;
var systemFacade = new PrescriptionSystemFacade();
await systemFacade.RunCompleteDiagnosis(
    file: @"D:\\新增資料夾\\blueballclass\\門面模式\\diagnosisSystem\\mock.json",
    diseaseTxt: @"D:\\新增資料夾\\blueballclass\\門面模式\\diagnosisSystem\\Disease.txt",
    patientId: "A123456789",
    symptoms: new List<Symptom> { Symptom.Snore },
    outputPath: @"D:\\新增資料夾\\blueballclass\\門面模式\\diagnosisSystem\\export.csv",
    outputFormat: "csv"
);
