using System.Reflection;
using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.CharacterState;
using 二維地圖冒險遊戲.Enum;
using 二維地圖冒險遊戲.TreasureObject;

namespace 二維地圖冒險遊戲;

public class Game
{
    private Map Map { get; set; }
    private Main Main { get; set; }
    private List<Monster> Monsters { get; set; } = new List<Monster>();
    private List<Obstacle> Obstacles { get; set; } = new List<Obstacle>();
    private List<Treasure> Treasures { get; set; } = new List<Treasure>();
    private List<Type> _availableTreasureTypes;
    private readonly Random _random = new Random();

    public Game()
    {
        Map = CreateMap(5, 5);
    }

    public Map CreateMap(int width, int height)
    {
        return new Map(width, height);
    }

    public List<Monster> CreateMonsters(int count)
    {
        var monsters = new List<Monster>();
        for (int i = 0; i < count; i++)
        {
            var direction = CreateDirection();
            var monster = new Monster(direction);
            CreateIMapObjectPosition(monster);
            monsters.Add(monster);
        }

        return monsters;
    }

    public List<Treasure> CreateTreasure(int count)
    {
        // Reflection
        var treasures = new List<Treasure>();
        var treasureTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Treasure)));

        // ProbabilityEvent
        var weightedList = new List<(Type type, double probability)>();
        foreach (var type in treasureTypes)
        {
            try
            {
                // 創建臨時實例以獲取 ProbabilityEvent
                Treasure tempInstance = (Treasure)Activator.CreateInstance(type);
                double probability = tempInstance.ProbabilityEvent;
                if (probability > 0)
                {
                    weightedList.Add((type, probability));
                }
                else
                {
                    Console.WriteLine($"Warning: {type.Name} has invalid probability ({probability})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting probability for {type.Name}: {ex.Message}");
            }
        }

        if (!weightedList.Any())
        {
            Console.WriteLine("No valid treasures with positive probability found.");
            return treasures;
        }

        //Calculate WeightedList
        double totalProbability = weightedList.Sum(t => t.probability);
        //random generate count treasure
        for (int i = 0; i < count; i++)
        {
            double diceRoll = _random.NextDouble() * totalProbability;
            double cumulative = 0.0;
            Type selectedType = null;
            foreach (var item in weightedList)
            {
                cumulative += item.probability;
                if (cumulative > diceRoll)
                {
                    selectedType = item.type;
                    break;
                }
            }

            if (selectedType != null)
            {
                try
                {
                    Treasure treasure = (Treasure)Activator.CreateInstance(selectedType);
                    CreateIMapObjectPosition(treasure);
                    treasures.Add(treasure);
                    Console.WriteLine($"Generated treasure: {treasure.GetType().Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating {selectedType.Name}: {ex.Message}");
                }
            }
        }

        return treasures;
    }

    public List<Obstacle> CreateObstacle(int count)
    {
        var obstacles = new List<Obstacle>();
        for (int i = 0; i < count; i++)
        {
            var obstacle = new Obstacle(CreateDirection());
            CreateIMapObjectPosition(obstacle);
        }

        return obstacles;
    }

    public void RandomlyAddMonstersAndTreasures()
    {
        var num = _random.Next(2);

        // for (int i = 0; i < num; i++)
        // {
        //     var direction = CreateDirection();
        //     var monster = new Monster(direction);
        //     CreateIMapObjectPosition(monster);
        //     this.Monsters.Add(monster);
        // }

        var monsters = CreateMonsters(num);
        var treasures = CreateTreasure(num);

        this.Monsters.AddRange(monsters);
        this.Treasures.AddRange(treasures);
    }

    public void Round()
    {
        while (true)
        {
            if (Monsters.Count == 0)
            {
                Console.WriteLine("All monsters have been killed.");
                break;
            }

            if (Main.Hp <= 0)
            {
                Console.WriteLine("You lose.");
                break;
            }

            RandomlyAddMonstersAndTreasures();
            Map.DisplayMap();
            Main.PrintInfo();
            // new process
            int actionsForThisTurn = Main.State.GetNumberOfActionsPerTurn;
            for (int i = 0; i < actionsForThisTurn; i++)
            {
                Main.HandleStartOfTurn(); // 處理狀態開始時的邏輯
                if (Main.CanAttack() && Main.ActionDetails.AllowDirection.Count > 0)
                {
                    HandleActionDecision(Map);
                }
                else
                {
                    HandleMovementDecision(Map);
                }

                // Main.Duration--;
                CleanupTreasures();
                CleanupDeadMonsters();
            }

            if (Main.Hp <= 0)
            {
                Console.WriteLine("You lose While under your turn.");
                break;
            }

            if (Monsters.Count == 0 && Main.Hp > 0)
            {
                Console.WriteLine("All monsters have been killed.");
                break;
            }

            //-----monster turn start-----

            foreach (var monster in Monsters)
            {
                bool actionTaken = false;
                actionsForThisTurn = monster.State.GetNumberOfActionsPerTurn;
                for (int i = 0; i < actionsForThisTurn; i++)
                {
                    monster.HandleStartOfTurn(); // 處理狀態開始時的邏輯

                    // ================== 新的、更強大的決策樹 ==================
                    bool canAttack = monster.CanAttack();
                    // 將攻擊條件的判斷，委派給當前的 State
                    bool attackConditionMet = monster.State.IsAttackConditionMet(Map, monster);
                    if (canAttack && attackConditionMet)
                    {
                        monster.Attack(Map, monster.ActionDetails);
                        actionTaken = true;
                    }

                    if (!actionTaken && monster.ActionDetails.AllowDirection.Count > 0)
                    {
                        Console.WriteLine($"怪物 選擇移動。");


                        Direction chosenDir =
                            monster.ActionDetails.AllowDirection[
                                _random.Next(monster.ActionDetails.AllowDirection.Count)];
                        int targetX = monster.X.Value;
                        int targetY = monster.Y.Value;
                        switch (chosenDir)
                        {
                            case Direction.Up: targetY--; break;
                            case Direction.Down: targetY++; break;
                            case Direction.Left: targetX--; break;
                            case Direction.Right: targetX++; break;
                        }

                        monster.Move(Map, targetX, targetY, chosenDir);
                        actionTaken = true;
                    }
                    else
                    {
                        Console.WriteLine($" 怪物  在混亂中找不到可移動的方向。");
                    }
                }

                monster.HandleEndOfTurn(Map);
            }

            CleanupDeadMonsters();
            CleanupTreasures();
            if (Main.Hp <= 0)
            {
                Console.WriteLine("You lose While under your turn.");
                break;
            }

            Main.HandleEndOfTurn(Map);
        }
    }
    
    public void CreateMapObject()
    {
        var monsters = CreateMonsters(5);
        var treasures = CreateTreasure(5);
        var obstacles = CreateObstacle(2);
        this.Monsters.AddRange(monsters);
        this.Treasures.AddRange(treasures);
        this.Obstacles.AddRange(obstacles);
    }

    public void CreateMain()
    {
        this.Main = new Main(CreateDirection());
        CreateIMapObjectPosition(Main);
    }

    public void CreateIMapObjectPosition(IMapObject obj)
    {
        int width = Map.Width;
        int height = Map.Height;

        if (obj is Character character) // 檢查是否可以設定位置
        {
            while (character.X == null && character.Y == null)
            {
                var x = _random.Next(0, width);
                var y = _random.Next(0, height);
                if (Map.Grid[x, y] == null)
                {
                    Map.Grid[x, y] = obj;
                    character.SetPosition(x, y);
                }
            }
        }

        if (obj is Treasure treasure) // 檢查是否可以設定位置
        {
            while (treasure.X == null && treasure.Y == null)
            {
                var x = _random.Next(0, width);
                var y = _random.Next(0, height);
                if (Map.Grid[x, y] == null)
                {
                    Map.Grid[x, y] = obj;
                    treasure.SetPosition(x, y);
                }
            }
        }

        if (obj is Obstacle obstacle) // 檢查是否可以設定位置
        {
            while (obstacle.X == null && obstacle.Y == null)
            {
                var x = _random.Next(0, width);
                var y = _random.Next(0, height);
                if (Map.Grid[x, y] == null)
                {
                    Map.Grid[x, y] = obj;
                    obstacle.SetPosition(x, y);
                }
            }
        }
    }

    public Direction CreateDirection()
    {
        return (Direction)_random.Next(0, 4);
    }

    private void LoadAvailableTreasureTypes()
    {
        _availableTreasureTypes = new List<Type>();
        // 獲取當前執行程式集 (如果 Treasure 子類別在其他程式集，需要調整)
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        // 或者，如果你知道 Treasure 基類所在的程式集
        // Assembly treasureAssembly = typeof(Treasure).Assembly;

        _availableTreasureTypes = currentAssembly.GetTypes()
            .Where(type => type.IsClass && // 必須是類別
                           !type.IsAbstract && // 不能是抽象類別 (因為無法實例化)
                           type.IsSubclassOf(typeof(Treasure))) // 必須是 Treasure 的子類別
            .ToList();

        Console.WriteLine($"Found {_availableTreasureTypes.Count} treasure types:");
        foreach (var type in _availableTreasureTypes)
        {
            Console.WriteLine($"- {type.FullName}");
        }
    }

    private void CleanupDeadMonsters()
    {
        int initialCount = Monsters.Count;

        Monsters.RemoveAll(monster =>
        {
            if (monster.Hp <= 0)
            {
                Console.WriteLine($"{monster.DisplaySymbol} (原HP: {monster.Hp}) 被從活躍怪物列表中移除。");
                if (monster.X != null && monster.Y != null && Map.Grid[monster.X.Value, monster.Y.Value] == monster)
                {
                    Map.RemoveObjectAt(monster.X.Value, monster.Y.Value);
                }

                return true;
            }

            return false;
        });
        if (Monsters.Count < initialCount)
        {
            Console.WriteLine($"清除了 {initialCount - Monsters.Count} 個死亡的怪物。");
        }
    }

    private void CleanupTreasures()
    {
        int initialCount = Treasures.Count;

        Treasures.RemoveAll(treasure =>
        {
            if (Map.Grid[treasure.X.Value, treasure.Y.Value] == null)
            {
                Console.WriteLine($"{treasure.DisplaySymbol} ({treasure.GetType().Name}) 被寶物列表中移除。");
                if (treasure.X != null && treasure.Y != null &&
                    Map.Grid[treasure.X.Value, treasure.Y.Value] == treasure)
                {
                    Map.RemoveObjectAt(treasure.X.Value, treasure.Y.Value);
                }

                return true;
            }

            return false;
        });
        if (Treasures.Count < initialCount)
        {
            Console.WriteLine($"清除了 {initialCount - Treasures.Count} 個寶物。");
        }
    }

    public void HandleMovementDecision(Map map)
    {
        if (Main.ActionDetails.AllowDirection.Count == 0)
        {
            Console.WriteLine("沒有可移動的方向。");
            return;
        }

        // 使用字典來動態映射選項和方向
        var availableMoves = new Dictionary<string, Direction>();
        int optionIndex = 1;

        Console.WriteLine("請選擇移動方向:");
        foreach (var dir in Main.ActionDetails.AllowDirection)
        {
            // 將選項數字和方向關聯起來
            availableMoves.Add(optionIndex.ToString(), dir);
            Console.WriteLine($"[{optionIndex}] {GetDirectionText(dir)}");
            optionIndex++;
        }

        // 循環直到玩家輸入有效的指令
        while (true)
        {
            string command = Console.ReadLine();
            if (availableMoves.TryGetValue(command, out Direction chosenDirection))
            {
                // 檢查角色座標是否有效
                if (!Main.X.HasValue || !Main.Y.HasValue)
                {
                    Console.WriteLine("錯誤：角色座標未知，無法計算移動。");
                    return;
                }

                // 1. 獲取當前座標
                int currentX = Main.X.Value;
                int currentY = Main.Y.Value;

                // 2. 根據選擇的方向，計算目標座標
                int targetX = currentX;
                int targetY = currentY;

                switch (chosenDirection)
                {
                    case Direction.Up:
                        targetY--;
                        break;
                    case Direction.Down:
                        targetY++;
                        break;
                    case Direction.Left:
                        targetX--;
                        break;
                    case Direction.Right:
                        targetX++;
                        break;
                }

                // 3. 使用計算出的座標呼叫你的 Move 方法
                Main.Move(map, targetX, targetY, chosenDirection);

                break; // 動作完成，跳出迴圈
            }
            else
            {
                Console.WriteLine("無效的指令，請重新輸入。");
            }
        }
    }

    public void HandleActionDecision(Map map)
    {
        Console.WriteLine("請選擇要執行的動作:");
        Console.WriteLine("[1] 攻擊");
        Console.WriteLine("[2] 移動");

        // 循環直到玩家輸入有效的指令
        while (true)
        {
            string command = Console.ReadLine();
            if (command == "1")
            {
                // 執行攻擊
                Main.Attack(map, Main.ActionDetails);
                break; // 成功攻擊，跳出迴圈
            }
            else if (command == "2")
            {
                // 複用移動決策的函式
                HandleMovementDecision(map);
                break; // 移動完成，跳出迴圈
            }
            else
            {
                Console.WriteLine("無效的指令，請重新輸入 [1] 或 [2]。");
            }
        }
    }

    public static Main FindAdjacentPlayer(Map map, int currentX, int currentY)
    {
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int neighborX = currentX + dx[i];
            int neighborY = currentY + dy[i];

            if (neighborX >= 0 && neighborX < map.Width &&
                neighborY >= 0 && neighborY < map.Height)
            {
                // 使用 is 關鍵字檢查類型，並同時賦值給新變數 targetMain
                if (map.GetObjectAt(neighborX, neighborY) is Main targetMain)
                {
                    // 找到了，直接回傳這個 Main 物件
                    return targetMain;
                }
            }
        }

        // 迴圈跑完都沒找到，回傳 null
        return null;
    }

    private string GetDirectionText(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return "向上";
            case Direction.Down: return "向下";
            case Direction.Left: return "向左";
            case Direction.Right: return "向右";
            default: return "未知方向";
        }
    }
}