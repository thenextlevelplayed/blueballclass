// Step 1: 初始化兩個獨立的軍隊

using RPG;

Console.WriteLine("#軍隊-1-開始");
Role hero = new Hero("英雄", 500, 500, 100);
Role aI = new AI("英雄", 500, 500, 100);
var troop1 = new Troop(new List<Role> { hero, aI });
Console.WriteLine("#軍隊-1-結束");

Console.WriteLine("#軍隊-2-開始");
Role slime1 = new AI("Slime1", 200, 90, 50);
Role slime2 = new AI("Slime2", 200, 90, 50);
Role slime3 = new AI("Slime3", 200, 9000, 50);
var troop2 = new Troop(new List<Role> { slime1, slime2, slime3 });
Console.WriteLine("#軍隊-2-結束");

// Step 2: 建立軍隊之間的關聯關係
// 這裡可以選擇將 troop1 設為 troop2 的敵人，反之亦然
Troop.SetRelation(troop1, troop2);

// 現在，troop1.EnemyTroop 和 troop2.EnemyTroop 都已被正確設定
Console.WriteLine($"Troop1 的敵人是: {troop1.EnemyTroop}");
Console.WriteLine($"Troop2 的敵人是: {troop2.EnemyTroop}");