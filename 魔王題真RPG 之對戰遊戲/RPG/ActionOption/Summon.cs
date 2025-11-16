using RPG.Enum;
using RPG.Observer;
using RPG.Status;

namespace RPG.ActionOption;

public class Summon : Skill
{
    public Summon(Role role) : base(role)
    {
    }

    public override int Mp { get; set; } = 150;
    public override int Str { get; set; } = 0;
    public override bool PassS2 { get; set; } = true;

    protected override State? GetState(Role target)
    {
        return null;
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.None, 0);

    protected override void ActionHook(List<Role> roles)
    {
        var slime = new AI("Slime", 100, 0, 50);
        Role.Troop.Roles.Add(slime);
        slime.Troop = Role.Troop;
        SummonerAndSummon summonerAndSummon = new SummonerAndSummon(Role, slime);
        Role.Summoner.Add(summonerAndSummon);
        slime.Summoned = summonerAndSummon;
        slime.RegisterObserver(new SummonBuff(slime));
    }
    public override string ToString()
    {
        return "召喚";
    }
}