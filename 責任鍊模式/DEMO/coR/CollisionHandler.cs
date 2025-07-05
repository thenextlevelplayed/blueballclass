namespace DEMO.coR;

public abstract class CollisionHandler
{
    protected CollisionHandler? next;

    public CollisionHandler(CollisionHandler? next)
    {
        this.next = next != null ? next : null;
    }

    // public void TemplateSpriteCollision(Sprite x1, Sprite x2, CollisionHandler? collisionHandler)
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

    public void TemplateSpriteCollision(world world, Sprite x1, Sprite x2)
    {
        if (HandleCollisionByType(x1, x2))
        {
            //do something
            CollisionAction(ref world, x1, x2);
        }
        else
        {
            if (next != null)
            {
                next.TemplateSpriteCollision(world, x1, x2);
            }
        }
    }

    protected abstract bool HandleCollisionByType(Sprite x1, Sprite x2);
    // {
    //     return x1.GetType() == Type1 && x2.GetType() == Type2;
    // }

    protected abstract void CollisionAction(ref world world, Sprite x1, Sprite x2);


    // public abstract void SpriteCollision(Sprite x1,Sprite x2,CollisionHandler?  collisionHandler);
}