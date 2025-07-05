namespace Template_Method.Challenge_3.TemplateGame;

public abstract class Player<TCard> where TCard : Card<Object, Object>
{
    public string Name { get; set; }
    public List<TCard> Hand = new List<TCard>();
    public int Point { get; set; } = 0;

    public Player(string name, List<TCard> hand, int point = 0)
    {
        this.Name = name;
        this.Hand = hand;
        this.Point = point;
    }

    public void AddCard(TCard card)
    {
        Hand.Add(card);
    }

    public void RemoveCard(TCard card)
    {
        Hand.Remove(card);
    }

    public void ReceivePoint()
    {
        this.Point++;
    }

    public bool DrawCard(TCard card)
    {
        var attribute1 = card.Attribute1;
        var attribute2 = card.Attribute2;
        for (int i = 0; i < Hand.Count; i++)
        {
            if (Hand[i].Attribute1.Equals(attribute1))
            {
                return false;
            }
            else if (Hand[i].Attribute2.Equals(attribute2))
            {
                return false;
            }
        }
        return true;
    }

    protected abstract void NameHimSelf(string? name);

    public abstract List<TCard> Decision(TCard unoCard);
}