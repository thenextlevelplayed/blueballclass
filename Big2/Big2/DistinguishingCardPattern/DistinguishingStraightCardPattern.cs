namespace Big2.DistinguishingCardPattern;

public class DistinguishingStraightCardPattern(CardPattern next) : CardPattern(next)
{
    protected override bool IsCardPattern(List<Card>? cards)
    {
        if (cards?.Count != 5)
        {
            return false;
        }

        List<Card> sortedCards = new List<Card>();
        bool isNormalStraight = true;
        sortedCards = cards.OrderBy(c => c.Rank).ThenBy(c => c.Suit).ToList();
        var uniqueRanks = new HashSet<Rank>(sortedCards.Select(c => c.Rank));
        if (uniqueRanks.Count != 5)
        {
            return false;
        }

        // 檢查手牌的牌階集合是否與任何一個有效的順子牌階集合相符
        foreach (var validSet in ValidStraightRankSets)
        {
            if (uniqueRanks.SetEquals(validSet))
            {
                return true;
            }
        }

        // 如果沒有找到匹配的有效順子組合，則不是順子
        return false;
    }

    protected override CardPattern Result()
    {
        return new DistinguishingStraightCardPattern(null);
    }


    protected override string NameItSelf()
    {
        return "順子 ";
    }

    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }

        if (obj is not DistinguishingStraightCardPattern otherPattern)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), NameItSelf());
    }

    private static readonly List<HashSet<Rank>> ValidStraightRankSets = new List<HashSet<Rank>>()
    {
        new HashSet<Rank> { Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven },
        new HashSet<Rank> { Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight },
        new HashSet<Rank> { Rank.Five, Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine },
        new HashSet<Rank> { Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten },
        new HashSet<Rank> { Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten, Rank.J },
        new HashSet<Rank> { Rank.Eight, Rank.Nine, Rank.Ten, Rank.J, Rank.Q },
        new HashSet<Rank> { Rank.Nine, Rank.Ten, Rank.J, Rank.Q, Rank.K },
        new HashSet<Rank> { Rank.Ten, Rank.J, Rank.Q, Rank.K, Rank.A }, // 10-J-Q-K-A
        new HashSet<Rank> { Rank.J, Rank.Q, Rank.K, Rank.A, Rank.Two }, // J-Q-K-A-2
        new HashSet<Rank> { Rank.Q, Rank.K, Rank.A, Rank.Two, Rank.Three }, // Q-K-A-2-3
        new HashSet<Rank> { Rank.K, Rank.A, Rank.Two, Rank.Three, Rank.Four }, // K-A-2-3-4
        new HashSet<Rank>
            { Rank.A, Rank.Two, Rank.Three, Rank.Four, Rank.Five }, // A-2-3-4-5 (梅花 3 是最小的牌，所以 3-4-5-A-2 通常是最小的順子)
        new HashSet<Rank>
            { Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six } // 2-3-4-5-6 (這也是一個順子，但在某些規則中比 A-2-3-4-5 大)
    };
}

/*
 * public override CardPattern DistinguishingCardPattern(List<Card>? cards)
    {
        if (cards?.Count != 5)
        {
            return next?.DistinguishingCardPattern(cards) ?? null;
        }

        List<Card> sortedCards = new List<Card>();
        sortedCards = cards.OrderBy(c => c.Rank).ThenBy(c => c.Suit).ToList();
        int secondPointer = 0;
        bool isStraightCardPattern = true;
        for (int firstPointer = 0; firstPointer < sortedCards.Count; firstPointer++)
        {
            secondPointer =  firstPointer + 1;
            if (secondPointer >= sortedCards.Count)
            {
                break;
            }
            if (sortedCards[firstPointer].Rank+1 != sortedCards[secondPointer].Rank)
            {
                isStraightCardPattern = false;
                break;
            }
        }

        if (isStraightCardPattern) //Straight
        {
            return new DistinguishingStraightCardPattern(null);
        }
        else if (next!=null)
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