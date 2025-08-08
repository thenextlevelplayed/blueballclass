// Step 1: 初始化兩個獨立的軍隊

using RPG;

Console.WriteLine("請貼上您的測資，以 '###' 結尾：");

var army1Roles = new List<Role>();
var army2Roles = new List<Role>();

int currentArmyFlag = 0; // 0=等待, 1=讀取軍隊1, 2=讀取軍隊2
string? line;

while ((line = Console.ReadLine()) != "###")
{
    if (string.IsNullOrWhiteSpace(line)) continue;

    // 更新狀態
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

    // 解析角色資料
    if (currentArmyFlag > 0)
    {
        string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 4) continue;

        // 根據您的 Role 建構子，解析 Name, Hp, Mp, Str
        string name = parts[0];
        int hp = int.Parse(parts[1]);
        int mp = int.Parse(parts[2]);
        int str = int.Parse(parts[3]); // 第四個參數是 Str

        // 將剩餘部分全部視為技能名稱
        string[] skillNames = parts.Skip(4).ToArray();

        Role newRole;

        // 根據名稱判斷是 Hero 還是 AI
        if (name == "英雄")
        {
            // 直接在建構子中傳入所有參數，包含技能
            newRole = new Hero(name, hp, mp, str, skillNames);
        }
        else
        {
            newRole = new AI(name, hp, mp, str, skillNames);
        }

        // 將角色加入對應的列表
        if (currentArmyFlag == 1)
        {
            army1Roles.Add(newRole);
        }
        else if (currentArmyFlag == 2)
        {
            army2Roles.Add(newRole);
        }
    }
}

Console.WriteLine("\n--- 輸入解析完畢，正在創建物件... ---\n");

// 創建物件
var troop1 = new Troop(army1Roles);
var troop2 = new Troop(army2Roles);

// 設定敵對關係
Troop.SetRelation(troop1, troop2);

// 驗證結果
Console.WriteLine("========= 物件創建結果 ==========");
Console.WriteLine($"已創建 {troop1}，成員數量: {troop1.Roles.Count}");
foreach (var role in troop1.Roles)
{
    Console.WriteLine($"- {role.Name} (HP:{role.Hp}, MP:{role.Mp}, STR:{role.Str})，隸屬於 {role.Troop}");
}

Console.WriteLine();

Console.WriteLine($"已創建 {troop2}，成員數量: {troop2.Roles.Count}");
foreach (var role in troop2.Roles)
{
    Console.WriteLine($"- {role.Name} (HP:{role.Hp}, MP:{role.Mp}, STR:{role.Str})，隸屬於 {role.Troop}");
}

Console.WriteLine();
Console.WriteLine($"關係驗證：{troop1} 的敵人是 {troop1.EnemyTroop}");
Console.WriteLine($"關係驗證：{troop2} 的敵人是 {troop2.EnemyTroop}");
var rpg = new RPG.Rpg(troop1, troop2);
// 開始戰鬥
Console.WriteLine("\n--- 戰鬥開始 ---\n");
rpg.Battle();