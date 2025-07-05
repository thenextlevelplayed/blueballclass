using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.CharacterState;

namespace 二維地圖冒險遊戲.TreasureObject;

public class SuperStar : Treasure
{
    public override double ProbabilityEvent { get; set; } = 0.1;

    public override State Touched(Character character)
    {
        return new Invincible(character);
    }
}