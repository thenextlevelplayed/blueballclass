namespace Big2;

public class Deck
{
    public List<Card> Cards { get; set; } = new List<Card>();

    //測試用
    public Deck(string orderedCardsString)
    {
        // 清空或忽略預設建立的牌
        this.Cards = new List<Card>();
        // 測試用:解析輸入字串並填充 Cards 列表
        this.Cards = ParseOrderedCardsString(orderedCardsString);
    }

    private List<Card> CreateCards()
    {
        List<Card> newCards = new List<Card>();
        foreach (var suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (var rank in Enum.GetValues(typeof(Rank)))
            {
                newCards.Add(new Card((Rank)rank, (Suit)suit));
            }
        }

        return newCards;
    }

    public void Shuffle()
    {
        string printDeckCards = "";
        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int randomIndex = new Random().Next(0, i + 1);
            (Cards[i], Cards[randomIndex]) = (Cards[randomIndex], Cards[i]);
            printDeckCards += Cards[i].ToString() + " ";
        }

        Console.WriteLine(printDeckCards);
    }

    public Card RemoveCard()
    {
        var removeCard = Cards[^1];
        Cards.Remove(Cards[^1]);
        return removeCard;
    }

    //測試用
    // 新增的輔助方法：解析指定順序的卡牌字串
    private List<Card> ParseOrderedCardsString(string orderedCardsString)
    {
        List<Card> parsedCards = new List<Card>();
        // 按空白分割字串，並移除空白條目
        string[] cardStrings = orderedCardsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var cardString in cardStrings)
        {
            try
            {
                // 解析單張卡牌字串並加入列表
                parsedCards.Add(ParseSingleCard(cardString));
            }
            catch (FormatException ex)
            {
                // 如果解析失敗，拋出新的例外，包含是哪個字串出問題
                throw new FormatException($"無法解析卡牌字串 '{cardString}'。請檢查格式。", ex);
            }
            catch (ArgumentException ex)
            {
                // 如果解析出的花色或數字值無效
                throw new ArgumentException($"卡牌字串 '{cardString}' 包含無效的花色或數字值。", ex);
            }
        }

        // 解析完成後，牌堆的最後一張應該是字串中的最後一張牌
        // 如果您的 RemoveCard() 移除列表的最後一個元素 (Cards[^1])
        // 並且您希望發牌順序是按照測試字串的順序（從左到右）
        // 那麼這個列表的順序是正確的。
        // 如果您希望 RemoveCard() 移除列表的第一個元素，那麼解析後的列表需要反轉

        return parsedCards;
    }
    private Card ParseSingleCard(string cardString)
    {
        // 預期格式: Suit[Rank]
        int openBracketIndex = cardString.IndexOf('[');
        int closeBracketIndex = cardString.IndexOf(']');

        // 基本格式檢查
        if (openBracketIndex <= 0 || closeBracketIndex <= openBracketIndex || closeBracketIndex != cardString.Length - 1)
        {
            throw new FormatException($"卡牌字串格式錯誤: {cardString}");
        }

        // 提取花色和數字字串
        string suitString = cardString.Substring(0, openBracketIndex);
        string rankString = cardString.Substring(openBracketIndex + 1, closeBracketIndex - openBracketIndex - 1);

        // 解析花色
        if (!Enum.TryParse<Suit>(suitString, true, out Suit suit)) // true 忽略大小寫
        {
            throw new ArgumentException($"無效的花色字串: {suitString}");
        }
        // 解析數字
        Rank rank;
        switch (rankString.ToUpper()) // 轉為大寫以便解析 A, J, Q, K
        {
            case "A":
                rank = Rank.A;
                break;
            case "2":
                rank = Rank.Two;
                break;
            case "J":
                rank = Rank.J;
                break;
            case "Q":
                rank = Rank.Q;
                break;
            case "K":
                rank = Rank.K;
                break;
            default:
                // 嘗試解析數字 3-10
                if (int.TryParse(rankString, out int rankValue))
                {
                    if (Enum.IsDefined(typeof(Rank), rankValue) && rankValue >= 3 && rankValue <= 10)
                    {
                        rank = (Rank)rankValue;
                    }
                    else
                    {
                        throw new ArgumentException($"數字 '{rankString}' 超出有效範圍 (3-10) 或無效。");
                    }
                }
                else
                {
                    throw new ArgumentException($"無法解析為數字或特殊牌點: {rankString}");
                }
                break;
        }

        return new Card(rank, suit);
    }
}