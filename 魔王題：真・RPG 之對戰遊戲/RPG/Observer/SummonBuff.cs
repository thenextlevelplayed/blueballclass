namespace RPG.Observer;

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