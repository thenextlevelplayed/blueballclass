using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.Enum;

namespace 二維地圖冒險遊戲.CharacterState;

public class Orderless(Character character) : State(character)
{
    public override int InitialDuration { get; } = 3;

    public override void EntryState()
    {
        var actionDetails = new ActionDetails
        {
            BaseDamage = Character.Ap,
            RangeType = Character is Main ? AttackRangeType.Line : AttackRangeType.OnePointMapArea,
            AllowDirection =  new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
        };
        character.ActionDetails = actionDetails;
        DetermineRandomDirections();
    }
    
    private void DetermineRandomDirections()
    {
        var random = Character.Random.Next(2);
        var allowDirection = random == 0
            ? new List<Direction> { Direction.Up, Direction.Down }
            : new List<Direction> { Direction.Left, Direction.Right };
        Character.ActionDetails.AllowDirection = allowDirection;
    }

    public override bool CanPerformAttack()
    {
        // 混亂狀態下不能攻擊
        Console.WriteLine($"{Character.DisplaySymbol} (Main): 混亂中，無法攻擊！");
        return false;
    }

    public override void HandleStartOfTurn()
    {
        
    }

    public override void HandleUnderAttack(int attackerAp, Map map)
    {
        Character.ApplyDamage(attackerAp, map);
        Character.EnterState(new Invincible(Character));
    }
}