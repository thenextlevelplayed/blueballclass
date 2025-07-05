using Big2.DistinguishingCardPattern;

namespace Big2.DistinguishingGameAndPlayerStatus;

public class PlayRoundAction : RoundAction
{
    private CompareCardPattern _compareCardPattern;

    public PlayRoundAction(CardPattern cardPattern, RoundAction? next, CompareCardPattern compareCardPattern) : base(
        cardPattern, next, compareCardPattern)
    {
        this.CardPattern = cardPattern;
        this.Next = next;
        this._compareCardPattern = compareCardPattern;
    }

    protected override bool CheckGameAndPlayerStatus(Player player, Dictionary<CardPattern, List<Card>> topPlay,
        Player topPlayer)
    {
        return player.IsPass == false;
    }

    protected override void PlayActionIsPass(ref Player player, ref bool isValidPlay)
    {
        Console.WriteLine($"玩家 {player.Name} PASS.");
        player.IsPass = true;
        isValidPlay = true; // 退出迴圈
    }

    protected override bool ShouldCompareCardPattern()
    {
        return true;
    }
}

/*
 *  if (currentPattern.GetType() == typeof(DistinguishingFullHouseCardPattern))
                {
                    //do compare FullHouseCardPattern rule , parameter1: Dictionary<CardPattern, List<Card>> topPlay  parameter2: List<Card> currentPlay
                }
                else if (currentPattern.GetType() == typeof(DistinguishingStraightCardPattern))
                {
                    //do compare PairCardPattern rule
                }
                else if (currentPattern.GetType() == typeof(DistinguishingPairCardPattern))
                {
                    //do compare PairCardPattern rule
                }
                else if (currentPattern.GetType() == typeof(DistinguishingSingleCardPattern))
                {
                    //do compare SingleCardPattern rule
                }
                替換成責任鍊
                _compareCardPattern.Compare
 */
/*
 public override void DistinguishingGameAndPlayerStatus(List<Player> players, Player player,
       ref Dictionary<CardPattern, List<Card>> topPlay, // <-- 加上 ref
       ref Player topPlayer // <-- 加上 ref
   )
   {
       if (player.IsPass == false)
       {
           Console.WriteLine($"輪到{player.Name}了");

           List<Card>? currentPlay = null; // 儲存玩家本次嘗試出的牌
           CardPattern? currentPattern = null; // 儲存本次嘗試出的牌型
           bool isValidPlay = false; // 標記本次出的牌是否合法

           // 使用迴圈直到玩家出牌合法或選擇 PASS
           while (!isValidPlay)
           {
               currentPlay = player.Play(); // <-- 在迴圈內部重新取得玩家輸入

               if (currentPlay == null) // 玩家選擇 PASS
               {
                   Console.WriteLine($"玩家 {player.Name} PASS.");
                   player.IsPass = true;
                   isValidPlay = true; // 退出迴圈
                   return; // 此 Handler 處理完畢
               }

               // 如果玩家沒有 PASS，則驗證出的牌
               currentPattern = Handle(currentPlay); // 檢查牌型

               // --- 驗證規則 ---
               // 1. 確保有頂牌可以比較 (除非這是新的回合第一手，但那個應該是 BeginToPlayRoundAction 處理)
               //    這個 Handler 應該假設 topPlay 不為 null 且不為空，否則這是遊戲流程錯誤。
               if (topPlay == null || !topPlay.Any())
               {
                   Console.WriteLine("遊戲狀態錯誤：輪到此玩家時，TopPlay應已存在。強制PASS。");
                   player.IsPass = true;
                   isValidPlay = true;
                   return;
               }

               // 2. 檢查牌型是否合法且與桌面上的牌型一致
               if (currentPattern == null)
               {
                   Console.WriteLine("您出的牌型不合法。請重新選牌。");
                   // isValidPlay 仍然是 false，迴圈會繼續
                   continue; // 直接進入迴圈的下一次迭代，重新取得輸入
               }

               // 取得桌面上最後一次出的牌型

               // CardPattern lastTopPattern = Enumerable.<CardPattern>(topPlay.Keys);
               CardPattern lastTopPattern = topPlay.Keys.First();
               if (!currentPattern.Equals(lastTopPattern))
               {
                   Console.WriteLine($"您出的牌型 ({currentPattern.Name}) 與桌面上的牌型 ({lastTopPattern.Name}) 不符。請重新選牌。");
                   // isValidPlay 仍然是 false，迴圈會繼續
                   continue;
               }

               // 3. 檢查出的牌是否比桌面上的牌大
               // 確保出的牌和桌面上的牌都不為空列表，避免 Last() 拋出 InvalidOperationException
               if (!currentPlay.Any())
               {
                   Console.WriteLine("出的牌不能是空列表。請重新選牌。");
                   continue;
               }

               if (!topPlay.Values.Any())
               {
                   Console.WriteLine("遊戲狀態錯誤：TopPlay的值應已存在。強制PASS。");
                   player.IsPass = true;
                   isValidPlay = true;
                   return;
               }

               if (!_compareCardPattern.TemplateCompare(topPlay, currentPattern, currentPlay))
               {
                   Console.WriteLine($"您出的牌太小 ({currentPlay.Last()})，必須大於 {topPlay.First().Value.First()}。請重新選牌。");
                   // isValidPlay 仍然是 false，迴圈會繼續
                   continue;
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
               Next.DistinguishingGameAndPlayerStatus(players, player,
                   ref topPlay,
                   ref topPlayer);
           }
       }
   }
    替換成責任鍊
 */