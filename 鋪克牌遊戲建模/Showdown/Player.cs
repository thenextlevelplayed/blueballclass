using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Showdown
{
    public abstract class Player
    {
        public string name { get; set; }
        public Hand hand { get; set; }
        public int point { get; private set; } = 0;
        public Player? exchangePartner { get; set; }
        public int countRound { get; set; } = 0;
        private readonly Deck deck;
        public Player(string name, Hand hand, int point, Player? exchangePartner, int countRound, Deck deck)
        {
            this.name = name;
            this.hand = hand;
            this.point = point;
            this.exchangePartner = exchangePartner;
            this.countRound = countRound;
            this.deck = deck;
        }

        private Game game = null!;


        public void DrawCard() //玩家抽牌
        {
            Card card = deck.Draw(); //從牌堆被抽出一張卡
            hand.AddCard(card);//玩家的手牌加入這張卡

        }

        public abstract void TryExchange(List<Player> players);


        public void RequestExchange(Player target) //交換手牌 (Exchange Hands)」 特權
        {
            if (target.exchangePartner != null || countRound > 0)
            {
                Console.WriteLine("You already exchange the card with someone or Partner is already exchanged to someone.");
                return;
            }
            this.exchangePartner = target; //雙向關聯
            target.exchangePartner = this;
            PerformExchange();
            EndExchange();
        }

        public void PerformExchange()
        {
            if (exchangePartner != null & countRound > 0)
            {
                Console.WriteLine("You don't have an exchange partner or you are under exchanging phrase .");
                return;
            }
            if (exchangePartner != null)
            {
                //交換手牌
                Hand temp = hand;
                hand = exchangePartner.hand;
                exchangePartner.hand = temp;
                exchangePartner.countRound = 3;
                this.countRound = 3;
            }
        }

        public void EndExchange()
        {
            if (exchangePartner != null)
            {
                if (countRound > 0)
                {
                    countRound--;
                    if (countRound == 0)
                    {
                        Console.WriteLine($"Player:{name} After 3 rounds return the original hand.");
                        Hand temp = hand;
                        hand = exchangePartner.hand;
                        exchangePartner.hand = temp;
                        Console.WriteLine($"before exchange hand:");
                        foreach (var card in exchangePartner.hand.cards)
                        {
                            Console.WriteLine($"{card.ToString()}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("You don't have an exchange partner.");
            }
        }

        public abstract Card? Show(); //出 (Show) 一張牌（此步驟彼此皆無法知曉彼此出的牌）。


        public int GetPoint() //取得自己的分數
        {
            return this.point;
        }

        public int GivePoint() //給遊戲控制我的分數
        {
            return this.point++;
        }

        public abstract string NameHimSelf(int position = 0);

    }
}
