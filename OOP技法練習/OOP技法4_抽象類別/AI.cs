using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法4_抽象類別
{
    public class AI : Player
    {
        public AI(int number) : base(number) // super的用法
        {
        }

        public override Decision decide()
        {
            int randomNum = new Random().Next(0, 2);
            switch (randomNum)
            {
                case 0:
                    return Decision.SCISSORS;
                case 1:
                    return Decision.PAPER;
                default:
                    return Decision.STONE;
            }
            throw new NotImplementedException();
        }
    }
}
