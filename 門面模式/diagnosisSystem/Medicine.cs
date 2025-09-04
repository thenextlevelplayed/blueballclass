using System.Text.RegularExpressions;

namespace diagnosisSystem;

public class Medicine
{
    public String Name { get; set; }

    public Medicine(string name)
    {
        Name = Regex.IsMatch(name, @".{3,30}&")
            ? name
            : throw new ArgumentException("Invalid medicine name, only 3-30 characters allowed");
    }
}