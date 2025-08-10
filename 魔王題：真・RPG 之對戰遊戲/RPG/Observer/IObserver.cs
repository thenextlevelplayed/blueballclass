namespace RPG.Observer;

public interface IObserver
{
    Role Role { get; set; }
    void Action();
}

public class CurseBuff(Role role) : IObserver
{
    public Role Role { get; set; } = role;

    public void Action()
    {
        foreach (var curse in Role.TheCursed)
        {
            if (curse.Caster.Hp > 0)
            {
                curse.Caster.Hp += curse.Victim.Mp;
                curse.Caster.Spellcaster.Remove(curse);
            }
        }
        
    }
}

public class SummonBuff : IObserver
{
    public Role Role { get; set; }

    public SummonBuff(Role role)
    {
        Role = role;
    }

    public void Action()
    {
        var summonerAndSummons = Role.Summoned;
        summonerAndSummons.Summoner.Hp += 30;
        summonerAndSummons.Summoner.Summoner.Remove(summonerAndSummons);
    }
}