namespace Big2.DistinguishingGameAndPlayerStatus;

public class FirstRoundAction(CardPattern cardPattern, RoundAction? next, CompareCardPattern compareCardPattern)
    : RoundAction(cardPattern, next, compareCardPattern)
{
    protected override bool CheckGameAndPlayerStatus(Player player, Dictionary<CardPattern, List<Card>> topPlay,
        Player topPlayer)
    {
        return topPlayer != null && (topPlay == null || topPlay.Count == 0);
    }

    protected override void PlayActionIsPass(ref Player player, ref bool isValidPlay)
    {
        Console.WriteLine("新的回合第一手牌不能為空或 PASS。請重新選牌。");
    }
}
/*
 public override void DistinguishingGameAndPlayerStatus(List<Player> players, Player player,
        ref Dictionary<CardPattern, List<Card>> topPlay, // <-- 加上 ref
        ref Player topPlayer // <-- 加上 ref
    )
    {
        // 這個 Handler 用於處理新的回合開始，由上一輪的贏家先出牌的情況
        // 條件：topPlayer 不為 null (有贏家)，且桌上沒有牌 (topPlay 為 null 或空)
        if (topPlayer != null && (topPlay == null || topPlay.Count == 0))
        {
            Console.Write("新的回合開始了。");

            // 新回合由上一輪的贏家 (topPlayer) 先出牌
            // var firstPlayer = topPlayer; // 使用傳入的 topPlayer 作為第一位出牌玩家

            Console.WriteLine($"輪到{player.Name}了");

            List<Card>? currentPlay = null; // 儲存玩家本次嘗試出的牌
            CardPattern? currentPattern = null; // 儲存本次嘗試出的牌型
            bool isValidFirstPlay = false; // 標記本次出的牌是否合法

            // 使用迴圈直到玩家出牌合法
            // 新回合的第一手牌通常不能 PASS。
            while (!isValidFirstPlay)
            {
                currentPlay = player.Play(); // <-- 在迴圈內部重新取得玩家輸入

                // 1. 檢查是否為 null 或空列表 (新回合第一手牌不能 PASS 或為空)
                if (currentPlay == null || !currentPlay.Any())
                {
                    Console.WriteLine("新的回合第一手牌不能為空或 PASS。請重新選牌。");
                    // isValidFirstPlay 仍然是 false，迴圈會繼續
                    continue; // 跳到迴圈的下一次迭代，重新取得輸入
                }

                // 2. 檢查是否構成合法的 Big Two 牌型
                currentPattern = Handle(currentPlay); // <-- 取得牌型

                if (currentPattern == null) // Handle 方法返回 null 表示牌型不合法
                {
                    Console.WriteLine("您出的牌型不合法。請重新選牌。");
                    // isValidFirstPlay 仍然是 false，迴圈會繼續
                    continue; // 跳到迴圈的下一次迭代，重新取得輸入
                }

                // 如果通過以上所有檢查，則這是合法的起手牌
                isValidFirstPlay = true;
            } // <-- 迴圈結束：玩家出了合法的牌

            // 迴圈結束時，currentPlay 不為 null/empty，currentPattern 不為 null
            if (currentPlay != null && currentPattern != null)
            {
                // 確定本次出的牌組中的最大牌
                // 這裡仍然假設 play.Last() 是最大牌，如果 Handle 確認了牌型且列表排序正確。
                // 更新遊戲狀態，使用 ref 參數會修改 Game 類別中的欄位
                topPlay = new Dictionary<CardPattern, List<Card>>(); // 建立新的 Dictionary
                topPlay.Add(currentPattern, currentPlay); // 加入牌型和最大牌
                // topPlayer 保持不變，因為就是他剛才出牌

                // 從玩家手牌中移除打出的牌
                player.RemoveCards(currentPlay);

                // 印出出牌資訊
                string printCardsInfo = "";
                currentPlay.ForEach(c => printCardsInfo += c.ToString());
                Console.WriteLine($"玩家 {player.Name} 打出了 {currentPattern.Name} {printCardsInfo}");
            }
            // 如果因為某種異常情況迴圈退出但 play/pattern 為 null，這裡可以選擇拋出錯誤或處理
        }
        else
        {
            if (next != null)
            {
                next.DistinguishingGameAndPlayerStatus(players, player,
                    ref topPlay,
                    ref topPlayer);
            }
        }
    }

 */