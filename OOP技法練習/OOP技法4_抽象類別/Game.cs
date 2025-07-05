using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法4_抽象類別
{
    public class Game
    {
        private Player p1;
        private Player p2;

        public Game(Player p1, Player p2)
        {
            if (p1 == p2)
            {
                throw new Exception("Player number must be unique");
            }
            this.p1 = p1;
            this.p2 = p2;
        }

        private static readonly Dictionary<Decision, Decision> counterMap = new Dictionary<Decision, Decision>
        {
            { Decision.SCISSORS, Decision.PAPER },
            { Decision.PAPER, Decision.STONE },
            { Decision.STONE, Decision.SCISSORS }
        };

        // 如果需要不可變性，可以進一步包裝為 ReadOnlyDictionary
        private static readonly ReadOnlyDictionary<Decision, Decision> immutableCounterMap =
            new ReadOnlyDictionary<Decision, Decision>(counterMap);

        public void start()
        {
            Decision p1Decsion = p1.decide();
            Decision p2Decsion = p2.decide();

            Console.WriteLine($"玩家{p1.getNumber()} 出了 {p1Decsion.GetName()}");
            Console.WriteLine($"玩家{p2.getNumber()} 出了 {p2Decsion.GetName()}");

            if (p1Decsion == p2Decsion)
            {
                Console.WriteLine($"平手");
            }
            else if (immutableCounterMap[p1Decsion] == p2Decsion)
            {
                Console.WriteLine($"玩家{p1.getNumber()} 獲勝!");
            }
            else
            {
                Console.WriteLine($"玩家{p2.getNumber()} 獲勝!");
            }

        }
    }
}
