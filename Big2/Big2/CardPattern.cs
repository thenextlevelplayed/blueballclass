using Big2.DistinguishingCardPattern;

namespace Big2;

public abstract class CardPattern
{
    protected CardPattern next;
    public string Name { get; set; }

    public CardPattern(CardPattern next)
    {
        this.next = next;
        SetName();
    }

    public CardPattern TemplateDistinguishingCardPattern(List<Card>? cards)
    {
        if (IsCardPattern(cards))
        {
            return Result();
        }
        else if (next != null)
        {
            return next.TemplateDistinguishingCardPattern(cards);
        }
        else
        {
            return null;
        }
        
        
    }

    // public abstract CardPattern DistinguishingCardPattern(List<Card>? cards);
    
    protected abstract bool IsCardPattern(List<Card>? cards);

    protected abstract CardPattern Result();

    private void SetName()
    {
        this.Name = NameItSelf();
    }

    protected abstract string NameItSelf();
    
    //equal method
    // 覆寫 Equals 方法以包含子類別特定的比較
    public virtual bool Equals(object obj)
    {
        // 1. 檢查參考是否相同
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        // 2. 檢查 obj 是否為 null 且類型是否為 CardPattern
        if (obj is not CardPattern otherPattern)
        {
            return false;
        }

        // 3. 比較 Name 屬性
        return string.Equals(Name, otherPattern.Name);
    }

    // 覆寫 GetHashCode 方法，確保與 Equals 方法一致
    public override int GetHashCode()
    {
        // 結合父類別的 HashCode 和子類別特有的屬性的 HashCode（如果有的話）
        return HashCode.Combine(Name);
    }

}