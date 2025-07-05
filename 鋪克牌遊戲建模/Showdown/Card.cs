using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showdown
{

    public class Card
    {
        // 屬性：使用外部定義的 Suit 和 Rank 列舉
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        // 建構子：初始化一張牌的花色和階級
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }        

        // （可選）覆寫 ToString 方法，方便顯示牌的內容
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
