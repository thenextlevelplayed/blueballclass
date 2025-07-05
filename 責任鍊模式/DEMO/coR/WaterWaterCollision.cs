namespace DEMO.coR;

public class WaterWaterCollision:CollisionHandler
{
    public WaterWaterCollision(CollisionHandler? next) : base(next)
    {
        
    }
    
    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Water) && x2.GetType() == typeof(Water);
    }
    
    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        Console.WriteLine("Movement failed.");
    }
    
    // public override void SpriteCollision(Sprite x1, Sprite x2, CollisionHandler? collisionHandler)
    // {
    //     if (x1 is Water && x2 is Water)
    //     {
    //         Console.WriteLine("Movement failed.");
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