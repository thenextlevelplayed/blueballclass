namespace MatchmakingSystem;

public interface ISorter
{
    Individual Sort(Dictionary<Individual, double> dictionary);
}