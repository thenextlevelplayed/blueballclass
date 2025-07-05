using Big2.DistinguishingCardPattern;

namespace Big2;

public abstract class CompareCardPattern
{
    private CompareCardPattern? next;

    public CompareCardPattern(CompareCardPattern? next)
    {
        this.next = next;
    }

    public bool TemplateCompare(Dictionary<CardPattern, List<Card>> topPlay, CardPattern? currentPattern,
        List<Card> currentPlay)
    {
        if (currentPattern?.GetType() == GetCardPattern().GetType())
        {
            return CompareAction(topPlay, currentPlay);
        }
        else
        {
            if (next != null)
            {
                return next.TemplateCompare(topPlay, currentPattern, currentPlay);
            }

            return false;
        }
    }

    protected abstract CardPattern GetCardPattern();

    protected abstract bool CompareAction(Dictionary<CardPattern, List<Card>> topPlay, List<Card> currentPlay);
}

// parameter1: Dictionary<CardPattern, List<Card>> topPlay  parameter2: List<Card> currentPlay 改成模板方法