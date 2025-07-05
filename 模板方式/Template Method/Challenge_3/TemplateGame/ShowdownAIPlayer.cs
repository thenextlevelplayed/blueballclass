namespace Template_Method.Challenge_3.TemplateGame;

public class ShowdownAIPlayer : AI<ShowdownCard>
{
    public ShowdownAIPlayer(string name, List<ShowdownCard> hand, int point = 0) : base(name,
        hand, point)
    {
    }

    public override List<ShowdownCard> Decision(ShowdownCard shodownCard)
    {
        if (this.Hand.Count == 0)
        {
            Console.WriteLine("You don't have any card in hand");
            return null;
        }

        while (true)
        {
            int random = new Random().Next(0, this.Hand.Count());

            var removeCard = Hand[random];
            this.RemoveCard(removeCard);
            return new List<ShowdownCard> { removeCard };
        }
    }
}