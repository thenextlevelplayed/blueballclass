namespace Template_Method.Challenge_3.UNO;

using System.Text.Json;

public class UNO
{
    private Deck _deck { get; set; }
    private Deck _saveDumpCard { get; set; } = new Deck(null);
    private List<Player> _players { get; set; }
    private Card? _placedCard { get; set; }

    // public UNO(Deck deck, Deck SaveDumpCard, List<Player> players, Card placedCard)
    // {
    //     _deck = deck;
    //     _saveDumpCard = SaveDumpCard;
    //     _players = players;
    //     _placedCard = placedCard;
    // }

    public Player SequencePlayers(Player p)
    {
        int index = _players.FindIndex(players => players.Equals(p));
        if (index == -1) // 確保找到玩家，否則拋出例外或返回 null
        {
            throw new ArgumentException("Player not found in list.");
        }

        return _players[(index + 1) % _players.Count]; // 使用模運算確保循環
    }

    public void CreateDeckCard()
    {
        _deck.CreateDeckWithCards();
    }

    public void DrawCard(Player player)
    {
        if (_deck._cards.Count == 0)
        {
            ReShuffleDeck();
        }

        Card card = _deck.Draw();
        player.AddCard(card);
    }

    public List<Card>? Show(Player player)
    {
        //ToDo 我應該把檢查可不可以抽牌先移到show 外面 如果可以 進行show，步行我們走DrawCard(player) 流程


        List<Card>? dumpCards = player.Decision(_placedCard);
        if (dumpCards != null)
        {
            LogToJson(dumpCards);
            foreach (Card dumpCard in dumpCards)
            {
                _saveDumpCard.AddCard(dumpCard);
                Console.WriteLine($"{player._name} deal {dumpCard}");
            }
        }

        return dumpCards;
    }

    private void UpdatePlacedCard(List<Card> dumpCards)
    {
        _placedCard = dumpCards.First();
    }

    private void ReShuffleDeck()
    {
        _saveDumpCard.Shuffle();
        _deck = _saveDumpCard;
        _saveDumpCard = new Deck(null);
    }

    public Player? IsWinner(Player player)
    {
        if (player._hand.Count == 0)
        {
            return player;
        }
        else
        {
            return null;
        }
    }

    public void PrintWinner(Player player)
    {
        Console.WriteLine($"Winner is {player._name}");
    }

    public void StartGame()
    {
        Player p1 = new AI("p1", new List<Card>());
        Player p2 = new AI("p2", new List<Card>());
        Player p3 = new AI("p3", new List<Card>());
        Player p4 = new Human(null, new List<Card>());
        p4.NameHimSelf(null);
        this._players = new List<Player>()
        {
            p1, p2, p3, p4
        };

        //洗牌
        this._deck = new Deck(null);
        _deck.CreateDeckWithCards();
        _deck.Shuffle();
        this._saveDumpCard = new Deck(new List<Card>());
        this._placedCard = null;

        //5回合流程
        for (int i = 0; i < 5; i++)
        {
            //玩家抽牌
            foreach (var player in this._players)
            {
                DrawCard(player);
            }
        }

        this._placedCard = _deck.Draw();
        Player currentPlayer = p1;
        while (true)
        {
            Console.WriteLine($"Current Player: {currentPlayer._name}");
            if (currentPlayer.DrawCard(_placedCard))
            {
                DrawCard(currentPlayer);
            }
            else
            {
                UpdatePlacedCard(Show(currentPlayer));
                if (IsWinner(currentPlayer) != null)
                {
                    PrintWinner(currentPlayer);
                    break;
                }
            }
            currentPlayer = SequencePlayers(currentPlayer);
        }
    }

    public static void LogToJson(object data)
    {
        // Serialize the input data to JSON
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            WriteIndented = true // Optional: makes JSON human-readable
        });

        // Output JSON to console
        Console.WriteLine(json);
    }
}