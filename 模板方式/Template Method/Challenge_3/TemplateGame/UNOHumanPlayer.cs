namespace Template_Method.Challenge_3.TemplateGame;

public class UNOHumanPlayer : Human<UNOCard>
{
    public UNOHumanPlayer(string name, List<UNOCard> hand, int point = 0) : base(name, hand,
        point)
    {
    }

    public override List<UNOCard> Decision(UNOCard unoCard)
    {
        if (this.Hand.Count == 0)
        {
            Console.WriteLine("You don't have any card in hand");
            return null;
        }

        while (true)
        {
            List<UNOCard> colorCards = new List<UNOCard>();
            List<UNOCard> numberCards = new List<UNOCard>();
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Color == unoCard.Color)
                {
                    colorCards.Add(Hand[i]);
                }
                else if (Hand[i].Number == unoCard.Number)
                {
                    numberCards.Add(Hand[i]);
                }
            }

            if (numberCards.Count == 0 && colorCards.Count == 0)
            {
                Console.WriteLine("Oops, you don't have any card pair, you have to draw a card.");
                return null;
            }
            else if (numberCards.Count == 0 && colorCards.Count > 0)
            {
                Console.WriteLine($"You choose the same suit of card.");
                Console.WriteLine($"Please choose the card you would like to be the top:");
                for (int i = 0; i < colorCards.Count(); i++)
                {
                    Console.WriteLine($"{colorCards[i].ToString()} => Index {i}");
                }

                string? indexInput = Console.ReadLine();
                if (!int.TryParse(indexInput, out int index) || index < 0 || index >= colorCards.Count())
                {
                    Console.WriteLine($"Invalid number! Must be between 0 and {colorCards.Count() - 1}.");
                    continue;
                }

                for (int i = 0; i < colorCards.Count(); i++)
                {
                    this.RemoveCard(colorCards[i]);
                }

                var temp = colorCards[0];
                colorCards[0] = colorCards[index];
                colorCards[index] = temp;
                return colorCards;
            }
            else if (numberCards.Count > 0 && colorCards.Count == 0)
            {
                Console.WriteLine($"You choose the same Rank of card.");
                Console.WriteLine($"Please choose the card you would like to be the head:");

                for (int i = 0; i < numberCards.Count(); i++)
                {
                    Console.WriteLine($"{numberCards[i].ToString()} => Index {i}");
                }

                string? indexInput = Console.ReadLine();
                if (!int.TryParse(indexInput, out int index) || index < 0 || index >= numberCards.Count())
                {
                    Console.WriteLine($"Invalid number! Must be between 0 and {numberCards.Count() - 1}.");
                    continue;
                }

                for (int i = 0; i < colorCards.Count(); i++)
                {
                    this.RemoveCard(colorCards[i]);
                }

                var temp = numberCards[0];
                numberCards[0] = numberCards[index];
                numberCards[index] = temp;
                return numberCards;
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
                    for (int i = 0; i < colorCards.Count(); i++)
                    {
                        Console.WriteLine($"{colorCards[i].ToString()} => Index {i}");
                    }

                    string? indexInput = Console.ReadLine();
                    if (!int.TryParse(indexInput, out int index) || index < 0 || index >= colorCards.Count())
                    {
                        Console.WriteLine($"Invalid number! Must be between 1 and {colorCards.Count() - 1}.");
                        continue;
                    }

                    for (int i = 0; i < colorCards.Count(); i++)
                    {
                        this.RemoveCard(colorCards[i]);
                    }

                    var temp = colorCards[0];
                    colorCards[0] = colorCards[index];
                    colorCards[index] = temp;
                    return colorCards;
                }
                else if (result == 2)
                {
                    Console.WriteLine($"You choose the same Rank of card.");
                    Console.WriteLine($"Please choose the card you would like to be the head:");

                    for (int i = 0; i < numberCards.Count(); i++)
                    {
                        Console.WriteLine($"{numberCards[i].ToString()} => Index{i}");
                    }

                    string? indexInput = Console.ReadLine();
                    if (!int.TryParse(indexInput, out int index) || index < 0 || index >= numberCards.Count())
                    {
                        Console.WriteLine($"Invalid number! Must be between 1 and {numberCards.Count() - 1}.");
                        continue;
                    }

                    for (int i = 0; i < colorCards.Count(); i++)
                    {
                        this.RemoveCard(colorCards[i]);
                    }

                    var temp = numberCards[0];
                    numberCards[0] = numberCards[index];
                    numberCards[index] = temp;
                    return numberCards;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}