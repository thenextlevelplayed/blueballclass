namespace Big2;

public abstract class RoundAction
{
    protected CardPattern CardPattern { get; set; }

    protected RoundAction? Next;
    
    private CompareCardPattern _compareCardPattern;


    public RoundAction(CardPattern cardPattern, RoundAction? next, CompareCardPattern compareCardPattern)
    {
        this.CardPattern = cardPattern;
        this.Next = next;
        this._compareCardPattern = compareCardPattern;
    }

    private CardPattern Handle(List<Card>? cards)
    {
        return CardPattern.TemplateDistinguishingCardPattern(cards);
    }

    public void TemplateGameAndPlayerStatus(List<Player> players, Player player,
        ref Dictionary<CardPattern, List<Card>> topPlay, // <-- 加上 ref
        ref Player topPlayer)
    {
        if (CheckGameAndPlayerStatus(player, topPlay, topPlayer))
        {
            Console.WriteLine($"輪到{player.Name}了");

            List<Card>? currentPlay = null; // 儲存玩家本次嘗試出的牌
            CardPattern? currentPattern = null; // 儲存本次嘗試出的牌型
            bool isValidPlay = false; // 標記本次出的牌是否合法

            // 使用迴圈直到玩家出牌合法或選擇 PASS
            while (!isValidPlay)
            {
                currentPlay = player.Play();

                if (currentPlay==null || !currentPlay.Any()) // 玩家選擇 PASS，出牌是空的
                {
                    PlayActionIsPass(ref player,ref isValidPlay);
                    continue;
                }

                // 如果玩家沒有 PASS，則驗證出的牌
                currentPattern = Handle(currentPlay); // 檢查牌型
                // 2. 檢查牌型是否合法且與桌面上的牌型一致
                if (currentPattern == null)
                {
                    Console.WriteLine("您出的牌型不合法。請重新選牌。");
                    continue;
                }
                /*
                 * BeginToPlayRound:檢查手牌有沒有梅花三，如果沒有使用continue，有則這是合法的起手牌，回傳isValidFirstPlay = true
                 * FirstRoundAction:通過以上所有檢查，則這是合法的起手牌，回傳isValidFirstPlay = true
                 * PlayRoundAction:檢查手牌有沒有符合檯面上的牌型，且手牌必須大於檯面上的牌型，如果沒有使用continue，有則這是合法的起手牌，回傳isValidFirstPlay = true
                 */
                
                if (ShouldCheckC3())//檢查梅花三
                {
                    if (!currentPlay.Any(card => card.Rank.Equals(Rank.Three) && card.Suit.Equals(Suit.C)))
                    {
                        Console.WriteLine("第一手牌必須包含梅花三。請重新選牌。");
                        continue; 
                    }
                }

                if (ShouldCompareCardPattern())//需比較手牌
                {
                    CardPattern lastTopPattern = topPlay.Keys.First();
                    if (!currentPattern.Equals(lastTopPattern))
                    {
                        Console.WriteLine($"您出的牌型 ({currentPattern.Name}) 與桌面上的牌型 ({lastTopPattern.Name}) 不符。請重新選牌。");
                        continue;
                    }
                    if (!_compareCardPattern.TemplateCompare(topPlay, currentPattern, currentPlay))
                    {
                        Console.WriteLine($"您出的牌太小 ({currentPlay.Last()})，必須大於 {topPlay.First().Value.First()}。請重新選牌。");
                        continue;
                    }
                }

                // 如果所有驗證都通過，標記為合法
                isValidPlay = true;
            } // <-- 迴圈結束：玩家出了合法牌或選擇 PASS

            // 如果迴圈是因為出了合法牌而結束 (isValidPlay 為 true 且 currentPlay 不為 null)
            if (currentPlay != null && currentPattern != null)
            {
                string printCardsInfo = "";
                currentPlay.ForEach(c => printCardsInfo += c.ToString());
                Console.WriteLine($"玩家 {player.Name} 打出了 {currentPattern.Name} {printCardsInfo}");
                var topCard = currentPlay.Last();

                // 更新遊戲狀態 - 使用合法的牌
                topPlay = new Dictionary<CardPattern, List<Card>>(); // 重新賦值 Dictionary
                topPlay.Add(currentPattern, currentPlay);
                topPlayer = player; // 更新出牌玩家

                // 從玩家手牌中移除打出的牌 - 使用合法的牌
                player.RemoveCards(currentPlay);
            }
            // 如果是 PASS 退出迴圈，CallNext 已經在迴圈內部呼叫過了
        }
        else
        {
            if (Next != null)
            {
                Next.TemplateGameAndPlayerStatus(players, player,
                    ref topPlay,
                    ref topPlayer);
            }
        }
    }
    

    protected abstract bool CheckGameAndPlayerStatus(Player player,
        Dictionary<CardPattern, List<Card>> topPlay, // <-- 加上 ref
        Player topPlayer);
    
    protected abstract void PlayActionIsPass(ref Player player, ref bool isValidPlay);

    protected virtual bool ShouldCheckC3()
    {
        return false;
    }

    protected virtual bool ShouldCompareCardPattern()
    {
        return false;
    }

    // public abstract void DistinguishingGameAndPlayerStatus(List<Player> players, Player player,
    //     ref Dictionary<CardPattern, List<Card>> topPlay, // <-- 加上 ref
    //     ref Player topPlayer);
}