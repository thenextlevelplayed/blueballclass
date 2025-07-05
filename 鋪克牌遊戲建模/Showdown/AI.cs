using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showdown
{
    public class AI : Player
    {
        private Random random = new Random();
        public AI(string name, Hand hand, int point, Player? exchangePartner, int countRound, Deck deck) : base(name, hand, point, exchangePartner, countRound, deck)
        {

        }

        public override string NameHimSelf(int position = 0)
        {

            return name = $"P{position}";

        }

        public override Card? Show()
        {
            if (this.hand.cards.Count() > 0)
            {
                int randomIndex = random.Next(0, this.hand.cards.Count());
                Card card = hand.cards[randomIndex];
                this.hand.RemoveCard(card);
                return card;
            }
            else
            {
                Console.WriteLine("You don't have any card in hand");
                return null;
            }
        }
        public override void TryExchange(List<Player> players) //確認是否要使用特權
        {
            int randomIndex = random.Next(0, 2);
            if (this.exchangePartner != null || this.countRound > 0)
            {
                randomIndex = 0;
            }
            if (randomIndex == 1)
            {
                randomIndex = random.Next(0, players.Count);
                Player target = players[randomIndex];
                RequestExchange(target); // 傳入單個 Player
            }
            else
            {
                return;
            }
        }

    }
}


