namespace LazyLoading;

public class VirtualProxyRealEmployee : RealEmployee
{
    private Database _database;
    private List<Employee>? _subordinatesCache = null;

    public VirtualProxyRealEmployee(Database database)
    {
        _database = database;
    }

    public override List<Employee> GetSubordinates()
    {
        if (_subordinatesCache != null)
        {
            return _subordinatesCache;
        }
        Console.WriteLine("正在讀取硬碟資料...");
        List<Employee> empList = new List<Employee>();
        foreach (var id in this.SubordinateId)
        {
            var emp = _database.GetEmployeeById(int.Parse(id));
            if (emp != null)
            {
                empList.Add(emp);
            }
        }

        _subordinatesCache = empList;
        return empList;
    }
}