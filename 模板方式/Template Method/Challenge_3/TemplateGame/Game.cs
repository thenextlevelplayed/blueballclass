using System.Runtime.InteropServices.JavaScript;
using Template_Method.Challenge_3.TemplateGame;

namespace Template_Method.Challenge_3.TemplateGame;

public abstract class Game<TCard> : ITempleteGame<TCard> where TCard : Card<object, object>
{
    public TCard PlaceCard { get; set; } // 使用泛型 TCard
    public Deck<TCard> _deck { get; set; } = new StandardDeck<TCard>();
    public Deck<TCard> _saveDumpCard { get; set; } = new StandardDeck<TCard>();
    public List<Player<TCard>> Players { get; set; }

    protected Game(List<Player<TCard>> players)
    {
        Players = players ?? throw new ArgumentNullException(nameof(players)); // 防範 null
        _deck._cards = TemplateCreateDeck();
        _deck.Shuffle();
    }

    protected List<TCard> TemplateCreateDeck()
    {
        var firstAttributes = GetFirstAttributes();
        var secondAttributes = GetSecondAttributes();
        var cards = new List<TCard>();

        foreach (var first in firstAttributes)
        {
            foreach (var second in secondAttributes)
            {
                cards.Add(CreateCard(first, second));
            }
        }

        return cards;
    }

    protected abstract IEnumerable<object> GetFirstAttributes();
    protected abstract IEnumerable<object> GetSecondAttributes();
    protected abstract TCard CreateCard(object first, object second);

    public virtual void DrawCard(Player<TCard> player)
    {
        if (ShouldReshuffleDeck())
        {
            ReShuffleDeck();
        }

        player.AddCard(_deck.Draw());
    }

    protected virtual bool ShouldReshuffleDeck()
    {
        return false; // 預設不需要洗牌
    }

    protected void ReShuffleDeck()
    {
        _saveDumpCard.Shuffle();
        _deck = _saveDumpCard;
        _saveDumpCard.Cards.Clear();
        Console.WriteLine("Deck reshuffled from discard pile.");
    }

    public virtual List<TCard> Show(Player<TCard> player)
    {
        var dumpCards = player.Decision(GetCurrentPlaceCard());
        if (dumpCards != null && dumpCards.Count > 0)
        {
            ProcessPlayedCards(dumpCards);
            PlaceCard = dumpCards[dumpCards.Count - 1]; // 更新場上的牌為最後一張
        }

        return dumpCards;
    }

    // 虛擬方法：獲取當前牌，子類別可覆寫
    protected virtual TCard? GetCurrentPlaceCard()
    {
        return PlaceCard; // 預設返回場上的牌
    }

    protected virtual void ProcessPlayedCards(List<TCard> dumpCards)
    {
    }

    //獲取玩家出的牌
    protected virtual Dictionary<Player<TCard>, TCard>? PrintPlayerShowCard()
    {
        return null;
    }

    public void PrintWinner(Player<TCard> player)
    {
        Console.WriteLine($"Winner is {player.Name}");
    }

    public void TemplateGameStart()
    {
        for (int i = 0; i < SetDrawCardRound(); i++)
        {
            //玩家抽牌
            foreach (var player in this.Players)
            {
                DrawCard(player);
            }
        }

        GameRoundAction();
    }

    protected abstract int SetDrawCardRound();

    protected abstract void GameRoundAction();
}