using System.Text;

namespace LazyLoading;

public interface Database
{
    Employee? GetEmployeeById(int id);
}

// public class RealDatabase : Database
// {
//     private static readonly string _file = "mock_employee_data.txt";
//     private static string _indexPath = "mock_employee_data_id_to_offset_index.txt";
//     private List<Employee> _employees;
// 17672176838
//     public RealDatabase()
//     {
//         Init();
//     }
//
//     public Employee? GetEmployeeById(int id)
//     {
//         var index = LoadIndex_IdToOffset_Tsv(_indexPath);
//
//         if (!index.TryGetValue(id+1.ToString(), out long offset))
//         {
//             return null; // 找不到此 ID
//         }
//
//         using var fs = new FileStream(_file, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1 << 20);
//         fs.Seek(offset, SeekOrigin.Begin);
//         using var sr = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1 << 20);
//
//         string? line = sr.ReadLine();
//         if (line == null)
//         {
//             return null;
//         }
//
//
//         var cols = line.Split(',');
//
//         return new RealEmployee
//         {
//             Id = int.Parse(cols[0]),
//             Name = cols.Length > 1 ? cols[1] : "",
//             Age = int.Parse(cols[2]),
//             SubordinateId = new List<int>()
//         };
//     }
//
//
//     private static void BuildIndex_IdToOffset_Tsv(string dataPath, string indexPath)
//     {
//         using var fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1 << 20);
//         using var sr = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1 << 20);
//         using var iw = new StreamWriter(indexPath, append: false, Encoding.UTF8, bufferSize: 1 << 20);
//
//         string? line;
//         while (true)
//         {
//             long offsetAtLineStart = fs.Position; // 行開頭的位移（最準）
//             line = sr.ReadLine();
//             if (line == null) break;
//
//             // TODO: 依你的檔案格式解析 ID（這裡假設逗號分隔、ID 在第 1 欄）
//             string idText = line.Split(',', 2)[0];
//
//             // 索引一行：id \t offset
//             iw.Write(idText);
//             iw.Write('\t');
//             iw.Write(offsetAtLineStart);
//             iw.WriteLine();
//         }
//     }
//
//
//     private static Dictionary<string, long> LoadIndex_IdToOffset_Tsv(string indexPath)
//     {
//         var map = new Dictionary<string, long>(StringComparer.Ordinal);
//         foreach (var line in File.ReadLines(indexPath))
//         {
//             var parts = line.Split('\t');
//             if (parts.Length != 2) continue;
//             var id = parts[0];
//             var offset = long.Parse(parts[1]);
//             // 若有重複 ID：這裡保留最後一筆，可改成第一次優先或丟例外
//             map[id] = offset;
//         }
//
//         return map;
//     }
//
//
//     private void Init()
//     {
//         BuildIndex_IdToOffset_Tsv(_file, _indexPath);
//     }
// }