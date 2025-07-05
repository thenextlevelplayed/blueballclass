namespace Template_Method.Challenge_3.TemplateGame;

public class ShowdownHumanPlayer : Human<ShowdownCard>
{
    public ShowdownHumanPlayer(string name, List< ShowdownCard> hand, int point = 0) : base(name,
        hand, point)
    {
    }

    public override List< ShowdownCard> Decision( ShowdownCard unoCard)
    {
        if (this.Hand.Count == 0)
        {
            Console.WriteLine("You don't have any card in hand");
            return null;
        }

        while (true)
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                Console.WriteLine($"{Hand[i].ToString()}  index => {i}");
            }

            Console.WriteLine("Please pick the index, it indicates the suit and rank of card:");
            string? cardInput = Console.ReadLine();

            if (string.IsNullOrEmpty(cardInput))
            {
                Console.WriteLine("Input cannot be null or empty!");
                continue;
            }

            if (!int.TryParse(cardInput, out int cardIndex) || cardIndex < 0 || cardIndex >= Hand.Count)
            {
                Console.WriteLine($"Invalid index! Must be between 0 and {Hand.Count - 1}.");
                continue;
            }

            var removeCard = Hand[cardIndex];
            this.RemoveCard(removeCard);
            return new List< ShowdownCard> { removeCard };

        }
    }
}