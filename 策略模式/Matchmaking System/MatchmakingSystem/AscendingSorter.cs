namespace MatchmakingSystem;

public class AscendingSorter : ISorter
{
    public Individual Sort(Dictionary<Individual, double> dictionary)
    {
        Console.WriteLine("匹配最小值");
        return dictionary.OrderBy(v => v.Value).ThenBy(k => k.Key.Id).First().Key;
    }
}