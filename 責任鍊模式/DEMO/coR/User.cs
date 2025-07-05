/*
namespace DEMO.coR
{
    public class User
    {
        private List<Sprite> Sprites { get; set; }

        public User(List<Sprite> sprites)
        {
            Sprites = sprites;
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
                var selectedSprite = Sprites.Where(coord => coord.Coordinate == number1).FirstOrDefault();
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
}
*/