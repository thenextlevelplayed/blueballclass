using RPG.ActionOption;
using RPG.Status;
namespace RPG;

public abstract class Role
{
    private SpellcasterAndTheCursed _spellcasterAndTheCursed;
    private SummonerAndSummon _summonerAndSummon;
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Str { get; set; }
    public State State { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public List<IActionOption> ActionOptions { get; set; } = new List<IActionOption>();
    public Role(string name,int hp, int mp, int str)
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

    public virtual bool CanAction()
    {
        return true;
    }

    public void EnterState(State state)
    {
        this.State.EntryState(state);
    }
}