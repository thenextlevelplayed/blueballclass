// See https://aka.ms/new-console-template for more information

using FriendshipAnalyzer;
using System;
using System.IO;
using FriendshipAnalyzer.基礎;

string scriptContent = "";
string filePath = "D:\\新增資料夾\\blueballclass\\轉接器模式\\FriendshipAnalyzer\\script.txt"; // 檔案路徑

try
{
    // 2. 這一行就是關鍵：讀取檔案所有文字
    scriptContent = File.ReadAllText(filePath);
}
catch (FileNotFoundException)
{
    Console.WriteLine($"錯誤：找不到檔案 '{filePath}'");
    return; // 找不到檔案，後續無法執行
}
catch (Exception ex)
{
    Console.WriteLine($"讀取檔案時發生錯誤: {ex.Message}");
    return;
}

var analyzer = new AnalyzerAdapter();
var relationshipGraph = analyzer.Parse(scriptContent);
relationshipGraph.HasConnection("A", "B");
var mutalFriends = analyzer.GetMutualFriends("A", "B");
Console.WriteLine(mutalFriends);
