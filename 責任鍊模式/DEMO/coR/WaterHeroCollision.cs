namespace DEMO.coR;

public class WaterHeroCollision(CollisionHandler? next):CollisionHandler(next)
{
    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Water) && x2.GetType() == typeof(Hero);
    }

    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        world.RemoveSprite(x1);
        x2.IncreaseHp(10);
        Console.WriteLine($"Hero current hp: {x2.GetHp()}");
        Console.WriteLine("Hero increases Hp and Water is removed.");
    }
}