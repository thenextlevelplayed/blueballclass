namespace Big2.DistinguishingCardPattern;

public class DistinguishingPairCardPattern(CardPattern next) : CardPattern(next)
{
    protected override bool IsCardPattern(List<Card>? cards)
    {
        return cards?.Count == 2 && cards[0].Rank == cards[1].Rank;
    }

    protected override CardPattern Result()
    {
        return new DistinguishingPairCardPattern(null);
    }

    protected override string NameItSelf()
    {
        return "對子 ";
    }

    public override bool Equals(object obj)
    {
        // 1. 先呼叫父類別的 Equals 方法
        if (!base.Equals(obj))
        {
            return false;
        }

        // 2. 檢查 obj 是否為 null 且類型是否相符
        // 使用 'is' 搭配 pattern matching 是比較現代且安全的寫法
        if (obj is not DistinguishingPairCardPattern otherPattern)
            // 如果你希望它也能與基類 CardPattern 比較，可以寫 'is CardPattern otherPattern'
            // 但通常同類型比較更常見且準確
        {
            return false;
        }

        // 3. 在這裡可以添加任何 DistinguishingPairCardPattern 特有的比較邏輯
        //    例如，如果這個類別有額外的屬性需要比較，就在這裡進行。
        //    在這個例子中，沒有額外的屬性，所以我們只需要依賴父類別的比較。

        return true;
    }

    // *** 覆寫 GetHashCode 方法 ***
    // 當你覆寫 Equals 時，**強烈建議**同時覆寫 GetHashCode，
    // 並且保證如果 a.Equals(b) 為 true，那麼 a.GetHashCode() 必須等於 b.GetHashCode()。
    public override int GetHashCode()
    {
        // 結合父類別的 HashCode 和子類別特有的屬性的 HashCode（如果有的話）
        return HashCode.Combine(base.GetHashCode(), NameItSelf());
    }
}

/*
 * public override CardPattern DistinguishingCardPattern(List<Card>? cards)
    {
        bool isPairCardPattern = cards?.Count == 2 && cards[0].Rank == cards[1].Rank;
        if (isPairCardPattern) //Pair
        {
            return new DistinguishingPairCardPattern(null);
        }
        else if(next!=null)
        {
            return next.DistinguishingCardPattern(cards);
        }
        else
        {
            return null;
        }
    }
    套樣板方式
 */