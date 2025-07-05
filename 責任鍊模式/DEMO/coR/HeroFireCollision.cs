namespace DEMO.coR;

public class HeroFireCollision : CollisionHandler
{
    public HeroFireCollision(CollisionHandler? next) : base(next)
    {
    }

    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Hero)  && x2.GetType() == typeof(Fire);
    }

    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        world.RemoveSprite(x2); //移除fire
        x1.DecreaseHp(10);
        Console.WriteLine($"Hero current hp: {x1.GetHp()}");
        Console.WriteLine("Hero decreases Hp and Fire is removed.");
        if (x1.GetHp() <= 0) //Hero HP 小於等於0 ，從世界移除
        {
            world.RemoveSprite(x1);
            world._world[x1.Coordinate] = null;
            Console.WriteLine("Hero is dead and removed.");
        }
        else
        {
            world.MoveSpriteToCoordinate(x1,x2.Coordinate);
        }
    }

    // public override void SpriteCollision(Sprite x1, Sprite x2, CollisionHandler? collisionHandler)
    // {
    //         World.RemoveSprite(x2); //移除fire
    //         World._world[x2.Coordinate] = null; //清除fire在世界位置
    //         x1.DecreaseHp(10);
    //         Console.WriteLine($"Hero current hp: {x1.GetHp()}");
    //         Console.WriteLine("Hero decreases Hp and Fire is removed.");
    //         if (x1.GetHp() <= 0) //Hero HP 小於等於0 ，從世界移除
    //         {
    //             World.RemoveSprite(x1);
    //             World._world[x1.Coordinate] = null;
    //             Console.WriteLine("Hero is dead and removed.");
    //         }
    //         else
    //         {
    //             x1.MoveToX2Coordinate(x2.Coordinate);
    //         }
    // }
}