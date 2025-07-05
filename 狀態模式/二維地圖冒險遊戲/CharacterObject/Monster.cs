using System.Reflection.Metadata;
using 二維地圖冒險遊戲.CharacterState;
using 二維地圖冒險遊戲.Enum;

namespace 二維地圖冒險遊戲.CharacterObject;

public class Monster : Character
{
    public Monster(Direction direction) : base(direction)
    {
        this.Hp = 1;
        this.MaxHp = 1;
        this.Ap = 50;
        State = new Normal(this);
        FaceInDirection = Direction.Right;
        DisplaySymbol = GetSymbolString();
        ActionDetails = new ActionDetails
        {
            BaseDamage = this.Ap,
            RangeType = AttackRangeType.OnePointMapArea,
            AllowDirection = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
        };
    }

}