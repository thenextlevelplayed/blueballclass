namespace DEMO.coR;

public class HeroWaterCollision: CollisionHandler
{
    public HeroWaterCollision(CollisionHandler? next) : base(next)
    {
    }
    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Hero) && x2.GetType() == typeof(Water);
    }
    
    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        world.RemoveSprite(x2);
        x1.IncreaseHp(10);
        world.MoveSpriteToCoordinate(x1,x2.Coordinate);
        Console.WriteLine($"Hero current hp: {x1.GetHp()}");
        Console.WriteLine("Hero increases Hp and Fire is removed.");
    }
    
    // public override void SpriteCollision(Sprite x1, Sprite x2, CollisionHandler? collisionHandler)
    // {
    //     if (x1 is Hero hero1 && x2 is Water)
    //     {
    //         World.RemoveSprite(x2); //移除water
    //         World._world[x2.Coordinate] = null; //清除water在世界位置
    //         x1.IncreaseHp(10);
    //         x1.MoveToX2Coordinate(x2.Coordinate);
    //         Console.WriteLine($"Hero current hp: {x1.GetHp()}");
    //         Console.WriteLine("Hero increases Hp and Fire is removed.");
    //     }else
    //     {
    //         if (next != null)
    //         {
    //             next.SpriteCollision(x1,x2, next);
    //         }
    //     }
    // }
    
}