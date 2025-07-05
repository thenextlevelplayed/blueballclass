namespace Template_Method.Challenge_3.TemplateGame;

public interface ITempleteGame<TCard> where TCard : Card<object, object>
{
    TCard PlaceCard { get; set; }
    void DrawCard(Player<TCard> player);
    List<TCard> Show(Player<TCard> player);
    void PrintWinner(Player<TCard> player);
}