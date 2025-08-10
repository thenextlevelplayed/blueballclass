using System.Security.Cryptography;
using RPG.ActionOption;
using RPG.Command;
using RPG.Enum;
using RPG.Observer;
using RPG.Status;

namespace RPG;

public abstract class Role
{
    public List<SpellcasterAndTheCursed> Spellcaster { get; set; } = new List<SpellcasterAndTheCursed>();
    public List<SpellcasterAndTheCursed> TheCursed { get; set; } = new List<SpellcasterAndTheCursed>();
    public SummonerAndSummon Summoned { get; set; }
    public List<SummonerAndSummon> Summoner { get; set; } = new List<SummonerAndSummon>();
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Str { get; set; }
    public State State { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public abstract ICommand S1();
    public abstract List<Role> S2(ICommand s1);
    public List<ICommand> Commands { get; set; } = new List<ICommand>();
    // public List<IActionOption> ActionOptions { get; set; } = new List<IActionOption>();

    public List<IObserver> Observers { get; set; } = new List<IObserver>();

    public Troop Troop { get; set; }

    public Role(string name, int hp, int mp, int str, params string[]? skillNames)
    {
        Hp = hp;
        Mp = mp;
        Str = str;
        Name = name;
        Duration = 1;
        State = new Normal(this);
        var bascicAttack = new BasicAttack(this);
        bascicAttack.Role = this;
        Commands.Add(new BasicAttackCommand(bascicAttack));
        SetCommands(skillNames);
    }

    public bool CanAction()
    {
        return this.State.CanAction;
    }

    public void EnterState(State state)
    {
        this.State.EntryState(state);
    }

    public List<Role> GetAvailableAllyTargets()
    {
        return this.Troop.Roles.Where(r => r != this).ToList();
    }

    public List<Role> GetAvailableEnemyTargets()
    {
        return this.Troop.EnemyTroop.Roles;
    }

    public void S3(ICommand command, List<Role> roles)
    {
        if (Commands.Contains(command))
        {
            if (this.Mp < command.ActionOption.Mp)
            {
                throw new InvalidOperationException(
                    $"{this.Name} does not have enough MP to perform {command.GetType().Name}.");
            }

            this.Mp -= command.ActionOption.Mp;
            command.Execute(roles);
        }
        else
        {
            throw new ArgumentException($"Command {command.GetType().Name} is not available for {this.Name}.");
        }
    }

    public void SetCommands(params string[]? skillNames)
    {
        if (skillNames == null || skillNames.Length == 0)
        {
            return;
        }

        foreach (var skillName in skillNames)
        {
            switch (skillName)
            {
                case "水球":
                    var waterBall = new WaterBall(this);
                    Commands.Add(new WaterBallCommand(waterBall));
                    break;
                case "火球":
                    var fireBall = new FireBall(this);
                    Commands.Add(new FireBallCommand(fireBall));
                    break;
                case "自我治療":
                    var selfHealing = new SelfHealing(this);
                    Commands.Add(new SelfHealingCommand(selfHealing));
                    break;
                case "石化":
                    var petrochemical = new ActionOption.Petrochemical(this);
                    Commands.Add(new PetrochemicalCommand(petrochemical));
                    break;
                case "下毒":
                    var poison = new Poison(this);
                    Commands.Add(new PoisonCommand(poison));
                    break;
                case "召喚":
                    var summon = new Summon(this);
                    Commands.Add(new SummonCommand(summon));
                    break;
                case "自爆":
                    var selfExplosion = new SelfExplosion(this);
                    Commands.Add(new SelfExplosionCommand(selfExplosion));
                    break;
                case "鼓舞":
                    var cheerUp = new ActionOption.CheerUp(this);
                    Commands.Add(new CheerUpCommand(cheerUp));
                    break;
                case "詛咒":
                    var curse = new Curse(this);
                    Commands.Add(new CurseCommand(curse));
                    break;
                case "一拳攻擊":
                    var onePunchAttack = new OnePunch(this);
                    Commands.Add(new OnePunchCommand(onePunchAttack));
                    break;
                default:
                    throw new ArgumentException($"Unknown skill name: {skillName}");
            }
        }
    }

    public void HandleStartOfTurn()
    {
        this.State.HandleStartOfTurn();
    }

    public void HandleEndOfTurn()
    {
        this.State.HandleEndOfTurn();
    }

    public void RegisterObserver(IObserver observer)
    {
        if (observer == null) return;
        Observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        if (observer == null) return;
        Observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        if (this.Hp <= 0)
        {
            foreach (var observer in Observers)
            {
                observer.Action();
            }
        }
    }
}

public class AI : Role
{
    private int Seed { get; set; } = 0;

    public AI(string name, int hp, int mp, int str, params string[]? skillNames) : base(name, hp, mp, str, skillNames)
    {
    }

    // 假設在你的角色類別 (Hero/Monster) 中有這個成員變數
// private int Seed = 0;

    public override ICommand S1()
    {
        Console.WriteLine($"輪到 {Troop}{Name} (HP: {Hp}, MP: {Mp}, STR: {Str}, State: {State})。");
        while (true)
        {
            // 1. 在每一次嘗試前，都印出可選行動
            string output = $"選擇行動：";
            for (int i = 0; i < Commands.Count; i++)
            {
                output += $"({i}) ";
                output += Commands[i].ActionOption + " ";
            }
            Console.WriteLine(output.Trim());

            // 2. 根據「目前」的 seed 決定要嘗試的行動
            int currentIndex = this.Seed % Commands.Count;
            ICommand currentCommand = Commands[currentIndex];

            // 3. 檢查 MP 是否足夠
            if (currentCommand.ActionOption.Mp <= Mp)
            {
                // 如果足夠，決策成功！
                // 將 seed+1 為「下一回合」做準備，然後返回這個行動
                this.Seed++;
                return currentCommand;
            }
            else
            {
                // 如果 MP 不足，決策失敗！
                Console.WriteLine("你缺乏 MP，不能進行此行動。");
                // 根據規則，被迫進行「下一次」決策，所以將 seed+1
                // 迴圈將會用新的 seed 值繼續執行
                this.Seed++;
            }
        }

        // 如果跑完一圈都找不到，則拋出例外
        Console.WriteLine($"{Name} 找不到可執行的行動。");
        throw new InvalidOperationException("error: No valid action available.");
    }

    public override List<Role> S2(ICommand s1)
    {
        if (s1.ActionOption.PassS2)
        {
            if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.None)
            {
                // this.Seed++;
                return new List<Role>();
            }
            else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.AllEnemy)
            {
                // this.Seed++;
                return Troop.EnemyTroop.Roles;
            }
            else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.Self)
            {
                // this.Seed++;
                return new List<Role> { this };
            }
            else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.All)
            {
                // this.Seed++;
                var roles = new List<Role>();
                roles.AddRange(Troop.EnemyTroop.Roles);
                roles.AddRange(Troop.Roles.Where(m => m != this).ToList());
                return roles;
            }
            else
            {
                throw new InvalidOperationException("Unknown target relation.");
            }
        }

        List<Role> availableTargets;
        if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.Enemy)
        {
            availableTargets = GetAvailableEnemyTargets();
        }
        else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.Ally)
        {
            availableTargets = GetAvailableAllyTargets();
        }
        else
        {
            throw new InvalidOperationException("Unknown target relation.");
        }

        if (s1.ActionOption.TargetCondition.MaxTargets >= availableTargets.Count())
        {
            // 目標足夠或過多時，選擇所有，這也算一次決策
            this.Seed++;
            return availableTargets;
        }
        else
        {
            var selectedIndices = new List<int>();
            for (int i = 0; i < s1.ActionOption.TargetCondition.MaxTargets; i++)
            {
                // 在搜尋過程中，只使用 Seed 來計算
                int numb = (this.Seed + i) % availableTargets.Count();
                selectedIndices.Add(numb);
            }

            // 選擇完成，決策完成！
            // 統一規則：完成決策後，Seed 加一
            this.Seed++;

            // 建議加上 .Distinct() 來處理 m > n 時的重複選取問題
            return availableTargets.Where((_, index) => selectedIndices.Distinct().Contains(index)).ToList();
        }
    }
}

public class Hero : Role
{
    public Hero(string name, int hp, int mp, int str, params string[]? skillNames) : base(name, hp, mp, str, skillNames)
    {
    }

    public override ICommand S1()
    {
        Console.WriteLine($"輪到 {Troop}{Name} (HP: {Hp}, MP: {Mp}, STR: {Str}, State: {State})。");

        // 等待玩家輸入
        int? choice = null;
        while (choice == null)
        {
            string output = $"選擇行動：";
            for (int i = 0; i < Commands.Count; i++)
            {
                output += $"({i}) ";
                output += Commands[i].ActionOption + " ";
            }

            Console.WriteLine(output.Trim());
            var input = Troop.Rpg.ReadInput(); //測資
            if (input != null && int.TryParse(input, out int index) && index >= 0 && index < Commands.Count)
            {
                // 先判斷輸入是否有效，再檢查 MP
                if (Commands[index].ActionOption.Mp <= Mp) // 使用 index 來檢查
                {
                    choice = index; // 所有條件都滿足，賦值給 choice，迴圈將會結束
                }
                else
                {
                    // MP 不足
                    Console.WriteLine("你缺乏 MP，不能進行此行動。");
                }
            }
            else
            {
                Console.WriteLine($"輸入無效。請輸入一個有效的數字選項。");
            }
        }

        return Commands[choice!.Value];
    }

    public override List<Role> S2(ICommand s1)
    {
        if (s1.ActionOption.PassS2)
        {
            if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.None)
            {
                return new List<Role>();
            }
            else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.AllEnemy)
            {
                return Troop.EnemyTroop.Roles;
            }
            else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.Self)
            {
                return new List<Role> { this };
            }
            else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.All)
            {
                var roles = new List<Role>();
                roles.AddRange(Troop.EnemyTroop.Roles);
                roles.AddRange(Troop.Roles.Where(m => m != this).ToList());
                return roles;
            }
            else
            {
                throw new InvalidOperationException("Unknown target relation.");
            }
        }
        else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.Enemy)
        {
            if (s1.ActionOption.TargetCondition.MaxTargets >= GetAvailableEnemyTargets().Count())
            {
                return GetAvailableEnemyTargets();
            }
            else
            {
                // 如果目標數量小於最大目標數量，則選擇目標
                string output = $"選擇 {s1.ActionOption.TargetCondition.MaxTargets} 位目標: ";
                for (int i = 0; i < GetAvailableEnemyTargets().Count(); i++)
                {
                    output += $"({i}) {GetAvailableEnemyTargets()[i].Troop}{GetAvailableEnemyTargets()[i].Name} ";
                }

                Console.WriteLine(output.Trim());
                List<int> selectedIndices = new List<int>();
                while (selectedIndices.Count < s1.ActionOption.TargetCondition.MaxTargets && !selectedIndices.Any())
                {
                    var input = Troop.Rpg.ReadInput(); //測資
                    if (input != null)
                    {
                        List<int> tempSelectedIndices = new List<int>();
                        input.Split(',').ToList().ForEach(m => tempSelectedIndices.Add(int.Parse(m)));
                        if (tempSelectedIndices.Count() == s1.ActionOption.TargetCondition.MaxTargets)
                        {
                            selectedIndices = tempSelectedIndices;
                        }
                        else
                        {
                            Console.WriteLine($"請選擇 {s1.ActionOption.TargetCondition.MaxTargets} 個目標索引。");
                        }
                    }
                    else
                    {
                        Console.WriteLine("請輸入有效的目標索引。");
                    }
                }

                return GetAvailableEnemyTargets().Where((_, index) => selectedIndices.Contains(index)).ToList();
            }
        }
        else if (s1.ActionOption.TargetCondition.TargetRelation == TargetRelation.Ally)
        {
            if (s1.ActionOption.TargetCondition.MaxTargets >= GetAvailableAllyTargets().Count())
            {
                return GetAvailableAllyTargets();
            }
            else
            {
                // 如果目標數量小於最大目標數量，則選擇目標
                string output = $"選擇 {s1.ActionOption.TargetCondition.MaxTargets} 位目標: ";
                for (int i = 0; i < GetAvailableAllyTargets().Count(); i++)
                {
                    output += $"({i}) {GetAvailableAllyTargets()[i].Troop}{GetAvailableAllyTargets()[i].Name} ";
                }

                Console.WriteLine(output.Trim());
                List<int> selectedIndices = new List<int>();
                while (selectedIndices.Count == 0) // 或者使用一個布林值 isChoiceMade = false;
                {
                    var input = Troop.Rpg.ReadInput();
                    if (input != null)
                    {
                        List<int> tempSelectedIndices = new List<int>();
                        try
                        {
                            input.Split(',').ToList()
                                .ForEach(m => tempSelectedIndices.Add(int.Parse(m.Trim()))); // 加上 Trim() 更健壯
                            if (tempSelectedIndices.Count() == s1.ActionOption.TargetCondition.MaxTargets)
                            {
                                selectedIndices = tempSelectedIndices;
                                // 選擇成功，可以跳出迴圈了
                            }
                            else
                            {
                                Console.WriteLine($"請選擇 {s1.ActionOption.TargetCondition.MaxTargets} 個目標索引。");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("輸入格式錯誤，請輸入以逗號分隔的數字。");
                        }
                    }
                    else
                    {
                        Console.WriteLine("請輸入有效的目標索引。");
                    }
                }

                return GetAvailableAllyTargets().Where((_, index) => selectedIndices.Contains(index)).ToList();
            }
        }
        else
        {
            throw new InvalidOperationException("Unknown target relation.");
        }
    }
}