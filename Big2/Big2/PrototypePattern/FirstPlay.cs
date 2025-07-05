/*
namespace Big2;

public class FirstPlay(
    List<Player> players,
    Player player,
    Dictionary<CardPattern, Card> topPlay,
    CardPattern cardPattern,
    Player topPlayer,
    RoundAction next)
    : RoundAction(players, player, topPlay, cardPattern, topPlayer, next)
{
    public override void DistinguishingGameAndPlayerStatus()
    {
        if (topPlayer == null && topPlay == null && player==null) //從梅花三開始
        {
            Console.Write("新的回合開始了。");
            //first player
            var p = Players
                .Where(c => c.HandCards.Any(card => card.Rank.Equals(Rank.Three) && card.Suit.Equals(Suit.C)))
                .First();
            Console.WriteLine($"輪到{p.Name}了");
            var play = p.Play();
            while (play == null ||
                   !play.Any(card => card.Rank.Equals(Rank.Three) && card.Suit.Equals(Suit.C))) //不能為null，且一定要出梅花３
            {
                play = p.Play();
            }

            var topCard = play.Last();
            var pattern = Handle(play); //check cardpattern
            this.TopPlay = new Dictionary<CardPattern, Card>();
            TopPlay.Add(pattern, topCard);
            TopPlayer = p;
            p.RemoveCards(play);
            string printCardsInfo = "";
            play.ForEach(c => printCardsInfo += c.ToString());
            Console.WriteLine($"玩家 {p.Name} 打出了 {pattern.Name} {printCardsInfo}");
        }
        else if (topPlayer != null && topPlay == null)
        {
            var p = topPlayer;
            Console.WriteLine($"輪到{p.Name}了");
            var play = p.Play();
            while (play == null)
            {
                play = p.Play();
            }

            var topCard = play.Last();
            var pattern = Handle(play); //check cardpattern
            this.TopPlay = new Dictionary<CardPattern, Card>();
            TopPlay.Add(pattern, topCard);
            TopPlayer = p;
            p.RemoveCards(play);
            string printCardsInfo = "";
            play.ForEach(c => printCardsInfo += c.ToString());
            Console.WriteLine($"玩家 {p.Name} 打出了 {pattern.Name} {printCardsInfo}");
            if (p.HandCards.Count == 0) //贏家
            {
                return;
            }
        }
        else
        {
            next.DistinguishingGameAndPlayerStatus();
        }
    }
}
*/