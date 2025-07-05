namespace Big2;

using Big2.DistinguishingCardPattern;
using Big2.DistinguishingGameAndPlayerStatus;

public class Game
{
    private Dictionary<CardPattern, List<Card>> TopPlay;
    private Player TopPlayer;
    private List<Player> Players { get; set; } = new List<Player>();
    private Deck Deck { get; set; }
    private RoundAction RoundAction { get; set; }

    public Game(Deck deck, RoundAction roundAction)
    {
        Deck = deck;
        Players = CreatePlayers();
        this.RoundAction = roundAction;
    }

    private List<Player> CreatePlayers()
    {
        for (int i = 0; i < 4; i++)
        {
            Player p = new Human(i);
            Players.Add(p);
        }

        return Players;
    }

    private void NamePlayer(Player p)
    {
        p.NameHimSelf();
    }

    private Player SequncePlayer(Player p)
    {
        return Players[(p.Index + 1) % 4];
    }

    private void HandleRoundAction(Player p)
    {
        RoundAction.TemplateGameAndPlayerStatus(this.Players, p, ref this.TopPlay, ref this.TopPlayer);
    }
    
    private void Round()
    {
        // 找到擁有梅花三的玩家作為遊戲的起始玩家
        var currentPlayer = this.Players
            .First(c => c.HandCards.Any(card => card.Rank.Equals(Rank.Three) && card.Suit.Equals(Suit.C)));


        // 確保遊戲開始時桌面為空，方便 BeginToPlayRoundAction 判斷
        this.TopPlay = null;
        this.TopPlayer = null;

        Console.WriteLine("遊戲開始！");

        // 遊戲主迴圈，直到有玩家手牌出完
        while (currentPlayer.HandCards.Count != 0)
        {
            // 處理當前玩家的回合行動 (出牌或 PASS)
            // handleRoundAction 會根據目前的遊戲狀態 (TopPlay 是否為空) 和玩家狀態 (是否已 PASS)
            // 呼叫責任鏈中的對應 Handler。
            // 這個呼叫會更新 TopPlay, TopPlayer, 以及玩家的 IsPass 狀態。
            HandleRoundAction(currentPlayer);

            // 檢查當前玩家是否出完牌 (贏得遊戲)
            if (currentPlayer.HandCards.Count == 0)
            {
                break; // 玩家贏了，結束遊戲
            }

            // --- 判斷下一位玩家 ---

            // 檢查是否發生三位玩家 PASS 的情況
            int passCount = Players.Count(p => p.IsPass);

            if (passCount == 3)
            {
                Console.WriteLine("\n三位玩家PASS，新回合開始！"); // 加入換行讓輸出更清晰

                // 清空桌面 (TopPlay設為null，表示新回合的領頭牌尚未出現)
                this.TopPlay = new Dictionary<CardPattern, List<Card>>();
                // 重設所有玩家的 PASS 狀態
                Players.ForEach(p => p.IsPass = false);

                // 新回合由前一輪最後出有效牌的玩家領頭
                // TopPlayer 應該在 PlayRoundAction 中被正確設定為出有效牌的玩家
                currentPlayer = this.TopPlayer;
                Console.WriteLine($"新回合由玩家 {currentPlayer.Name} 開始");

                // 當發生三位 PASS 時，我們已經確定了下一位玩家是 TopPlayer
                // 迴圈會直接進入下一輪，處理這位領頭玩家的行動
            }
            else
            {
                // 如果沒有發生三位 PASS，則輪到順序的下一位玩家
                Player nextCandidate = SequncePlayer(currentPlayer);
                int playersChecked = 0; // 防止死迴圈

                // 找到下一位尚未 PASS 的玩家
                while (nextCandidate.IsPass && playersChecked < 4)
                {
                    nextCandidate = SequncePlayer(nextCandidate);
                    playersChecked++;
                }

                // 如果檢查了一圈仍然回到原來的 passing 玩家，表示邏輯有問題，或所有人都 PASS 了 (應該被上面的 3 PASS 捕捉)
                if (nextCandidate.IsPass && passCount < 3) // 加上 passCount < 3 避免和 3 PASS 衝突
                {
                    Console.WriteLine("Error: Could not find a player who isn't passing in sequence.");
                    break; // 退出迴圈防止無限循環
                }

                // 更新當前玩家為下一位可以行動的玩家
                currentPlayer = nextCandidate;
            }

            // 迴圈繼續，處理新的 currentPlayer
        }

        Console.WriteLine($"遊戲結束，遊戲的勝利者為 {currentPlayer.Name}");
    }

    public void GameStart()
    {
        foreach (var player in Players)
        {
            NamePlayer(player); //遊戲開始之後就會輸入每個玩家的名稱 (Name)
        }

        //Deck.Shuffle(); //牌堆洗牌

        Player p = Players[0];
        for (int i = Deck.Cards.Count - 1; i >= 0; i--) //將 52 張牌輪流發給四位玩家直到牌堆空了為止。
        {
            p.Deal(Deck.RemoveCard());
            p = SequncePlayer(p);
        }

        Players.ToList()
            .ForEach(ps =>
            {
                ps.HandCards = ps.HandCards.OrderBy(c => c.Rank).ThenBy(c => c.Suit).ToList();
            }); //sorted each player handcard

        Round();
    }
}

/*
    public void Round()
    {
        //new round start, remove TopPlay
        TopPlay.Clear();

        //first player
        var p = TopPlayer;
        Console.WriteLine($"輪到{p.Name}了");
        var play = p.Play();
        while (play == null) //不能為null
        {
            play = p.Play();
        }

        var topCard = play.Last();
        var pattern = Handle(play); //check cardpattern

        string printCardsInfo = "";
        play.ForEach(c => printCardsInfo += c.ToString());
        Console.WriteLine($"玩家 {p.Name} 打出了 {pattern.Name} {printCardsInfo}");

        this.TopPlay = new Dictionary<CardPattern, Card>();
        TopPlay.Add(pattern, topCard);
        p.RemoveCards(play);
        if (p.HandCards.Count == 0)
        {
            return;
        }
        else
        {
            //second third forth player action
            for (int i = 0; i < 3; i++)
            {
                p = SequncePlayer(p);
                Console.WriteLine($"輪到{p.Name}了");
                play = p.Play();
                pattern = Handle(play); //check cardpattern
                if (play == null) // player pass!
                {
                    Console.WriteLine($"玩家 {p.Name} PASS.");
                    p = SequncePlayer(p);
                }
                else
                {
                    while (play != null || !TopPlay.Keys.Contains(pattern) ||
                           play.Last().Rank < TopPlay.First().Value.Rank ||
                           (play.Last().Rank == TopPlay.First().Value.Rank &&
                            play.Last().Suit <= TopPlay.First().Value.Suit))
                    {
                        play = p.Play();
                    }

                    printCardsInfo = "";
                    play.ForEach(c => printCardsInfo += c.ToString());
                    Console.WriteLine($"玩家 {p.Name} 打出了 {pattern.Name} {printCardsInfo}");

                    topCard = play.Last();
                    this.TopPlay = new Dictionary<CardPattern, Card>();
                    TopPlay.Add(pattern, topCard);
                    TopPlayer = p;
                    p.RemoveCards(play);
                    if (p.HandCards.Count == 0)
                    {
                        return;
                    }
                }
            }

            Round();
        }
    }

    public void FirstRound()
    {
        Console.Write("新的回合開始了。");
        //first player
        var p = Players.Where(c => c.HandCards.Any(card => card.Rank.Equals(Rank.Three) && card.Suit.Equals(Suit.C)))
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


        //second third forth player action
        for (int i = 0; i < 3; i++)
        {
            p = SequncePlayer(p);
            Console.WriteLine($"輪到{p.Name}了");
            play = p.Play();
            if (play == null) // 喊 PASS —— 放棄出牌機會，不打任何牌
            {
                Console.WriteLine($"玩家 {p.Name} PASS.");
                p = SequncePlayer(p);
            }
            else
            {
                //檢查合法牌型
                pattern = Handle(play);
                if (!TopPlay.Keys.Last().Equals(pattern) || play.Last() < TopPlay.Values.Last())
                {
                    //請玩家重新選牌
                }


                printCardsInfo = "";
                play.ForEach(c => printCardsInfo += c.ToString());
                Console.WriteLine($"玩家 {p.Name} 打出了 {pattern.Name} {printCardsInfo}");
                topCard = play.Last();
                // this.TopPlay = new Dictionary<CardPattern, Card>();
                TopPlay.Add(pattern, topCard);
                TopPlayer = p;
                p.RemoveCards(play);
            }
        }
    }
*/