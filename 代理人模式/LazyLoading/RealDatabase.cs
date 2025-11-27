namespace LazyLoading;

public class RealDatabase : Database
{
    private static readonly string File = "D:\\新增資料夾\\blueballclass\\代理人模式\\LazyLoading\\mock_employee_data.txt";
    // private List<Employee> _employees;

    public Employee? GetEmployeeById(int id)
    {
        var emp = System.IO.File.ReadLines(File).Skip(id).First();
        if (String.IsNullOrWhiteSpace(emp))
        {
            return null;
        }

        var parts = emp.Split(" ");
        var name = parts[1];
        var age = int.Parse(parts[2]);
        var subordinateIds = !String.IsNullOrWhiteSpace(parts[3]) ? parts[3].Split(",").ToList() : new List<string>();
        return new VirtualProxyRealEmployee(this)
        {
            Id = id,
            Name = name,
            Age = age,
            SubordinateId = subordinateIds
        };
    }
}