namespace Big2.DistinguishingCardPattern;

public class DistinguishingSingleCardPattern(CardPattern? next) : CardPattern(next)
{
    protected override bool IsCardPattern(List<Card>? cards)
    {
        return cards?.Count == 1;
    }

    protected override CardPattern Result()
    {
        return new DistinguishingSingleCardPattern(null);
    }

    protected override string NameItSelf()
    {
        return "單張 ";
    }

    // *** 覆寫 Equals 方法 ***
    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }

        if (obj is not DistinguishingSingleCardPattern otherPattern)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), NameItSelf());
    }

    // *** 覆寫 == 和 != 運算子 (可選但推薦) ***
    // 為了讓 == 和 != 運算子也能執行值比較，通常會連同覆寫這兩個運算子。
    // 這裡利用前面寫好的 Equals 方法。
    public static bool operator ==(DistinguishingSingleCardPattern left, DistinguishingSingleCardPattern right)
    {
        // Handle null on the left side
        if (ReferenceEquals(left, null))
        {
            return ReferenceEquals(right, null); // left is null, return true if right is null
        }

        // Equals handles case where right is null
        return left.Equals(right); // Use the Equals method we just implemented
    }

    public static bool operator !=(DistinguishingSingleCardPattern left, DistinguishingSingleCardPattern right)
    {
        return !(left == right); // Just negate the == operator
    }
}
/*
 *  public override CardPattern DistinguishingCardPattern(List<Card>? cards)
    {
        bool isSingleCardPattern = cards?.Count == 1;
        if (isSingleCardPattern) //Single
        {
            return new DistinguishingSingleCardPattern(null);
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
    套樣板方式
 */