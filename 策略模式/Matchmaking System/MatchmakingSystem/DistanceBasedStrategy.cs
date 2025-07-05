using System.Drawing;

namespace MatchmakingSystem;

public class DistanceBasedStrategy : IMatchmakingStrategy
{
    public string Desc { get; set; }
    public string Asc { get; set; }

    public Dictionary<Individual, double> Match(Individual p1, List<Individual> individuals)
    {
        Dictionary<Individual, double> distanceBasedDictionary = new Dictionary<Individual, double>();
        foreach (var p2 in individuals)
        {
            if (!p2.Equals(p1)) //不能匹配自己
            {
                distanceBasedDictionary.Add(p2, CalculateDistance(p1.Coord, p2.Coord));
            }
        }

        Console.WriteLine("使用距離先決");
        return distanceBasedDictionary;
    }

    private double CalculateDistance(Point p1, Point p2)
    {
        return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }
}