namespace Big2;

public abstract class Player
{
    public List<Card> HandCards { get; set; } = new List<Card>();
    public String Name { get; set; }
    public int Index { get; set; }

    public bool IsPass { get; set; } = false;

    public Player(int Index)
    {
        this.Index = Index;
        IsPass = false;
    }

    public List<Card>? Play()
    {
        string printIndex = string.Join("    ", Enumerable.Range(0, HandCards.Count));

        Console.WriteLine(printIndex);
        string printCardsInfo = string.Join("", HandCards.Select(c => c.ToString()));
        Console.WriteLine(printCardsInfo);

        HandCards?.ForEach(c => printCardsInfo += c.ToString());
        var cardsInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries); //刪除空白字串
        if (cardsInput.Contains("-1"))
        {
            return null;
        }

        var cards = new List<Card>();
        foreach (var cardIndex in cardsInput)
        {
            cards.Add(HandCards[int.Parse(cardIndex)]);
        }

        return cards.OrderBy(c => c.Rank).ThenBy(c => c.Suit).ToList();
    }

    public void RemoveCards(List<Card> cards)
    {
        HandCards.RemoveAll(c => cards.Any(card => c.Rank.Equals(card.Rank) && c.Suit.Equals(card.Suit)));
    }

    public void Deal(Card card)
    {
        HandCards.Add(card);
    }

    public void NameHimSelf()
    {
        //Console.WriteLine("請輸入你的玩家名字:");
        var getName = GetName();
        if (!string.IsNullOrEmpty(getName))
        {
            this.Name = getName;
        }
        else
        {
            //Console.WriteLine("名字不能為空，請重新輸入！");
            NameHimSelf();
        }
    }

    protected abstract string GetName();
}