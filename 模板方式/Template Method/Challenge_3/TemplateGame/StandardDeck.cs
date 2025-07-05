using Template_Method.Challenge_3.UNO;

namespace Template_Method.Challenge_3.TemplateGame;

public class StandardDeck<TCard> : Deck<TCard> where TCard : Card<object, object>
{
    public StandardDeck(List<TCard>? cards = null) : base(cards ?? new List<TCard>())
    {
    }

    public override void AddCard(TCard card)
    {
        if (card != null)
        {
            _cards.Add(card);
        }
    }
}