using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.Enum;

namespace 二維地圖冒險遊戲.CharacterState;

public class Stockpile(Character character) : State(character)
{
    public override int InitialDuration { get; } = 2;

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

    public override void HandleUnderAttack(int attackerAp, Map map)
    {
        Character.ApplyDamage(attackerAp, map);
        Character.EnterState(new Normal(Character));
    }

    public override void HandleStartOfTurn()
    {
        
    }
    
    public override void HandleEndOfTurn(Map map) //回合結束動作
    {
        if (Character.Duration > 0)
        {
            Character.Duration--;
        }
        
        if (Character.Duration == 0)
        {
            Character.EnterState(new Erupting(Character));
        }
    }
}