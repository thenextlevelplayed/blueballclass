using RPG.ActionOption;
using RPG.Command;
using RPG.Observer;
using RPG.Status;
using ICommand = System.Windows.Input.ICommand;

namespace RPG;

public abstract class Role
{
    public SpellcasterAndTheCursed SpellcasterAndTheCursed { get; set; }
    public SummonerAndSummon SummonerAndSummon { get; set; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Str { get; set; }
    public State State { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public List<ICommand> Commands { get; set; } = new List<ICommand>();
    public List<IActionOption> ActionOptions { get; set; } = new List<IActionOption>();

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
        ActionOptions.Add(bascicAttack);
    }

    public bool CanAction()
    {
        return this.State.CanAction;
    }

    public void EnterState(State state)
    {
        this.State.EntryState(state);
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
                    var waterball = new WaterBall(this);
                    Commands.Add(new WaterBallCommand(waterball));
                    ActionOptions.Add(waterball);
                    break;
                case "BasicAttack":
                    Commands.Add(new BasicAttackCommand());
                    break;
                default:
                    throw new ArgumentException($"Unknown skill name: {skillName}");
            }
        }
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