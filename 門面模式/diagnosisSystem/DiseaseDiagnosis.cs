namespace diagnosisSystem;

public class DiseaseDiagnosis
{
    public List<string> Diseases { get; set; } = new List<string>();
    
    public Prescriber Prescriber { get; set; }
    
    public DiseaseDiagnosis(Prescriber prescriber)
    {
        Prescriber = prescriber;
        Prescriber.DiseaseDiagnosis = this;
    }
    public void TxtParser(string file)
    {
        try
        {
            foreach (string line in File.ReadLines(file))
            {
                Console.WriteLine(line); // 或其他處理方式
                Diseases.Add(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("讀取檔案時發生錯誤：" + ex.Message);
        }
    }

    public void LoadDiseasesInfo()
    {
        foreach (var disease in Diseases)
        {
            if(disease == "COVID-19")
            {
                Prescriber.DiagnosisStrategy.Add(new CovidStrategy());
            }
            else if(disease == "Attractive")
            {
                Prescriber.DiagnosisStrategy.Add(new AttractiveStrategy());
            }
            else if(disease == "SleepApneaSyndrome")
            {
                Prescriber.DiagnosisStrategy.Add(new SleepApneaStrategy());
            }
        }
    }
}