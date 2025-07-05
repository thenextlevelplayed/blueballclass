namespace DEMO.coR;

public class FireWaterCollision(CollisionHandler? next):CollisionHandler(next)
{
    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Fire) && x2.GetType() == typeof(Water);
    }

    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        world.RemoveSprite(x1);
        world.RemoveSprite(x2);
        Console.WriteLine("Fire and Water are removed.");
    }
}