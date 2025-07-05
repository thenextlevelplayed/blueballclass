namespace Template_Method.Challenge_3.Showdown;

public class Card
{
    // 屬性：使用外部定義的 Suit 和 Rank 列舉
    public UNO_Color UnoColor { get; set; }
    public UNO_Number UnoNumber { get; set; }

    // 建構子：初始化一張牌的花色和階級
    public Card(UNO_Color unoColor, UNO_Number unoNumber)
    {
        UnoColor = unoColor;
        UnoNumber = unoNumber;
    }

    // （可選）覆寫 ToString 方法，方便顯示牌的內容
    public override string ToString()
    {
        return $"{UnoNumber} of {UnoColor}";
    }
}