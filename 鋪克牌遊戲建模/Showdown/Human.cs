using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showdown
{
    public class Human : Player
    {

        public Human(string name, Hand hand, int point, Player? exchangePartner, int countRound, Deck deck) : base(name, hand, point, exchangePartner, countRound, deck)
        {
        }

        public override string NameHimSelf(int position = 0)
        {
            Console.WriteLine("請輸入你的玩家名字:");
            string? nameInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(nameInput))
            {
                return name = nameInput;
            }
            Console.WriteLine("名字不能為空，請重新輸入！");
            return NameHimSelf();
        }

        public override Card? Show()//出牌
        {
            if (this.hand.cards.Count == 0)
            {
                Console.WriteLine("You don't have any card in hand");
                return null;
            }

            while (true)
            {
                for (int i = 0; i < hand.cards.Count; i++)
                {
                    Console.WriteLine($"{hand.cards[i].ToString()}  index => {i}");
                }

                Console.WriteLine("Please pick the index, it indicates the suit and rank of card:");
                string? cardInput = Console.ReadLine();

                if (string.IsNullOrEmpty(cardInput))
                {
                    Console.WriteLine("Input cannot be null or empty!");
                    continue;
                }

                if (!int.TryParse(cardInput, out int cardIndex) || cardIndex < 0 || cardIndex >= hand.cards.Count)
                {
                    Console.WriteLine($"Invalid index! Must be between 0 and {hand.cards.Count - 1}.");
                    continue;
                }

                Card removeCard = hand.cards[cardIndex];
                this.hand.RemoveCard(removeCard);
                return removeCard;
            }
        }

        public override void TryExchange(List<Player> players) //確認是否要使用特權
        {
            while (true)
            {
                Console.WriteLine("Do you want to exchange cards with someone? Please enter 'y' or 'n':");
                string? tryExchangeInput = Console.ReadLine()?.ToLower();

                if (string.IsNullOrEmpty(tryExchangeInput))
                {
                    Console.WriteLine("Input cannot be empty. Please enter 'y' or 'n'.");
                    continue;
                }

                if (tryExchangeInput == "n")
                {
                    return; // Exit if user doesn't want to exchange
                }

                if (tryExchangeInput == "y")
                {
                    while (true)
                    {
                        Console.WriteLine("Select the player you want to exchange with:");
                        Console.WriteLine("Available players:");
                        foreach (var player in players)
                        {
                            if (player != this)
                            {
                                Console.WriteLine($"{player.name}");
                            }
                        }

                        string? selectPlayer = Console.ReadLine();
                        if (string.IsNullOrEmpty(selectPlayer))
                        {
                            Console.WriteLine("Player name cannot be empty. Please try again.");
                            continue;
                        }

                        if (selectPlayer == this.name)
                        {
                            Console.WriteLine("You can't exchange hands with yourself. Please choose another player.");
                            continue;
                        }

                        var target = players.FirstOrDefault(p => p.name == selectPlayer);
                        if (target == null)
                        {
                            Console.WriteLine("Player not found. Please enter a correct player name.");
                            continue;
                        }
                       
                        RequestExchange(target);
                        break;
                    }
                    break;
                }

                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }
    }
}