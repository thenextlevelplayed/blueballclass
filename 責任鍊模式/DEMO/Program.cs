// See https://aka.ms/new-console-template for more information

using DEMO.coR;

Console.WriteLine("Hello, World!");

var world = new DEMO.coR.world(
    new FireFireCollision(new FireHeroCollision(new FireWaterCollision(new HeroFireCollision(
        new HeroHeroCollision(
            new HeroWaterCollision(new WaterFireCollision(new WaterHeroCollision(new WaterWaterCollision(null))))))))),
    30);
var sprites = CreateSprites(world);
// foreach (var sprite in sprites)
// {
//     world.AddNewSprite(sprite);
// }
sprites.ForEach(s =>world.AddNewSprite(s));
world.GameStart();

List<Sprite> CreateSprites(world world)
{
    var _sprites = new List<Sprite>();
    for (int i = 0; i < 10; i++)
    {
        int random = new Random().Next(0, 3);
        switch (random)
        {
            case 0:
                _sprites.Add(new Hero(world));
                break;
            case 1:
                _sprites.Add(new Water(world));
                break;
            case 2:
                _sprites.Add(new Fire(world));
                break;
        }
    }

    return _sprites;
}