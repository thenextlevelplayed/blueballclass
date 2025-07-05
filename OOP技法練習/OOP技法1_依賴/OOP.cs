using OOP技法練習.OOP技法1_依賴;

namespace OOP技法練習.OOP技法1_依賴
{
    public class Hero
    {
        private int level = 1;
        private int totalExp = 0;
        private int hp = 100;

        public Hero()
        {

        }

        public Hero(int level, int totalExp, int hp) //建構子
        {
            this.level = level; //TODO
            setTotalExp(totalExp);
            setHp(hp);
        }

        private void setHp(int hp)
        {
            if (hp < 0)
            {
                throw new Exception("Hp Must be greater than or equal 0");
            }
            this.hp = hp;
        }

        private void setLevel(int level)
        {
            if (level < 0)
            {
                throw new Exception("Level Must be greater than or equal 0");
            }
            this.level = level;
        }

        private void setTotalExp(int totalExp)
        {
            if (totalExp < 0)
            {
                throw new Exception("TotalExp Must be greater than or equal 0");
            }
            this.totalExp = totalExp;
        }

        public void GainExp(int exp, LevelSheet levelSheet)
        {
            if (exp < 0)
            {
                throw new Exception("GainExp Must be greater than or equal 0");
            }
            int currentLevel = level;
            //totalExp += exp;
            //level = levelSheet.queryLevel(totalExp);
            setTotalExp(totalExp += exp);
            setLevel(levelSheet.queryLevel(totalExp));
            Console.WriteLine($"英雄目前等級{currentLevel} ，獲得{exp} ，最新總共獲得經驗值為{totalExp} ， 最新等級為{level}");
        }

        public int Level { get; set; }
        public int TotalExp { get; set; }
        public int Hp { get; set; }

        public static void Run()
        {
            Console.WriteLine("執行題目 1 的程式碼！");
        }

    }
}