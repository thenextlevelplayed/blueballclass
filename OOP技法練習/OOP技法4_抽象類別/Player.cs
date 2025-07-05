using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法4_抽象類別
{
    public abstract class Player
    {
        private int number;

        public Player(int number)
        {
            this.number = number;
        }
        public abstract Decision decide();

        public int getNumber()
        {
            return number;
        }
    }
}
