using RPG.Enum;

namespace RPG;

public class TargetCondition
{
    public TargetRelation TargetRelation { get; set; }
    public int MaxTargets { get; set; }

    public TargetCondition(TargetRelation targetRelation, int maxTargets)
    {
        TargetRelation = targetRelation;
        MaxTargets = maxTargets;
    }
}