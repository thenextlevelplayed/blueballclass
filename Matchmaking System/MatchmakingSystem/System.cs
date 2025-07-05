using System.Drawing;

namespace MatchmakingSystem;

public class System
{
    private List<Individual> Individuals { get; set; } = new List<Individual>();
    private readonly IMatchmakingStrategy _matchmakingStrategy;
    private readonly ISorter _sorter;

    public System(IMatchmakingStrategy strategy, List<Individual> individuals, ISorter sorter)
    {
        Individuals = SetIndividuals(individuals);
        _matchmakingStrategy = strategy;
        _sorter = sorter;
    }

    public void StartMatch(Individual individual)
    {
        Console.WriteLine("開始匹配:");
        var matches = _matchmakingStrategy.Match(individual, Individuals);
        if (matches.Count == 0)
        {
            Console.WriteLine("沒有可配對的對象");
            return;
        }
        var matchedPlayer = _sorter.Sort(matches);

        var result =
            $"玩家ID:{individual.Id} 匹配到: 年齡:{matchedPlayer.Age} 興趣:{matchedPlayer.Habits} ID:{matchedPlayer.Id} 自我介紹:{matchedPlayer.Intro} 距離:{matchedPlayer.Coord.X} {matchedPlayer.Coord.Y}";
        Console.WriteLine(result);
    }

    public List<Individual> SetIndividuals(List<Individual> Individuals)
    {
        if (Individuals.Count <= 2)
        {
            throw new Exception("Must have at least 2 individual");
        }

        return Individuals;
    }

    // private int SelectStrategy { get; set; }
    //原本的程使馬
    // public Individual Match(Individual p1) 
    // {
    //     switch (SelectStrategy) 
    //     {
    //         case 0: 
    //             // do something DistanceBasedStrategy()
    //             Dictionary<Individual,double>  distanceBasedDictionary = new Dictionary<Individual, double>();
    //             foreach (var p2 in Individuals)
    //             {
    //                 if (!p2.Equals(p1))//不能匹配自己
    //                 {
    //                     distanceBasedDictionary.Add(p2, CalculateDistance(p1.Coord, p2.Coord));
    //                 }
    //             }
    //             return distanceBasedDictionary.OrderBy(distance => distance.Value).ThenBy(distance => distance.Key.Id).First().Key;;
    //            
    //         case 1:
    //             //HabitBasedStrategy
    //             List<String> p1Habits = new List<String>();
    //             Dictionary<Individual,int>  habitsDictionary = new Dictionary<Individual,int>();
    //
    //             p1.Habits.Split(',').ToList().ForEach(habit => p1Habits.Add(habit));
    //             foreach (var p2 in Individuals)
    //             {
    //                 if (!p1.Equals(p2)) //不能匹配自己
    //                 {
    //                     int count = 0; //最大交集量
    //                     var p2Habits = p2.Habits.Split(',').ToList();
    //                     p1Habits.ForEach(delegate(string habit)
    //                     {
    //                         if (p2Habits.Contains(habit))
    //                         {
    //                             count++;
    //                         }
    //                     });
    //                     habitsDictionary.Add(p2, count);
    //                 }
    //                 
    //             }
    //             // habitsDictionary.OrderBy(count => count.Value).ThenBy(distance => distance.Key.Id);
    //             Console.WriteLine(p1Habits.Count + " habits");
    //             
    //
    //             return habitsDictionary.OrderByDescending(count => count.Value).ThenBy(distance => distance.Key.Id).First().Key;
    //         default:
    //             return null;
    //     }
    // }

    // private double CalculateDistance(Point p1, Point p2)
    // {
    //     double x = (p1.X -= p2.X) * (p1.X -= p2.X);
    //     double y = (p1.Y -= p2.Y) * (p1.Y -= p2.Y);
    //     return Math.Sqrt(x + y);
    // }
    //
    // public void ShowPlayerMatchInfo(Individual p1,Individual p2)
    // {
    //     
    //     var p2Info = $"玩家ID:{p1.Id} 匹配到: 年齡:{p2.Age} 興趣:{p2.Habits} ID:{p2.Id} 自我介紹:{p2.Intro} 距離:{p2.Coord.X} {p2.Coord.Y}";
    //     Console.WriteLine(p2Info);
    // }
}