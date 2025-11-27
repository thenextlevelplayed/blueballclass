// See https://aka.ms/new-console-template for more information

using LazyLoading;

Environment.SetEnvironmentVariable("PASSWORD", "1qaz2wsx");

var db = new RealDatabase();
var protectDb = new ProtectionProxyDatabase(db);

Console.WriteLine("--- 取得員工 ID=1 ---");
Employee? emp = protectDb.GetEmployeeById(1);

Console.WriteLine("\n--- 第一次呼叫 GetSubordinates ---");
var emps1 = emp?.GetSubordinates();
emps1?.ForEach(e => Console.WriteLine($"  下屬: {e.Name}"));

Console.WriteLine("\n--- 第二次呼叫 GetSubordinates (測試快取) ---");
var emps2 = emp?.GetSubordinates();
emps2?.ForEach(e => Console.WriteLine($"  下屬: {e.Name}"));

Console.WriteLine("--- 取得員工 ID=3---");
Employee? emp1 = protectDb.GetEmployeeById(3);

Console.WriteLine("\n--- 第一次呼叫 GetSubordinates ---");
var emps3 = emp1?.GetSubordinates();
emps3?.ForEach(e => Console.WriteLine($"  下屬: {e.Name}"));

Console.WriteLine("\n--- 第二次呼叫 GetSubordinates (測試快取) ---");
var emps4 = emp1?.GetSubordinates();
emps4?.ForEach(e => Console.WriteLine($"  下屬: {e.Name}"));