using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP技法練習.OOP技法1_依賴;


namespace OOP技法練習.OOP技法1_依賴
{
    public class LevelSheet
    {
        public int queryLevel(int totalExp)
        {
            return totalExp / 1000 + 1;
        }
    }
}
