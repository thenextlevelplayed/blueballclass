using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法4_抽象類別
{
    public class Human : Player
    {
        // Read user input from the console
        private readonly static string? input = Console.ReadLine();
        public Human(int number) : base(number) // super的用法
        {
        }

        public override Decision decide()
        {
            Console.WriteLine("Please 出拳 (1) 剪刀 (2) 拳頭 (3) 布:");

            // Convert the input to an integer
            int.TryParse(input, out int num);

            switch (num)
            {
                case 1:
                    return Decision.SCISSORS;
                case 2:
                    return Decision.STONE;
                case 3:
                    return Decision.PAPER;
                default:
                    Console.WriteLine("只能輸入1~3數字，請再輸入一次");
                    return decide();
            }
        }

    }
}

