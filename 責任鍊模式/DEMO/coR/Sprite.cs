namespace DEMO.coR;

public abstract class Sprite
{
    public int Coordinate { get; set; }
    public world World { get; set; }

    public Sprite(world world)
    {
        this.World = world;
    }

    public void CreateCoordinate()
    {
        var random = new Random();
        int newCoordinate = random.Next(0, 30);

        if (World._world[newCoordinate] == null)
        {
            Coordinate = newCoordinate;
            World._world[Coordinate] = this;
            PrintCoordinate();
        }
        else
        {
            CreateCoordinate();
        }
    }

    // public void Collision(Sprite x2,World world)
    // {
    //     CollisionHandler.SpriteCollision(x2, CollisionHandler,world);
    // }

    // public void Collision(Sprite x2)
    // {
    //     CollisionManager.HandleCollision(this, x2);
    // }


/*
    public void Collision(Sprite x2)
    {
        if (this is Water && x2 is Fire)
        {
            World.RemoveSprite(this);
            World.RemoveSprite(x2);
            World._world[this.Coordinate]= null;
            World._world[x2.Coordinate]= null;
            Console.WriteLine("Water and Fire are removed.");
        }
        else if (this is Water && x2 is Water)
        {
            Console.WriteLine("Movement failed.");
        }
        else if (this is Hero hero && x2 is Fire)
        {
            MoveToX2Coordinate(x2.Coordinate);
            World.RemoveSprite(x2);
            this.DecreaseHp(10);
            Console.WriteLine($"Hero current hp: {this.GetHp()}");
            Console.WriteLine("Hero decreases Hp and Fire is removed.");
            if (this.GetHp() <= 0) //Hero HP 小於等於0 ，從世界移除
            {
                World.RemoveSprite(this);
                World._world[this.Coordinate]= null;
                Console.WriteLine("Hero is dead and removed.");
            }
        }
        else if (this is Hero hero1 && x2 is Water)
        {
            MoveToX2Coordinate(x2.Coordinate);
            this.IncreaseHp(10);
            World.RemoveSprite(x2);
            Console.WriteLine($"Hero current hp: {this.GetHp()}");
            Console.WriteLine("Hero increases Hp and Fire is removed.");
        }
        else if (this is Hero && x2 is Hero)
        {
            Console.WriteLine("Movement failed.");
        }
    }
*/
    public void MoveToX2Coordinate(int newCoordinate)
    {
        Console.WriteLine($"Sprite moved from {Coordinate} to {newCoordinate}");
        World._world[Coordinate] = null; //世界移除原本座標的生命體
        Coordinate = newCoordinate; // 該生命體的座標變成指定座標
        World._world[Coordinate] = this; //世界把指定座標儲存該生命體
    }

    public virtual int GetHp() => throw new NotImplementedException();
    public virtual void IncreaseHp(int number) => throw new NotImplementedException();
    public virtual void DecreaseHp(int number) => throw new NotImplementedException();

    public abstract string ToString();

    public void PrintCoordinate()
    {
        Console.WriteLine($"{ToString()}:Coordinates:{Coordinate}");
    }
}