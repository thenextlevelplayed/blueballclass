namespace Template_Method.Challenge_3.TemplateGame;

public abstract class Deck<TCard> where TCard : Card<object, object>
{
    private Random random = new Random();
    public List<TCard> _cards; // Changed to protected for better encapsulation

    public Deck(List<TCard> cards)
    {
        _cards = cards ?? throw new ArgumentNullException(nameof(cards));
    }

    // 提供屬性來訪問牌組
    public List<TCard> Cards => _cards;

    public void CreateDeckWithCards<T1, T2>(IEnumerable<T1> values1, IEnumerable<T2> values2) where T1 : class where T2 : class
    {
        List<TCard> cards = new List<TCard>();
        foreach (var value1 in values1)
        {
            foreach (var value2 in values2)
            {
                var card = Activator.CreateInstance(typeof(TCard), value1, value2);
                cards.Add((TCard)card);
            }
        }

        _cards = cards;
    }

    public List<TCard> Shuffle() // Fisher–Yates shuffle
    {
        if (_cards == null || _cards.Count == 0)
        {
            throw new InvalidOperationException("牌卡內必須有牌");
        }

        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);
            TCard temp = _cards[i];
            _cards[i] = _cards[randomIndex];
            _cards[randomIndex] = temp;
            Console.WriteLine(_cards[i].ToString());
        }

        return _cards;
    }

    public TCard Draw()
    {
        if (_cards == null || _cards.Count == 0)
        {
            throw new InvalidOperationException("There is no card inside deck!");
        }

        TCard drawCard = _cards[0];
        _cards.RemoveAt(0);
        return drawCard;
    }

    public abstract void AddCard(TCard card);
}
