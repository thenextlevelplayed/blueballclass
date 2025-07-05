namespace MatchmakingSystem;

public class DescendingSorter : ISorter
{
    public Individual Sort(Dictionary<Individual, double> dictionary)
    {
        Console.WriteLine("匹配最大值");
        return dictionary.OrderByDescending(v => v.Value).ThenBy(k => k.Key.Id).First().Key;
    }
}