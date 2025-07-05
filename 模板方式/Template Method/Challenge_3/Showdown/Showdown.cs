namespace Template_Method.Challenge_3.Showdown;

public class Showdown
{
    private Deck _deck;
    private List<Player> _players;


    // public Showdown(Deck deck, List<Player> players)
    // {
    //     _deck = deck;
    //     _players = players;
    // }

    public void DrawCard(Player player)
    {
        player.AddCard(_deck.Draw());
    }

    public Card Show(Player player)
    {
        return player.Decision();
    }

    public Dictionary<Player, Card> PrintPlayerShowCard()
    {
        Dictionary<Player, Card> PlayerShowCard = new Dictionary<Player, Card>();
        foreach (var player in _players)
        {
            PlayerShowCard.Add(player, Show(player));
        }

        return PlayerShowCard;
    }

    public void SetPoint()
    {
        Dictionary<Player, Card> playersCard = PrintPlayerShowCard();
        if (!playersCard.Any())
        {
            return;
        }

        var maxEntry = playersCard
            .OrderByDescending(entry => entry.Value.UnoNumber)
            .ThenByDescending(entry => entry.Value.UnoColor)
            .First();
        Console.WriteLine($"Player: {maxEntry.Key._name} has a biggest card => {maxEntry.Value.ToString()}");
        maxEntry.Key.ReceivePoint();
    }

    public void Winner()
    {
        PrintWinner(_players.OrderByDescending(p => p._point).First());
    }

    public void PrintWinner(Player player)
    {
        Console.WriteLine($"Winner is {player._name}");
    }

    public void StartGame()
    {
        Player p1 = new AI("p1", new List<Card>(), 0);
        Player p2 = new AI("p2", new List<Card>(), 0);
        Player p3 = new AI("p3", new List<Card>(), 0);
        Player p4 = new Human(null,new List<Card>(), 0);
        p4.NameHimSelf(null);
        this._players = new List<Player>()
        {
            p1, p2, p3, p4
        };

        //洗牌
        this._deck = new Deck();
        _deck.Shuffle();

        //13回合流程
        for (int i = 0; i < 13; i++)
        {
            //玩家抽牌
            foreach (var player in this._players)
            {
                DrawCard(player);
            }
        }

        for (int i = 0; i < 13; i++)
        {
            //玩家出台
            SetPoint();
        }

        Winner();
    }
}