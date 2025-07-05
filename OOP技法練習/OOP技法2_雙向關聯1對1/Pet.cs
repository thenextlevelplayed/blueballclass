using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法2_雙向關聯1對1
{
    public class Pet
    {
        private readonly string name;
        private Hero? owner;

        public Pet(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void setOwner(Hero? owner)
        {           
            this.owner = owner;
            //throw new NotImplementedException();
        }

        public Hero? getOwner()
        {
            return owner;
        }

        public void eat(Fruit fruit)
        {
            Console.WriteLine("吃水果");
            if (owner != null)
            {
                owner.setHp(owner.getHp() + 10);
            }           
        }
    }
}