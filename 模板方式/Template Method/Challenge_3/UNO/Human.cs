namespace Template_Method.Challenge_3.UNO;

public class Human : Player
{
    public Human(string? name, List<Card> hand) : base(null, hand)
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
                Console.WriteLine($"you have {_hand[i].ToString()}");
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
                Console.WriteLine("Oops, you don't have any card pair, you have to draw a card.");
                return null;
            }
            else if (RankCards.Count == 0 && SuitCards.Count > 0)
            {
                Console.WriteLine($"You choose the same suit of card.");
                Console.WriteLine($"Please choose the card you would like to be the top:");
                for (int i = 0; i < SuitCards.Count(); i++)
                {
                    Console.WriteLine($"{SuitCards[i].ToString()} => Index {i}");
                    // Card removeCard = _hand[result];
                    // this.RemoveCard(SuitCards[i]);
                }

                string? indexInput = Console.ReadLine();
                if (!int.TryParse(indexInput, out int index) || index < 0 || index >= SuitCards.Count())
                {
                    Console.WriteLine($"Invalid number! Must be between 0 and {SuitCards.Count() - 1}.");
                    continue;
                }
                for (int i = 0; i < SuitCards.Count(); i++)
                {
                    // Console.WriteLine($"{SuitCards[i].ToString()} => Index {i}");
                    // Card removeCard = _hand[result];
                    this.RemoveCard(SuitCards[i]);
                }

                Card temp = SuitCards[0];
                SuitCards[0] = SuitCards[index];
                SuitCards[index] = temp;
                return SuitCards;
            }
            else if (RankCards.Count > 0 && SuitCards.Count == 0)
            {
                Console.WriteLine($"You choose the same Rank of card.");
                Console.WriteLine($"Please choose the card you would like to be the head:");

                for (int i = 0; i < RankCards.Count(); i++)
                {
                    Console.WriteLine($"{RankCards[i].ToString()} => Index {i}");
                    // Card removeCard = _hand[result];
                    // this.RemoveCard(RankCards[i]);
                }

                string? indexInput = Console.ReadLine();
                if (!int.TryParse(indexInput, out int index) || index < 0 || index >= RankCards.Count())
                {
                    Console.WriteLine($"Invalid number! Must be between 0 and {RankCards.Count() - 1}.");
                    continue;
                }
                for (int i = 0; i < SuitCards.Count(); i++)
                {
                    // Console.WriteLine($"{SuitCards[i].ToString()} => Index {i}");
                    // Card removeCard = _hand[result];
                    this.RemoveCard(SuitCards[i]);
                }

                Card temp = RankCards[0];
                RankCards[0] = RankCards[index];
                RankCards[index] = temp;
                return RankCards;
            }
            else
            {
                // 3種狀況 1 2 同時存在、1存在2不存在、1不存在2存在
                Console.WriteLine("Please choose the card type you would like to play:");
                Console.WriteLine("input number 1 => Choose the same Suit of card");
                Console.WriteLine("input number 2 => Choose the same Rank of card");
                string? typeInput = Console.ReadLine();

                if (string.IsNullOrEmpty(typeInput))
                {
                    Console.WriteLine("Input cannot be null or empty!");
                    continue;
                }

                if (!int.TryParse(typeInput, out int result) || result < 0 || result >= 3)
                {
                    Console.WriteLine($"Invalid number! Must be between 0 or 2.");
                    continue;
                }

                Console.WriteLine($"Please choose the card type you would like to draw:");
                if (result == 1)
                {
                    Console.WriteLine($"You choose the same suit of card.");
                    Console.WriteLine($"Please choose the card you would like to be the top:");
                    for (int i = 0; i < SuitCards.Count(); i++)
                    {
                        Console.WriteLine($"{SuitCards[i].ToString()} => Index {i}");
                        // Card removeCard = _hand[result];
                        // this.RemoveCard(SuitCards[i]);
                    }

                    string? indexInput = Console.ReadLine();
                    if (!int.TryParse(indexInput, out int index) || index < 0 || index >= SuitCards.Count())
                    {
                        Console.WriteLine($"Invalid number! Must be between 1 and {SuitCards.Count() - 1}.");
                        continue;
                    }
                    for (int i = 0; i < SuitCards.Count(); i++)
                    {
                        // Console.WriteLine($"{SuitCards[i].ToString()} => Index {i}");
                        // Card removeCard = _hand[result];
                        this.RemoveCard(SuitCards[i]);
                    }

                    Card temp = SuitCards[0];
                    SuitCards[0] = SuitCards[index];
                    SuitCards[index] = temp;
                    return SuitCards;
                }
                else if (result == 2)
                {
                    Console.WriteLine($"You choose the same Rank of card.");
                    Console.WriteLine($"Please choose the card you would like to be the head:");

                    for (int i = 0; i < RankCards.Count(); i++)
                    {
                        Console.WriteLine($"{RankCards[i].ToString()} => Index{i}");
                        // Card removeCard = _hand[result];
                        // this.RemoveCard(RankCards[i]);
                    }

                    string? indexInput = Console.ReadLine();
                    if (!int.TryParse(indexInput, out int index) || index < 0 || index >= RankCards.Count())
                    {
                        Console.WriteLine($"Invalid number! Must be between 1 and {RankCards.Count() - 1}.");
                        continue;
                    }
                    for (int i = 0; i < SuitCards.Count(); i++)
                    {
                        // Console.WriteLine($"{SuitCards[i].ToString()} => Index {i}");
                        // Card removeCard = _hand[result];
                        this.RemoveCard(SuitCards[i]);
                    }

                    Card temp = RankCards[0];
                    RankCards[0] = RankCards[index];
                    RankCards[index] = temp;
                    return RankCards;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}