namespace DEMO.coR;

public class FireHeroCollision(CollisionHandler? next):CollisionHandler(next)
{
    protected override bool HandleCollisionByType(Sprite x1, Sprite x2)
    {
        return x1.GetType() == typeof(Fire) && x2.GetType() == typeof(Hero);
    }

    protected override void CollisionAction(ref world world,Sprite x1, Sprite x2)
    {
        world.RemoveSprite(x1); //移除fire
        // world._world[x1.Coordinate] = null; //清除fire在世界位置
        x2.DecreaseHp(10);
        Console.WriteLine($"Hero current hp: {x2.GetHp()}");
        Console.WriteLine("Hero decreases Hp and Fire is removed.");
        if (x2.GetHp() <= 0) //Hero HP 小於等於0 ，從世界移除
        {
            world.RemoveSprite(x2);
            world._world[x2.Coordinate] = null;
            Console.WriteLine("Hero is dead and removed.");
        }
    }
}