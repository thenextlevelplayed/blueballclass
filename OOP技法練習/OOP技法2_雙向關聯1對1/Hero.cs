namespace OOP技法練習.OOP技法2_雙向關聯1對1
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

        //has-a 
        private Pet? pet;

        
        public void setPet(Pet? pet)//養寵物
        {           
            this.pet = pet;

            if (pet != null)//如果有新的寵物
            {
                pet.setOwner(this); //新寵物的主人是這個hero
            }                  
        }

        public void removePet()
        {
            if (this.pet != null)//如果有寵物，都要先解除關係
            {
                this.pet.setOwner(null);
            }
            this.pet = null;
        }

        public Pet getPet()
        {
            return pet;
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

    }
}