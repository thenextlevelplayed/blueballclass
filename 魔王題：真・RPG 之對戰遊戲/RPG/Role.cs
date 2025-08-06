using System.Security.Cryptography;
using RPG.ActionOption;
using RPG.Command;
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
    public abstract List<Role> S2();
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
    public AI(string name, int hp, int mp, int str, params string[]? skillNames) : base(name, hp, mp, str, skillNames)
    {
    }
}

public class Hero : Role
{
    public Hero(string name, int hp, int mp, int str, params string[]? skillNames) : base(name, hp, mp, str, skillNames)
    {
    }
}