namespace DEMO.coR;

public class WaterFireCollision : CollisionHandler
{
    public WaterFireCollision(CollisionHandler? next) : base(next)
    {
    }

    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Water) && x2.GetType() == typeof(Fire);
    }

    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        world.RemoveSprite(x1);
        world.RemoveSprite(x2);
        Console.WriteLine("Water and Fire are removed.");
    }

    // public override void SpriteCollision(Sprite x1, Sprite x2, CollisionHandler? collisionHandler)
    // {
    //     if (x1 is Water && x2 is Fire)
    //     {
    //         World.RemoveSprite(x1);
    //         World.RemoveSprite(x2);
    //         World._world[x1.Coordinate] = null;
    //         World._world[x2.Coordinate] = null;
    //         Console.WriteLine("Water and Fire are removed.");
    //     }
    //     else
    //     {
    //         if (next != null)
    //         {
    //             next.SpriteCollision(x1,x2, next);
    //         }
    //     }
    // }
}