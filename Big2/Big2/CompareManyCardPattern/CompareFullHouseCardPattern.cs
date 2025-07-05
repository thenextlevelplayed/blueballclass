using Big2.DistinguishingCardPattern;

namespace Big2.CompareManyCardPattern;

public class CompareFullHouseCardPattern(CompareCardPattern? next) : CompareCardPattern(next)
{
    protected override CardPattern GetCardPattern()
    {
        return new DistinguishingFullHouseCardPattern(null);
    }

    protected override bool CompareAction(Dictionary<CardPattern, List<Card>> topPlay,List<Card> currentPlay)
    {
        return BiggestCardNumber(currentPlay) > BiggestCardNumber(topPlay.Values.First());
    }

    private int BiggestCardNumber(List<Card> cards)
    {
        Dictionary<int, int> dict = new();
        foreach (Card card in cards)
        {
            int rank = (int)card.Rank;
            dict[rank] = dict.GetValueOrDefault(rank, 0) + 1;
        }

        return dict.FirstOrDefault(x => x.Value == 3).Key;
    }
}
/*
public override bool Compare(Dictionary<CardPattern, List<Card>> topPlay, CardPattern? currentPattern,
        List<Card> currentPlay)
    {
        if (currentPattern.GetType() == typeof(DistinguishingFullHouseCardPattern))
        {
            return BiggestCardNumber(currentPlay) > BiggestCardNumber(topPlay.Values.First());
        }
        else
        {
            if (next != null)
            {
                return next.Compare(topPlay, currentPattern, currentPlay);
            }

            return false;
        }
    }
    改成模板方法
 */