namespace Big2.DistinguishingCardPattern;

public class DistinguishingFullHouseCardPattern(CardPattern next) : CardPattern(next)
{
    protected override bool IsCardPattern(List<Card>? cards)
    {
        if (cards?.Count != 5)
        {
            return false;
        }

        Dictionary<int, int> dict = new();
        foreach (Card card in cards)
        {
            int rank = (int)card.Rank;
            dict[rank] = dict.GetValueOrDefault(rank, 0) + 1;
        }

        return dict.Count == 2 &&
               dict.Values.Contains(3) &&
               dict.Values.Contains(2);
    }

    protected override CardPattern Result()
    {
        return new DistinguishingFullHouseCardPattern(null);
    }

    protected override string NameItSelf()
    {
        return "葫蘆 ";
    }

    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return true;
        }

        if (obj is not DistinguishingFullHouseCardPattern otherPattern)
        {
            return false;
        }

        
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), NameItSelf());
    }
}
/*
public override CardPattern DistinguishingCardPattern(List<Card>? cards)
{
    if (cards?.Count != 5)
    {
        return next?.DistinguishingCardPattern(cards) ?? null;
    }

    Dictionary<int, int> dict = new();
    foreach (Card card in cards)
    {
        int rank = (int)card.Rank;
        dict[rank] = dict.GetValueOrDefault(rank, 0) + 1;
    }

    bool isFullHouseCardPattern = dict.Count == 2 &&
                                  dict.Values.Contains(3) &&
                                  dict.Values.Contains(2);


    if (isFullHouseCardPattern) //Straight
    {
        return new DistinguishingFullHouseCardPattern(null);
    }
    else if (next != null)
    {
        return next.DistinguishingCardPattern(cards);
    }
    else
    {
        return null;
    }
}
使用樣板方式取代
*/