namespace DEMO.Prototype;

public class World
{
    public Sprite[] _world { get; set; }
    private List<Sprite> _sprites { get; set; }
    public Random random { get; set; } =  new Random();

    private User User { get; set; }

    public World(int len = 30)
    {
        _world = new Sprite[len];
        _sprites = new List<Sprite>();
        User = new User(_sprites);
        CreateSprites();
    }

    public void CreateSprites()
    {
        for (int i = 0; i < 10; i++)
        {
            int random = new Random().Next(0, 3);
            switch (random)
            {
                case 0:
                    _sprites.Add(new Hero(this));
                    break;
                case 1:
                    _sprites.Add(new Water(this));
                    break;
                case 2:
                    _sprites.Add(new Fire(this));
                    break;
            }
        }
    }

    public void AddSprits()
    {
        foreach (Sprite sprite in _sprites)
        {
            _world[sprite.Coordinate] = sprite;
        }
    }

    public void RemoveSprite(Sprite sprite)
    {
        _sprites.Remove(sprite);
    }

    public void GameStart()
    {
        while (_sprites.Any())
        {
            var decision = User.Decision();
            var x1 = decision.Item1;
            var x2 = decision.Item2;
            var isCollision = _world[x1.Coordinate].IsCollision(x2);
            if (isCollision)
            {
                Console.WriteLine(x1.ToString());
                x1.Collision(_world[x2]);
            }
            else
            {
                x1.MoveToX2Coordinate(x2);
            }
            Console.WriteLine("Refresh ALlSpritesCoordinates:");
            PrintALlSpritesCoordinates();
        }
    }

    public void PrintALlSpritesCoordinates()
    {
        foreach (var sprite in _sprites)
        {
            sprite.PrintCoordinate();
        }
    }
}