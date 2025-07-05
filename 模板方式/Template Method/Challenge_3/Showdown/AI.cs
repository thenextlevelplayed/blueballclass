namespace Template_Method.Challenge_3.Showdown;

public class AI : Player
{
    public AI(string name, List<Card> hand, int point = 0) : base(name, hand, point)
    {
    }

    private Random random = new Random();

    public override void NameHimSelf(string name)
    {
        _name = name;
    }

    public override Card Decision()
    {
        if (this._hand.Count == 0)
        {
            Console.WriteLine("You don't have any card in hand");
            return null;
        }

        while (true)
        {
            int random = new Random().Next(0, this._hand.Count());

            Card removeCard = _hand[random];
            this.RemoveCard(removeCard);
            return removeCard;
        }
    }
}