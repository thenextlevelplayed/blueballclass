using Big2.DistinguishingCardPattern;

namespace Big2.CompareManyCardPattern;

public class CompareSingleCardPattern(CompareCardPattern? next) : CompareCardPattern(next)
{
    protected override CardPattern GetCardPattern()
    {
        return new DistinguishingSingleCardPattern(null);
    }

    protected override bool CompareAction(Dictionary<CardPattern, List<Card>> topPlay,List<Card> currentPlay)
    {
        return currentPlay.Last() > topPlay.First().Value.Last();
    }
}
/*
 public override bool Compare(Dictionary<CardPattern, List<Card>> topPlay, CardPattern? currentPattern,
        List<Card> currentPlay)
    {
        if (currentPattern?.GetType() == typeof(DistinguishingSingleCardPattern))
        {
            return currentPlay.Last() > topPlay.First().Value.Last();
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