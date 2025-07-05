namespace DEMO.coR;

public class FireFireCollision(CollisionHandler? next) : CollisionHandler(next)
{
    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Fire) && x2.GetType() == typeof(Fire);
    }

    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        Console.WriteLine("Movement failed.");
    }
    // public override void SpriteCollision(Sprite x1, Sprite x2, CollisionHandler? collisionHandler)
    // {
    //     if (x1 is Fire hero1 && x2 is Fire)
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