namespace Big2;

public class Card()
{
    public Rank Rank { set; get; }
    public Suit Suit { set; get; }

    public Card(Rank rank, Suit suit) : this()
    {
        this.Rank = rank;
        this.Suit = suit;
    }

    public override string ToString()
    {
        string rankString;
        switch (Rank)
        {
            case Rank.J:
                rankString = "J";
                break;
            case Rank.Q:
                rankString = "Q";
                break;
            case Rank.K:
                rankString = "K";
                break;
            case Rank.A:
                rankString = "A";
                break;
            case Rank.Two:
                rankString = "2";
                break;
            default:
                rankString = ((int)Rank).ToString();
                break;
        }

        return $"{Suit}[{rankString}] ";
    }
    
    public static bool operator <(Card c1, Card c2)
    {
        return c1.Rank < c2.Rank || (c1.Rank == c2.Rank && c1.Suit < c2.Suit);
    }

    public static bool operator >(Card c1, Card c2)
    {
        return c1.Rank > c2.Rank || (c1.Rank == c2.Rank && c1.Suit > c2.Suit);

    }
}