using System.Text.RegularExpressions;

namespace diagnosisSystem;

public class Patient
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public List<Case> PatientCases { get; set; }

    private Patient(string id, string name, string gender, int age, float height, float weight, List<Case> patientCases)
    {
        Id = Regex.IsMatch(id, @"^[A-Z]{1}[1-2]{1}[0-9]{8}$") ? id : throw new ArgumentException("Invalid id");
        Name = Regex.IsMatch(name, @"[A-Za-z]{1,30}$")
            ? name
            : throw new ArgumentException("Invalid name, only letters allowed");
        Gender = gender is "male" || gender is "female"
            ? gender
            : throw new ArgumentException("Invalid Gender, only 0 or 1 allowed");
        Age = age >= 1 && age <= 180 ? age : throw new ArgumentException("Invalid age, only 1-120 allowed");
        Height = height is >= 1 and <= 500
            ? height
            : throw new ArgumentException("Invalid height, only 1-500 allowed");
        Weight = weight is >= 1 and <= 500
            ? weight
            : throw new ArgumentException("Invalid weight, only 1-500 allowed");
        PatientCases = patientCases;
    }
}