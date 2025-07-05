using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showdown
{
    public class Hand
    {
        public List<Card> cards { get; private set; }

        public Hand()
        {
            this.cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }
    }
}
