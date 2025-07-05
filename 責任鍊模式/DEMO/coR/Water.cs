namespace DEMO.coR;

public class Water(world world) : Sprite(world)
{
    private CollisionHandler CollisionHandler { get; set; }
    
    public override string ToString()
    {
        return $"Water Sprite";
    }

    //這個方法被CollisionManager 提取出來
    // public override void Collision(Sprite x2) 
    // {
    //     CollisionHandler.SpriteCollision(this, x2, new WaterFireCollision(new WaterWaterCollision(null, World), World));
    // }
}