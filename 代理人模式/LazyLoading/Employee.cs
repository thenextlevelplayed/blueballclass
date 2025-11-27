namespace LazyLoading;

public interface Employee
{
    int Id { get; set; }
    string Name { get; set; }
    int Age { get; set; }
    List<string> SubordinateId { get; set; }
    List<Employee> GetSubordinates();
}