namespace DEMO.coR;

public class HeroHeroCollision : CollisionHandler
{
    public HeroHeroCollision(CollisionHandler? next) : base(next)
    {
    }

    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Hero) && x2.GetType() == typeof(Hero);
    }

    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        Console.WriteLine("Movement failed.");
    }
    // public override void SpriteCollision(Sprite x1, Sprite x2, CollisionHandler? collisionHandler)
    // {
    //     if (x1 is Hero && x2 is Hero)
    //     {
    //         Console.WriteLine("Movement failed.");
    //     }else
    //     {
    //         if (next != null)
    //         {
    //             next.SpriteCollision(x1,x2, next);
    //         }
    //     }
    // }
}