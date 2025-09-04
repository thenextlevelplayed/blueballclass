using System.Text.RegularExpressions;

namespace diagnosisSystem;

public class Prescription
{
    public string Name { get; set; }
    public string PotentialDisease { get; set; }
    public List<Medicine> Medicines { get; set; }
    public string Usage { get; set; }

    public Prescription(string name, string potentialDisease, List<Medicine> medicines, string usage)
    {
        Name = Regex.IsMatch(name, @".{4,30}$")
            ? name
            : throw new ArgumentException("Invalid name, only 4-30 characters allowed");
        PotentialDisease = Regex.IsMatch(potentialDisease, @".{3,100}$")
            ? potentialDisease
            : throw new ArgumentException("Invalid disease name, only 3-100 characters allowed");
        Medicines = medicines;
        Usage = Regex.IsMatch(usage, @".{0,1000}$")
            ? usage
            : throw new ArgumentException("Invalid usage, only 0-1000 characters allowed");
    }
}