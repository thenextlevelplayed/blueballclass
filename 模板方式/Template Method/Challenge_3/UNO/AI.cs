namespace Template_Method.Challenge_3.UNO;

public class AI : Player
{
    public AI(string name, List<Card> hand) : base(name, hand)
    {
    }

    // private Random random = new Random();

    public override void NameHimSelf(string name)
    {
        _name = name;
    }

    public override List<Card>? Decision(Card card)
    {
        if (this._hand.Count == 0)
        {
            Console.WriteLine("You don't have any card in hand");
            return null;
        }

        while (true)
        {
            List<Card> SuitCards = new List<Card>();
            List<Card> RankCards = new List<Card>();
            for (int i = 0; i < _hand.Count; i++)
            {
                if (_hand[i].Rank == card.Rank)
                {
                    RankCards.Add(_hand[i]);
                }
                else if (_hand[i].Suit == card.Suit)
                {
                    SuitCards.Add(_hand[i]);
                }
            }

            if (RankCards.Count == 0 && SuitCards.Count == 0)
            {
                return null;
            }
            else if (RankCards.Count == 0 && SuitCards.Count > 0)
            {
                for (int i = 0; i < SuitCards.Count(); i++)
                {
                    Console.WriteLine($"{SuitCards[i].ToString()} => Index{i}");
                    // Card removeCard = _hand[result];
                    this.RemoveCard(SuitCards[i]);
                }

                int rand = new Random().Next(0, SuitCards.Count());

                Card temp = SuitCards[0];
                SuitCards[0] = SuitCards[rand];
                SuitCards[rand] = temp;
                return SuitCards;
            }
            else if (RankCards.Count > 0 && SuitCards.Count == 0)
            {
                Console.WriteLine($"You choose the same Rank of card.");
                Console.WriteLine($"Please choose the card you would like to be the head:");

                for (int i = 0; i < RankCards.Count(); i++)
                {
                    this.RemoveCard(RankCards[i]);
                }

                int rand = new Random().Next(0, RankCards.Count());
                Card temp = RankCards[0];
                RankCards[0] = RankCards[rand];
                RankCards[rand] = temp;
                return RankCards;
            }

            Console.WriteLine("Please choose the card type you would like to play:");
            Console.WriteLine("input number 1 => Choose the same Suit of card");
            Console.WriteLine("input number 2 => Choose the same Rank of card");
            int random = new Random().Next(0, 3);

            Console.WriteLine($"Please choose the card type you would like to draw:");
            if (random == 1)
            {
                Console.WriteLine($"You choose the same suit of card.");
                Console.WriteLine($"Please choose the card you would like to be the top:");
                for (int i = 0; i < SuitCards.Count(); i++)
                {
                    Console.WriteLine($"{SuitCards[i].ToString()} => Index{i}");
                    // Card removeCard = _hand[result];
                    this.RemoveCard(SuitCards[i]);
                }

                random = new Random().Next(0, SuitCards.Count());

                Card temp = SuitCards[0];
                SuitCards[0] = SuitCards[random];
                SuitCards[random] = temp;
                return SuitCards;
            }
            else if (random == 2)
            {
                Console.WriteLine($"You choose the same Rank of card.");
                Console.WriteLine($"Please choose the card you would like to be the head:");

                for (int i = 0; i < RankCards.Count(); i++)
                {
                    this.RemoveCard(RankCards[i]);
                }

                random = new Random().Next(0, RankCards.Count());
                Card temp = RankCards[0];
                RankCards[0] = RankCards[random];
                RankCards[random] = temp;
                return RankCards;
            }
            else
            {
                continue;
            }
        }
    }
}