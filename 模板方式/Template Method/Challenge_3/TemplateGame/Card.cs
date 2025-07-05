namespace Template_Method.Challenge_3.TemplateGame;

public class Card<T1, T2>
{
    // 屬性：使用外部定義的 Suit 和 Rank 列舉
    public T1 Attribute1 { get; }
    public T2 Attribute2 { get; }

    public Card(T1 attribute1, T2 attribute2)
    {
        Attribute1 = attribute1;
        Attribute2 = attribute2;
    }

    public override string ToString()
    {
        return $"{Attribute1} of {Attribute2}";
    }
}