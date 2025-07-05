namespace Template_Method.Challenge_3.TemplateGame;

public class UNOAIPlayer : AI<UNOCard>
{
    public UNOAIPlayer(string name, List<UNOCard> hand, int point = 0) : base(name, hand,
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
            // var colors = Enum.GetValues(typeof(UNO_Color)).Cast<UNO_Color>();
            // var numbers = Enum.GetValues(typeof(UNO_Number)).Cast<UNO_Number>();
            List<UNOCard> colorCards = new List<UNOCard>();
            List<UNOCard> numberCards = new List<UNOCard>();
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Number == unoCard.Number)
                {
                    numberCards.Add(Hand[i]);
                }
                else if (Hand[i].Color == unoCard.Color)
                {
                    colorCards.Add(Hand[i]);
                }
            }

            if (numberCards.Count == 0 && colorCards.Count == 0)
            {
                return null;
            }
            else if (numberCards.Count == 0 && colorCards.Count > 0)
            {
                for (int i = 0; i < colorCards.Count(); i++)
                {
                    Console.WriteLine($"{colorCards[i].ToString()} => Index{i}");
                    // Card removeCard = _hand[result];
                    this.RemoveCard(colorCards[i]);
                }

                int rand = new Random().Next(0, colorCards.Count());

                var temp = colorCards[0];
                colorCards[0] = colorCards[rand];
                colorCards[rand] = temp;
                return colorCards;
            }
            else if (numberCards.Count > 0 && colorCards.Count == 0)
            {
                Console.WriteLine($"You choose the same Rank of card.");
                Console.WriteLine($"Please choose the card you would like to be the head:");

                for (int i = 0; i < numberCards.Count(); i++)
                {
                    this.RemoveCard(numberCards[i]);
                }

                int rand = new Random().Next(0, numberCards.Count());
                var temp = numberCards[0];
                numberCards[0] = numberCards[rand];
                numberCards[rand] = temp;
                return numberCards;
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
                for (int i = 0; i < colorCards.Count(); i++)
                {
                    Console.WriteLine($"{colorCards[i].ToString()} => Index{i}");
                    // Card removeCard = _hand[result];
                    this.RemoveCard(colorCards[i]);
                }

                random = new Random().Next(0, colorCards.Count());

                var temp = colorCards[0];
                colorCards[0] = colorCards[random];
                colorCards[random] = temp;
                return colorCards;
            }
            else if (random == 2)
            {
                Console.WriteLine($"You choose the same Rank of card.");
                Console.WriteLine($"Please choose the card you would like to be the head:");

                for (int i = 0; i < numberCards.Count(); i++)
                {
                    this.RemoveCard(numberCards[i]);
                }

                random = new Random().Next(0, numberCards.Count());
                var temp = numberCards[0];
                numberCards[0] = numberCards[random];
                numberCards[random] = temp;
                return numberCards;
            }
            else
            {
                continue;
            }
        }
    }
}