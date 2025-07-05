using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.CharacterState;

namespace 二維地圖冒險遊戲.TreasureObject;

public abstract class Treasure : IMapObject
{

    public string DisplaySymbol { get; } = "x";
    public abstract double ProbabilityEvent { get; set; }
    public int? X { get; set; } = null;
    public int? Y { get; set; } = null;
    public abstract State Touched(Character character);
    
    public void SetPosition(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}