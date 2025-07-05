namespace Template_Method.Challenge_3.TemplateGame;

public class UNOCard : Card<object, object>
{
    public UNOCard(UNO_Number number, UNO_Color color) : base(number, color)
    {
    }
    public UNO_Number Number => (UNO_Number)Attribute1;
    public UNO_Color Color => (UNO_Color)Attribute2;
}