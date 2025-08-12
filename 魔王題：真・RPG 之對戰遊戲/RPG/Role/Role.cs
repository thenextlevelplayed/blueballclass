using System.Security.Cryptography;
using RPG.ActionOption;
using RPG.Command;
using RPG.CommandHandler;
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
    public List<ICommand> Commands { get; set; } = new List<ICommand>();

    public List<IObserver> Observers { get; set; } = new List<IObserver>();

    public Troop Troop { get; set; }

    public Role(string name, int hp, int mp, int str)
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
    }

    public bool CanAction()
    {
        return this.State.CanAction;
    }

    public void EnterState(State state)
    {
        this.State.EntryState(state);
    }

    protected List<Role> GetAvailableAllyTargets()
    {
        return this.Troop.Roles.Where(r => r != this).ToList();
    }

    protected List<Role> GetAvailableEnemyTargets()
    {
        return this.Troop.EnemyTroop.Roles.ToList();
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

    protected abstract int SelectActionIndexHook(); 
    protected abstract void S2PostDecisionHook();
    protected abstract List<Role> SelectTargetsHook(List<Role> availableTargets, int targetConditionMaxTargets);

    public ICommand TemplateS1()
    {
        while (true)
        {
            // 共通邏輯：顯示所有可選行動
            string output = $"選擇行動：";
            for (int i = 0; i < Commands.Count; i++)
            {
                output += $"({i}) ";
                output += Commands[i].ActionOption + " ";
            }
            Console.WriteLine(output.Trim());

            // 可變邏輯：把「如何選擇」交給子類別的鉤子方法
            int proposedIndex = SelectActionIndexHook();

            // 共通邏輯：驗證索引並檢查 MP
            if (proposedIndex >= 0 && proposedIndex < Commands.Count)
            {
                ICommand proposedCommand = Commands[proposedIndex];
                if (proposedCommand.ActionOption.Mp <= this.Mp)
                {
                    // 成功！返回指令
                    return proposedCommand; 
                }
                else
                {
                    // MP 不足
                    Console.WriteLine("你缺乏 MP，不能進行此行動。");
                }
            }
            else
            {
                // 只有 Hero 可能輸入無效索引，AI 的計算總是在範圍內
                Console.WriteLine($"輸入無效。請輸入一個有效的數字選項。");
            }
            
        }
    }

    public List<Role> TemplateS2(ICommand s1)
    {
        if (s1.ActionOption.PassS2)
        {
            S2PostDecisionHook();
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
            S2PostDecisionHook();
            return availableTargets;
        }

        return SelectTargetsHook(availableTargets, s1.ActionOption.TargetCondition.MaxTargets);
    }
}