namespace LazyLoading;

public class RealEmployee : Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> SubordinateId { get; set; }

    public virtual List<Employee> GetSubordinates() => new List<Employee>();
}