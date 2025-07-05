using System.Collections.ObjectModel;

namespace OOP技法練習.OOP技法2_雙向關聯多對多
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

       
        public void setHp(int hp)
        {
            this.hp = (hp < 0 ? throw new Exception("Hp Must be greater than or equal 0") : hp);
            Console.WriteLine($"英雄血量更新至{hp}");
        }

        public void setLevel(int level)
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
        public int getLevel()
        {
            return level;
        }
        public int getTotalExp()
        {
            return totalExp;
        }
        public int getHp()
        {
            return hp;
        }
        public static void Run()
        {
            Console.WriteLine("執行題目 1 的程式碼！");
        }
        //以下都是雙向關係
        private HashSet<Guild> guilds = new HashSet<Guild>();

        public void removeGuild(Guild guild)
        {
            guilds.Remove(guild);
        }

        public void addGuild(Guild guild)
        {
            guilds.Add(guild);
        }

        public ReadOnlyCollection<Guild> getGuilds() //不可以改變
        {
            return guilds.ToList().AsReadOnly();
        }
    }
}