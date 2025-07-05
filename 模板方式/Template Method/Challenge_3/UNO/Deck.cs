namespace Template_Method.Challenge_3.UNO;

public class Deck
{
    private Random random = new Random();
    public List<Card>? _cards { get; set; } = new List<Card>();

    public Deck(List<Card>? cards)
    {
        _cards = cards;
    }

    public void CreateDeckWithCards()
    {
        List<Card> cards = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
        _cards =  cards;
    }

    public List<Card> Shuffle() //Fisher–Yates shuffle
    {
        if (_cards == null)
        {
            throw new Exception("牌卡內必須有牌");
        }

        for (int i = this._cards.Count() - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);
            Card temp = _cards[i];
            _cards[i] = _cards[randomIndex];
            _cards[randomIndex] = temp;
            Console.WriteLine(_cards[i].ToString());
        }

        return _cards;
    }

    public Card Draw()
    {
        if (_cards.Count == 0)
        {
            throw new Exception("There is no card inside deck!");
        }

        Card drawCard = _cards[0];
        this._cards.RemoveAt(0);
        return drawCard;
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }
}