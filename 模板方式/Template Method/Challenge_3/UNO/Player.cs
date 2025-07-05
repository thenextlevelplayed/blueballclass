namespace Template_Method.Challenge_3.UNO;

public abstract class Player
{
    public string _name { get; set; }
    public List<Card>? _hand { get; set; } = new List<Card>();

    public Player(string name, List<Card> hand)
    {
        this._name = name;
        this._hand = hand;
    }

    public void AddCard(Card card)
    {
        _hand.Add(card);
    }

    public void RemoveCard(Card card)
    {
        _hand.Remove(card);
    }

    public bool DrawCard(Card card)
    {
        Suit suit = card.Suit;
        Rank rank = card.Rank;
        for (int i = 0; i < _hand.Count; i++)
        {
            if (_hand[i].Rank.Equals(rank))
            {
                return false;
            }
            else if (_hand[i].Suit.Equals(suit))
            {
                return false;
            }
        }
        return true;
    }

    public abstract void NameHimSelf(string? name);

    public abstract List<Card> Decision(Card card);
}