namespace DEMO.coR;

public class world
{
    public Sprite[] _world { get; set; }
    public List<Sprite> _sprites { get; set; } = new List<Sprite>();

    private CollisionHandler _collisionHandler;

    public world( CollisionHandler collisionHandler, int len = 30)
    {
        _world = new Sprite[len];
        this._collisionHandler = collisionHandler;
    }

    public void AddNewSprite(Sprite sprite)
    {
        _sprites.Add(sprite);
        sprite.CreateCoordinate();
    }

    public void RemoveSprite(Sprite sprite)
    {
        _sprites.Remove(sprite);
        this._world[sprite.Coordinate] = null;
    }

    public void MoveSpriteToCoordinate(Sprite x1, int targetCoordinate)
    {
        if (_world[targetCoordinate] == null)
        {
            _world[x1.Coordinate] = null;
            x1.Coordinate = targetCoordinate;
            _world[targetCoordinate] = x1;
        }
    }

    public void GameStart()
    {
        while (true)
        {
            var decision = Decision();
            var x1 = decision.Item1;
            var moveToCoordinate = decision.Item2;
            if (IsLocationOccupied(moveToCoordinate))
            {
                var x2 = this._world[moveToCoordinate];
                Console.WriteLine(x1.ToString());
                HandleCollision( this,x1, x2);
            }
            else
            {
                MoveSpriteToCoordinate(x1, moveToCoordinate);
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
    
    public void HandleCollision(world world,Sprite x1, Sprite x2)
    {
        _collisionHandler.TemplateSpriteCollision(world,x1, x2);
    }
    
    public bool IsLocationOccupied(int coordinate)
    {
        return this._world[coordinate] == null ? false : true;
    }
    
    public Tuple<Sprite, int> Decision()
    {
        Console.WriteLine("Please enter two numbers, separated by a space:");
        string? input = Console.ReadLine();
        List<string> saveInt = input.Split(' ').ToList();

        if (saveInt.Count != 2)
        {
            Console.WriteLine("Invalid input. Please enter exactly two numbers.");
            return Decision();
        }

        if (int.TryParse(saveInt[0], out int number1) && int.TryParse(saveInt[1], out int number2))
        {
            var selectedSprite = _sprites.FirstOrDefault(coord => coord.Coordinate == number1);
            if (selectedSprite != null)
            {
                var decision = new Tuple<Sprite, int>(selectedSprite, number2);
                return decision;
            }
            else
            {
                Console.WriteLine("One of coordinate is empty, please try again.");
                return Decision();
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter valid numbers.");
            return Decision();
        }
    }
}