/*
namespace Big2;

public class Play(
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
        if (Big2.Player.IsPass == false)
        {
            Console.WriteLine($"輪到{Big2.Player.Name}了");
            var play = Big2.Player.Play();
            //檢查合法牌型
            var pattern = Handle(play);
            if (!TopPlay.Keys.Last().Equals(pattern) || play.Last() < TopPlay.Values.Last())
            {
                //請玩家重新選牌
            }

            string printCardsInfo = "";
            play.ForEach(c => printCardsInfo += c.ToString());
            Console.WriteLine($"玩家 {Big2.Player.Name} 打出了 {pattern.Name} {printCardsInfo}");
            var topCard = play.Last();
            // this.TopPlay = new Dictionary<CardPattern, Card>();
            TopPlay.Add(pattern, topCard);
            TopPlayer = Big2.Player;
            Big2.Player.RemoveCards(play);
        }
    }
}
*/