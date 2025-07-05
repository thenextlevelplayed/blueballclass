namespace MatchmakingSystem;

public interface IMatchmakingStrategy
{
    public Dictionary<Individual, double> Match(Individual p1,List<Individual> individuals);
}