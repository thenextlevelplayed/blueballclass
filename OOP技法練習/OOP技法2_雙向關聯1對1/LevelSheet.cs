using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法2_雙向關聯1對1
{
    public class LevelSheet
    {
        public int queryLevel(int totalExp)
        {
            return totalExp / 1000 + 1;
        }
    }
}
