using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showdown
{
    public class Deck
    {
        private List<Card> Cards { get; set; }
        private readonly Random random = new Random();

        public Deck()
        {
            this.Cards = new List<Card>();
        }

        public List<Card> CreateDeck()// 組合52張牌
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Card card = new Card(suit, rank);
                    Cards.Add(card);
                }
            }
            return Cards;
        }

        public List<Card> Shuffle() //Fisher–Yates shuffle
        {
            if (Cards == null)
            {
                throw new Exception("牌卡內必須有牌");
            }
            for (int i = this.Cards.Count() - 1; i > 0; i--)
            {
                int randomIndex = random.Next(0, i + 1);
                Card temp = Cards[i];
                Cards[i] = Cards[randomIndex];
                Cards[randomIndex] = temp;
                Console.WriteLine(Cards[i].ToString());
            }
            return Cards;
        }

        //public List<Card> SetCards(List<Card> cards)
        //{
        //    if (cards == null)
        //    {
        //        throw new Exception("牌卡內必須有牌");
        //    }
        //    return this.Cards = cards;
        //}

        public Card Draw()
        {
            if (Cards.Count == 0)
            {
                throw new Exception("There is no card inside deck!");
            }
            Card drawCard = Cards[0];
            this.Cards.RemoveAt(0);
            return drawCard;
        }
    }
}
