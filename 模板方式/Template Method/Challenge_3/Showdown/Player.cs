namespace Template_Method.Challenge_3.Showdown;

public abstract class Player
{
    public string _name { get; set; }
    protected List<Card> _hand = new List<Card>();
    public int _point { get; set; } = 0;

    public Player(string name, List<Card> hand,int  point=0)
    {
        this._name = name;
        this._hand = hand;
        this._point = point;
    }

    public void AddCard(Card card)
    {
        _hand.Add(card);
    }

    public void ReceivePoint()
    {
        _point++;
    }

    public void RemoveCard(Card card)
    {
        _hand.Remove(card);
    }

    public abstract void NameHimSelf(string? name);

    public abstract Card Decision();
}