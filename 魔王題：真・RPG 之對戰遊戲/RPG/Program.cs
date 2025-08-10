// ===================================================================
// 步驟 2.1: 強制設定 Console 的輸出編碼為 UTF-8，這是關鍵！
// ===================================================================

using System.Text;
using RPG;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// --- 您的原始碼開頭 (讀取測資部分) ---
string inputPath = "curse.in";
if (!File.Exists(inputPath))
{
    Console.WriteLine($"找不到測資檔案：{inputPath}");
    return;
}

var army1Roles = new List<Role>();
var army2Roles = new List<Role>();
int currentArmyFlag = 0;
string[] lines = File.ReadAllLines(inputPath);

foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line)) continue;
    if (line.Trim() == "#軍隊-1-開始")
    {
        currentArmyFlag = 1;
        continue;
    }

    if (line.Trim() == "#軍隊-2-開始")
    {
        currentArmyFlag = 2;
        continue;
    }

    if (line.Trim().Contains("-結束"))
    {
        currentArmyFlag = 0;
        continue;
    }

    if (currentArmyFlag > 0)
    {
        string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 4) continue;
        string name = parts[0];
        int hp = int.Parse(parts[1]);
        int mp = int.Parse(parts[2]);
        int str = int.Parse(parts[3]);
        string[] skillNames = parts.Skip(4).ToArray();
        Role newRole = name == "英雄"
            ? new Hero(name, hp, mp, str, skillNames)
            : new AI(name, hp, mp, str, skillNames);
        if (currentArmyFlag == 1) army1Roles.Add(newRole);
        else if (currentArmyFlag == 2) army2Roles.Add(newRole);
    }
}

var troop1 = new Troop(army1Roles);
var troop2 = new Troop(army2Roles);
Troop.SetRelation(troop1, troop2);
string[] extraInputs = lines
    .SkipWhile(l => !l.Trim().Contains("#軍隊-2-結束"))
    .Skip(1)
    .TakeWhile(l => l.Trim() != "###")
    .ToArray();
var rpg = new RPG.Rpg(troop1, troop2, extraInputs);

// --- 以下是新增的測試邏輯 ---
Console.WriteLine("--- 準備進行輸出比對測試 ---");

TextWriter originalConsoleOut = Console.Out;
using var stringWriter = new StringWriter();
Console.SetOut(stringWriter); // 將輸出捕獲到 stringWriter

try
{
    rpg.Battle();
}
catch (Exception ex)
{
    Console.SetOut(originalConsoleOut);
    Console.WriteLine("測試過程中發生未預期的錯誤：" + ex.Message);
    Console.WriteLine(ex.StackTrace);
    return;
}
finally
{
    Console.SetOut(originalConsoleOut); // 確保恢復 Console 輸出
}

string actualOutput = stringWriter.ToString().Trim();
string expectedOutputPath = "curse.out";

if (!File.Exists(expectedOutputPath))
{
    Console.WriteLine($"找不到預期的輸出檔案：{expectedOutputPath}");
    return;
}

// 讀取預期輸出檔案時，也明確指定 UTF-8
string expectedOutput = File.ReadAllText(expectedOutputPath, Encoding.UTF8).Trim();

actualOutput = actualOutput.Replace("\r\n", "\n");
expectedOutput = expectedOutput.Replace("\r\n", "\n");

Console.WriteLine("\n--- 比對結果 ---");
if (actualOutput == expectedOutput)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("測試通過！ 您的程式輸出與 cheerup.out 完全相符！");
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("測試失敗！ 您的程式輸出與 cheerup.out 不符。");
    Console.ResetColor();

    // 將有問題的輸出寫入檔案，以便除錯
    // 這裡使用 File.WriteAllText，它預設就是 UTF-8，最穩定
    File.WriteAllText("my_actual_output.txt", actualOutput);
    File.WriteAllText("expected_output.txt", expectedOutput);
    Console.WriteLine("\n詳細的差異已寫入 my_actual_output.txt 和 expected_output.txt 以便比對。");
    Console.WriteLine("請使用 VS Code 的檔案比對功能來查看這兩個檔案的差異。");
}