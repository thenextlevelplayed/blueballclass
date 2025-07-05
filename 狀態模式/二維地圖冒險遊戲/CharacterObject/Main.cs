using 二維地圖冒險遊戲.CharacterState;
using 二維地圖冒險遊戲.Enum;

namespace 二維地圖冒險遊戲.CharacterObject;

public class Main : Character
{
    public Main(Direction direction) : base(direction)
    {
        this.Hp = 300;
        this.MaxHp = 300;
        this.Ap = 1;
        State = new Normal(this);
        FaceInDirection = Direction.Right;
        DisplaySymbol = GetSymbolString();
        ActionDetails = new ActionDetails
        {
            BaseDamage = this.Ap,
            RangeType = AttackRangeType.Line,
            AllowDirection = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
        };
    }
    
    public void PrintInfo()
    {
        DisplaySymbol = GetSymbolString();
        Console.WriteLine($"Main Hp: {Hp} State: {State.ToString()} Direction: {DisplaySymbol}");
    }

}