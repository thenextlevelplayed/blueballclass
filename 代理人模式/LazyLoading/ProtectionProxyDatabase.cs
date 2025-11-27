namespace LazyLoading;

public sealed class ProtectionProxyDatabase : Database
{
    private readonly Database _realDatabase;

    public ProtectionProxyDatabase(Database realDatabase)
    {
        _realDatabase = realDatabase;
    }

    private Boolean CheckPWD()
    {
        string pathValue = Environment.GetEnvironmentVariable("PASSWORD");
        if (!String.IsNullOrWhiteSpace(pathValue))
        {
            return pathValue == "1qaz2wsx";
        }

        return false;
    }

    public Employee? GetEmployeeById(int id)
    {
        if (!CheckPWD())
        {
            Console.WriteLine("密碼錯誤，無法存取資料庫");
            return null;
        }

        return _realDatabase.GetEmployeeById(id);
    }
}