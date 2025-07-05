using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法4_抽象類別
{
    public enum Decision
    {
        PAPER,SCISSORS,STONE    
    }
    public static class DecisionExtensions
    {
        private static readonly Dictionary<Decision, string> nameMap = new Dictionary<Decision, string>
    {
        { Decision.PAPER, "布" },
        { Decision.SCISSORS, "剪刀" },
        { Decision.STONE, "石頭" }
    };

        public static string GetName(this Decision decision)
        {
            return nameMap[decision];
        }

        // 可選：模擬 toString 的行為
        public static string ToString(this Decision decision)
        {
            return nameMap[decision];
        }
    }
}
