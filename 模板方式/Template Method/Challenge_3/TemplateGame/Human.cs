using System.Runtime.InteropServices.JavaScript;

namespace Template_Method.Challenge_3.TemplateGame;

public abstract class Human<TCard> : Player<TCard> where TCard : Card<object, object>
{
    public Human(string name, List<TCard> hand, int point = 0) : base(name, hand, point)
    {
        NameHimSelf(name);
    }


    protected override void NameHimSelf(string? name)
    {
        Console.WriteLine("請輸入你的玩家名字:");
        string nameInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(nameInput))
        {
            Name = nameInput;
        }
        else
        {
            Console.WriteLine("名字不能為空，請重新輸入！");
            NameHimSelf(null);
        }
    }
}