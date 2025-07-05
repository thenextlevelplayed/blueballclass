namespace Template_Method.Challenge_3.Showdown;

public class Human : Player
{
    public Human(string? name, List<Card> hand, int point = 0) : base(null, hand, point)
    {
    }

    public override void NameHimSelf(string? name)
    {
        Console.WriteLine("請輸入你的玩家名字:");
        string nameInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(nameInput))
        {
            _name = nameInput;
        }
        else
        {
            Console.WriteLine("名字不能為空，請重新輸入！");
            NameHimSelf(null);
        }
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
            for (int i = 0; i < _hand.Count; i++)
            {
                Console.WriteLine($"{_hand[i].ToString()}  index => {i}");
            }

            Console.WriteLine("Please pick the index, it indicates the suit and rank of card:");
            string? cardInput = Console.ReadLine();

            if (string.IsNullOrEmpty(cardInput))
            {
                Console.WriteLine("Input cannot be null or empty!");
                continue;
            }

            if (!int.TryParse(cardInput, out int cardIndex) || cardIndex < 0 || cardIndex >= _hand.Count)
            {
                Console.WriteLine($"Invalid index! Must be between 0 and {_hand.Count - 1}.");
                continue;
            }

            Card removeCard = _hand[cardIndex];
            this.RemoveCard(removeCard);
            return removeCard;
        }
    }
}