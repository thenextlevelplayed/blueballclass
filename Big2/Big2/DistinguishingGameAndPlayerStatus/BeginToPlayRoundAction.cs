namespace Big2.DistinguishingGameAndPlayerStatus;

public class BeginToPlayRoundAction(CardPattern cardPattern, RoundAction? next, CompareCardPattern compareCardPattern)
    : RoundAction(cardPattern, next, compareCardPattern)
{
    protected override bool CheckGameAndPlayerStatus(Player player, Dictionary<CardPattern, List<Card>> topPlay,
        Player topPlayer)
    {
        return topPlayer == null && topPlay == null;
    }

    protected override void PlayActionIsPass(ref Player player, ref bool isValidPlay)
    {
        Console.WriteLine("第一手牌不能為空。請重新選牌。");
    }

    protected override bool ShouldCheckC3()
    {
        return true;
    }
}
/*
 * public override void DistinguishingGameAndPlayerStatus(List<Player> players, Player player,
        ref Dictionary<CardPattern, List<Card>> topPlay, // <-- 加上 ref
        ref Player topPlayer // <-- 加上 ref
    )
    {
        // 這個 Handler 只處理新的回合開始且桌上沒有牌的情況
        if (topPlayer == null && topPlay == null)
        {
            Console.Write("新的回合開始了。");

            // 找到擁有梅花三的玩家 (假設只有一個)
            // var firstPlayer = players
            //     .Where(c => c.HandCards.Any(card => card.Rank.Equals(Rank.Three) && card.Suit.Equals(Suit.C)))
            //     .First();

            Console.WriteLine($"輪到{player.Name}了");

            List<Card>? currentPlay = null; // 儲存玩家本次嘗試出的牌
            CardPattern? currentPattern = null; // 儲存本次嘗試出的牌型
            bool isValidFirstPlay = false; // 標記本次出的牌是否為合法的起手牌

            // 使用迴圈直到玩家出牌合法
            while (!isValidFirstPlay)
            {
                currentPlay = player.Play(); // <-- 在迴圈內部重新取得玩家輸入

                // 1. 檢查是否為 null 或空列表 (第一手牌不能 PASS 或為空)
                if (currentPlay == null || !currentPlay.Any())
                {
                    Console.WriteLine("第一手牌不能為空。請重新選牌。");
                    // isValidFirstPlay 仍然是 false，迴圈會繼續
                    continue; // 跳到迴圈的下一次迭代，重新取得輸入
                }

                // 2. 檢查是否包含梅花三
                if (!currentPlay.Any(card => card.Rank.Equals(Rank.Three) && card.Suit.Equals(Suit.C)))
                {
                    Console.WriteLine("第一手牌必須包含梅花三。請重新選牌。");
                    // isValidFirstPlay 仍然是 false，迴圈會繼續
                    continue; // 跳到迴圈的下一次迭代，重新取得輸入
                }

                // 3. 檢查是否構成合法的 Big Two 牌型
                currentPattern = Handle(currentPlay); // <-- 取得牌型

                if (currentPattern == null) // Handle 方法返回 null 表示牌型不合法
                {
                    Console.WriteLine("您出的牌型不合法。請重新選牌。");
                    // isValidFirstPlay 仍然是 false，迴圈會繼續
                    continue; // 跳到迴圈的下一次迭代，重新取得輸入
                }

                // 如果通過以上所有檢查，則這是合法的起手牌
                isValidFirstPlay = true;
            } // <-- 迴圈結束：玩家出了合法的起手牌

            // 迴圈結束時，currentPlay 不為 null/empty，currentPattern 不為 null
            if (currentPlay != null && currentPattern != null)
            {
                // 確定本次出的牌組中的最大牌 (通常是牌型中決定大小的牌)
                // 這裡假設 play.Last() 就是最大牌，且 Handle 確認了牌型。
                // 更嚴謹的做法是根據 currentPattern 和 currentPlay 來確定最大牌，
                // 例如：Card topCard = currentPattern.GetHighestCard(currentPlay);
                // 但為了與您原來的邏輯保持一致，我們仍然使用 play.Last()，但請注意潛在風險
                // 如果 player.Play() 返回的列表沒有按大小排序，或者 Last() 對於某些牌型不代表最大牌，這裡會有問題。
                var topCard = currentPlay.Last(); // 假設列表是排序過的，最後一張是最大的

                // 更新遊戲狀態，使用 ref 參數會修改 Game 類別中的欄位
                topPlay = new Dictionary<CardPattern, List<Card>>(); // 建立新的 Dictionary
                topPlay.Add(currentPattern, currentPlay); // 加入牌型和最大牌
                topPlayer = player; // 設定出牌玩家

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
                    ref topPlay, ref topPlayer);
            }
        }
    }
 * 替換成責任鍊
 */