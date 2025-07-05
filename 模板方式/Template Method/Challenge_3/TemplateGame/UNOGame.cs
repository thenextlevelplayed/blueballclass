using Template_Method.Challenge_3.Showdown;

namespace Template_Method.Challenge_3.TemplateGame;

public class UNOGame : Game<UNOCard>
{
    public UNOGame(List<Player<UNOCard>> players) : base(players)
    {
        
    }

    protected override IEnumerable<object> GetFirstAttributes()
    {
        return Enum.GetValues(typeof(UNO_Color)).Cast<object>();
    }

    protected override IEnumerable<object> GetSecondAttributes()
    {
        return Enum.GetValues(typeof(UNO_Number)).Cast<object>();
    }

    protected override UNOCard CreateCard(object first, object second)
    {
        return new UNOCard((UNO_Number)first, (UNO_Color)second);
    }

    // 覆寫 ShouldReshuffleDeck，檢查牌堆是否為空
    protected override bool ShouldReshuffleDeck()
    {
        return _deck._cards.Count == 0;
    }

    protected override void ProcessPlayedCards(List<UNOCard> dumpCards)
    {
        foreach (var dumpCard in dumpCards)
        {
            _saveDumpCard.AddCard(dumpCard);
        }
    }

    private void UpdatePlacedCard(List<UNOCard> dumpCards)
    {
        PlaceCard = dumpCards.First();
    }

    protected Player<UNOCard> SequencePlayers(Player<UNOCard> p)
    {
        int index = Players.FindIndex(players => players.Equals(p));
        if (index == -1) // 確保找到玩家，否則拋出例外或返回 null
        {
            throw new ArgumentException("Player not found in list.");
        }

        return Players[(index + 1) % Players.Count]; // 使用模運算確保循環
    }

    protected Player<UNOCard>? IsWinner(Player<UNOCard> p)
    {
        if (p.Hand.Count == 0)
        {
            return p;
        }
        else
        {
            return null;
        }
    }

    protected override int SetDrawCardRound()
    {
        return 5;
    }

    protected override void GameRoundAction()
    {
        var dumpCard = _deck.Draw();
        this.PlaceCard = dumpCard;
        Player<UNOCard> currentPlayer = Players[0];
        //玩家開始出牌
        while (true)
        {
            Console.WriteLine($"Current Player: {currentPlayer.Name}");
            if (currentPlayer.DrawCard(PlaceCard))
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
}