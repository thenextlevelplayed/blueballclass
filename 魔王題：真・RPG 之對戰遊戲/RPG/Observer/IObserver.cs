namespace RPG.Observer;

public interface IObserver
{
    Role Role { get; set; }
    void Action();
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
        Role.SummonerAndSummon.Summoner.Hp += 30;
        Role.SummonerAndSummon = new SummonerAndSummon();
        Role.SummonerAndSummon.Summoner.SummonerAndSummon.Summoned.Remove(Role);
    }
}