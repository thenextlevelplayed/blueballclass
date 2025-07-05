/*
namespace Big2;

public class Pass(
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
        int count = 0;
        Players.ForEach(player =>
        {
            if (player.IsPass == true)
            {
                count++;
            }
        });
        if (Big2.Player.IsPass == true && count == 3)
        {
            TopPlay.Clear();
            new FirstPlay(Players, player, TopPlay, cardPattern, topPlayer, next);
        }
        else if (Big2.Player.IsPass == true)
        {
            //to do 輪到下一位玩家
        }else
        {
            next.DistinguishingGameAndPlayerStatus();
        }
    }
}
*/