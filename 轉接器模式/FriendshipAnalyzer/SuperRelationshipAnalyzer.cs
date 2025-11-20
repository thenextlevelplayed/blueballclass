using System;
using System.Collections.Generic;

/// <summary>
/// 這是「小華」定義的介面，對應 OOA 圖 C 中的 SuperRelationshipAnalyzer 介面。
/// </summary>
public interface ISuperRelationshipAnalyzer
{
    /// <summary>
    /// 初始化分析器，讀取 "A -- B" 格式的腳本。
    /// </summary>
    /// <param name="script">"A -- B" 格式的字串腳本</param>
    void Init(string script);

    /// <summary>
    /// 檢查 targetName 是否同時是 name1 和 name2 的好友。
    /// </summary>
    /// <param name="targetName">要檢查的目標</param>
    /// <param name="name1">第一個人</param>
    /// <param name="name2">第二個人</param>
    /// <returns>如果是共同好友，回傳 true；否則回傳 false。</returns>
    bool IsMutualFriend(string targetName, string name1, string name2);
}

/// <summary>
/// 這是「小華」提供的具體實作類別。
/// 你不能修改這個類別，只能透過 Adapter 來使用它。
/// </summary>
public class SuperRelationshipAnalyzer : ISuperRelationshipAnalyzer
{
    // 內部使用鄰接串列 (Adjacency List) 來儲存好友關係圖
    private Dictionary<string, HashSet<string>> _friendshipGraph;

    public SuperRelationshipAnalyzer()
    {
        _friendshipGraph = new Dictionary<string, HashSet<string>>();
    }

    /// <summary>
    /// 實作 Init 方法，解析 "A -- B" 格式。
    /// </summary>
    public void Init(string script)
    {
        // 每次初始化都清空舊資料
        _friendshipGraph = new Dictionary<string, HashSet<string>>();

        if (string.IsNullOrWhiteSpace(script))
        {
            return;
        }

        // 拆分每一行
        var lines = script.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            // 拆分 " -- "
            var parts = line.Split(new[] { " -- " }, StringSplitOptions.None);
            if (parts.Length == 2)
            {
                string name1 = parts[0].Trim();
                string name2 = parts[1].Trim();

                // 因為好友關係是雙向的，所以兩邊都要加入
                AddFriendship(name1, name2);
                AddFriendship(name2, name1);
            }
        }
    }

    /// <summary>
    /// 實作 IsMutualFriend 方法。
    /// </summary>
    public bool IsMutualFriend(string targetName, string name1, string name2)
    {
        // 檢查 name1 是否是 targetName 的朋友
        bool isFriendOfName1 = _friendshipGraph.ContainsKey(name1) && 
                               _friendshipGraph[name1].Contains(targetName);

        // 檢查 name2 是否是 targetName 的朋友
        bool isFriendOfName2 = _friendshipGraph.ContainsKey(name2) && 
                               _friendshipGraph[name2].Contains(targetName);

        // 必須兩者皆是，才是共同好友
        return isFriendOfName1 && isFriendOfName2;
    }

    // 內部輔助方法，用來建立雙向關係
    private void AddFriendship(string person, string friend)
    {
        if (!_friendshipGraph.ContainsKey(person))
        {
            _friendshipGraph[person] = new HashSet<string>();
        }
        _friendshipGraph[person].Add(friend);
    }
}