using System.Runtime.InteropServices.JavaScript;

namespace Template_Method.Challenge_3.TemplateGame;

public class ShowdownGame : Game<ShowdownCard>
{
    public ShowdownGame(List<Player<ShowdownCard>> players) :
        base(players)
    {
    
    }

    protected override IEnumerable<object> GetFirstAttributes()
    {
        // return Enum.GetValues(typeof(Showdown_Rank)).Cast<object>();
        return Enum.GetValues(typeof(Showdown_Rank)).Cast<object>();
    }

    protected override IEnumerable<object> GetSecondAttributes()
    {
        return Enum.GetValues(typeof(Showdown_Suit)).Cast<object>();
    }

    protected override ShowdownCard CreateCard(object first, object second)
    {
        return new ShowdownCard((Showdown_Rank)first, (Showdown_Suit)second);
    }

    public override List<ShowdownCard>? Show(
        Player<ShowdownCard> player)
    {
        var cards = player.Decision(null);
        if (cards != null && cards.Count > 0) PlaceCard = cards[0];
        return cards;
    }

    protected override Dictionary<Player<ShowdownCard>, ShowdownCard>? PrintPlayerShowCard()
    {
        var PlayerShowCard = new Dictionary<Player<ShowdownCard>, ShowdownCard>();
        foreach (var player in Players)
        {
            PlayerShowCard.Add(player, Show(player)[0]); //取list中第一章卡
        }

        return PlayerShowCard;
    }


    private void SetPoint()
    {
        Dictionary<Player<ShowdownCard>, ShowdownCard>
            playersCard =
                PrintPlayerShowCard();
        if (!playersCard.Any())
        {
            return;
        }

        var maxEntry = playersCard
            .OrderByDescending(entry => entry.Value.Attribute1)
            .ThenByDescending(entry => entry.Value.Attribute2)
            .First();
        Console.WriteLine($"Player: {maxEntry.Key.Name} has a biggest card => {maxEntry.Value.ToString()}");
        maxEntry.Key.ReceivePoint();
    }

    private void Winner()
    {
        PrintWinner(Players.OrderByDescending(p => p.Point).First());
    }

    protected override int SetDrawCardRound()
    {
        return 13;
    }

    protected override void GameRoundAction()
    {
        for (int i = 0; i < 13; i++)
        {
            //玩家出台
            SetPoint();
        }

        Winner();
    }
}