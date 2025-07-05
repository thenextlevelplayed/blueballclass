namespace DEMO.coR;

public class Hero(world world) : Sprite(world)
{
    private int hp { get; set; } = 30;

    public override void IncreaseHp(int number)
    {
        hp+=number;
    }
    
    public override void DecreaseHp(int number)
    {
        hp-=number;
    }
    
    public override string ToString()
    {
        return $"Hero Sprite";
    }

    public override int GetHp() => hp;
    
    // public override void Collision(Sprite x2)
    // {
    //     CollisionHandler.SpriteCollision(this, x2, new WaterFireCollision(new WaterWaterCollision(null, World), World));
    // }
}