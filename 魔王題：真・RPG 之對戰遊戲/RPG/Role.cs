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
        var bascicAttack = new BasicAttack();
        bascicAttack.Role = this;
        Commands.Add(new BasicAttackCommand(bascicAttack));
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
    public int Seed { get; set; } = 0;

    public AI(string name, int hp, int mp, int str, params string[]? skillNames) : base(name, hp, mp, str, skillNames)
    {
    }

    public override ICommand S1()
    {
        Console.WriteLine($"輪到 {Troop}{Name} (HP: {Hp}, MP: {Mp}, STR: {Str}, State: {State})。");

        // 等待玩家輸入
        int? choice = null;
        while (choice != null)
        {
            string output = $"選擇行動：";
            for (int i = 0; i < Commands.Count; i++)
            {
                output += $"({i}) ";
                output += Commands[i].ActionOption + " ";
            }

            Console.WriteLine(output);
            if (Seed >= 0 && Seed < Commands.Count
                && Commands[choice.Value].ActionOption.Mp <= Mp)
            {
                choice = Seed;
            }
            else
            {
                Console.WriteLine("你缺乏 MP，不能進行此行動。");
                Seed++;
            }
        }

        Seed++;
        return Commands[choice!.Value];
    }

    public override List<Role> S2(ICommand s1)
    {
        throw new NotImplementedException();
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
        while (choice != null)
        {
            string output = $"選擇行動：";
            for (int i = 0; i < Commands.Count; i++)
            {
                output += $"({i}) ";
                output += Commands[i].ActionOption + " ";
            }

            Console.WriteLine(output);
            var input = Console.ReadLine();
            if (input != null && int.TryParse(input, out int index) && index >= 0 && index < Commands.Count
                && Commands[choice.Value].ActionOption.Mp <= Mp)
            {
                choice = index;
            }
            else
            {
                Console.WriteLine("你缺乏 MP，不能進行此行動。");
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
                    output += $"({i}) [{GetAvailableEnemyTargets()[i].Troop}]{GetAvailableEnemyTargets()[i].Name} ";
                }

                Console.WriteLine(output);
                List<int> selectedIndices = new List<int>();
                while (selectedIndices.Count < s1.ActionOption.TargetCondition.MaxTargets && !selectedIndices.Any())
                {
                    var input = Console.ReadLine();
                    if (input != null)
                    {
                        List<int> tempSelectedIndices = new List<int>();
                        input.Split(',').ToList().ForEach(m => tempSelectedIndices.Add(int.Parse(m)));
                        if (tempSelectedIndices.Count() == S1().ActionOption.TargetCondition.MaxTargets)
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
                for (int i = 0; i < GetAvailableEnemyTargets().Count(); i++)
                {
                    output += $"({i}) [{GetAvailableAllyTargets()[i].Troop}]{GetAvailableAllyTargets()[i].Name} ";
                }

                Console.WriteLine(output);
                List<int> selectedIndices = new List<int>();
                while (selectedIndices.Count < s1.ActionOption.TargetCondition.MaxTargets && !selectedIndices.Any())
                {
                    var input = Console.ReadLine();
                    if (input != null)
                    {
                        List<int> tempSelectedIndices = new List<int>();
                        input.Split(',').ToList().ForEach(m => tempSelectedIndices.Add(int.Parse(m)));
                        if (tempSelectedIndices.Count() == S1().ActionOption.TargetCondition.MaxTargets)
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
                return GetAvailableAllyTargets().Where((_, index) => selectedIndices.Contains(index)).ToList();
            }
        }
        else
        {
            throw new InvalidOperationException("Unknown target relation.");
        }
    }
}