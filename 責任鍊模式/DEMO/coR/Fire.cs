namespace DEMO.coR;

public class Fire(world world) : Sprite(world)
{
    // private CollisionHandler CollisionHandler { get; set; }

    public override string ToString()
    {
        return $"Fire Sprite";
    }

    // public override void Collision(Sprite x2)
    // {
    //     CollisionHandler.SpriteCollision(this, x2, new FireFireCollision(null,World));
    // }
}