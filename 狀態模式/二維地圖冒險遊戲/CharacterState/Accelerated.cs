using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.Enum;

namespace 二維地圖冒險遊戲.CharacterState;

public class Accelerated(Character character) : State(character)
{
    public override void EntryState()
    {
        var actionDetails = new ActionDetails
        {
            BaseDamage = Character.Ap,
            RangeType = Character is Main ? AttackRangeType.Line : AttackRangeType.OnePointMapArea,
            AllowDirection =  new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
        };
        character.ActionDetails = actionDetails;
    }

    public override int InitialDuration { get; } = 3;

    public override int GetNumberOfActionsPerTurn => 2;

    public override void HandleUnderAttack(int attackerAp, Map map)
    {
        Console.WriteLine($"{Character.DisplaySymbol} 處於 {Character.State.GetType().Name} 狀態，受到攻擊！加速狀態解除。");
        // 角色仍然會受到這次攻擊的傷害
        Character.ApplyDamage(attackerAp, map);
        Character.EnterState(new Normal(Character));
    }

    public override void HandleStartOfTurn()
    {
        
    }
    
    public override void HandleEndOfTurn(Map map)
    {
       
        if (Character.Duration == 0)
        {
            Character.EnterState(new Normal(Character));
        }
    }
}