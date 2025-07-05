namespace Template_Method.Challenge_3.TemplateGame;

public class ShowdownCard : Card<object, object>
{
    public ShowdownCard(Showdown_Rank rank, Showdown_Suit suit) : base(rank, suit)
    {
    }

    public Showdown_Rank rank => (Showdown_Rank)Attribute1;
    public Showdown_Suit suit => (Showdown_Suit)Attribute2;
}