using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法2_雙向關聯多對多
{
    public class Guild
    {
        private readonly string name;

        private HashSet<Hero> members = new HashSet<Hero>(); //1~10位成員 所以用List

        public Guild(string name, List<Hero> members)
        {
            this.name = name;
            if (members.Count() > 10 || members.Count() < 1)
            {
                throw new Exception("Num of members must be within 1~10.");
            }
            foreach (var hero in members) //雙向關係
            {
                hero.addGuild(this);
            }
            this.members = new HashSet<Hero>(members);
        }

        public void join(Hero member)
        {
            if (members.Count() > 10) //侷限 1~10位
            {
                throw new Exception("Cannot join guild due to member number max is 10");
            }

            if (members.Contains(member)) //不可以重複加入
            {
                throw new Exception("Cannot join the same guild");
            }
            members.Add(member);           
        }

        public void leave(Hero member)
        {
            if (!members.Contains(member))
            {
                throw new Exception("Only guild member can leave.");
            }
            if (members.Count() == 1)
            {
                throw new Exception("The only member cannot leave guild.");
            }
            members.Remove(member);
            member.removeGuild(this);//雙向關係
        }

        public ReadOnlyCollection<Hero> GetMembers() //由於我們有限制1~10位 ，使用pulic有可能讓外部打破這個規則，我們要使用不可修改的型別去定義
        {
            return members.ToList().AsReadOnly();
        }

        public string getName()
        {
            return name;
        }
        //如果想把guild改成單向關聯，hero不能存取guild的話，將hero類別內有關guild的程式碼都刪掉，guild這邊有建立雙向關聯的程式碼也刪掉
    }
}
