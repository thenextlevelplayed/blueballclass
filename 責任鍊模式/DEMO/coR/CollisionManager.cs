/*
namespace DEMO.coR;

public class CollisionManager
{
    private CollisionHandler _collisionHandler;

    public CollisionManager(World world)
    {
        _collisionHandler =
            new WaterFireCollision(
                new WaterWaterCollision(
                    new HeroFireCollision(
                        new HeroWaterCollision(
                            new HeroHeroCollision(new FireFireCollision(null, world), world), world), world), world),
                world);
    }

    public void HandleCollision(Sprite x1, Sprite x2)
    {
        _collisionHandler.TemplateSpriteCollision(x1, x2, _collisionHandler);
    }
}
*/