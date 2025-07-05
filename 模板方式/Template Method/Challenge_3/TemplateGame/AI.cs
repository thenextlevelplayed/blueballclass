using Template_Method.Challenge_3.UNO;

namespace Template_Method.Challenge_3.TemplateGame;

public abstract class AI<TCard> : Player<TCard> where TCard : Card<object, object>
{

    public AI(string name, List<TCard> hand, int point = 0) : base(name, hand, point)
    {
        NameHimSelf(name);
    }
    protected override void NameHimSelf(string name)
    {
        Name = name;
    }
}