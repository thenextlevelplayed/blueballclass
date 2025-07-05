namespace MatchmakingSystem;

public class HabitBasedStrategy : IMatchmakingStrategy
{
    public string Desc { get; set; }
    public string Asc { get; set; }

    public Dictionary<Individual, double> Match(Individual p1, List<Individual> individuals)
    {
        List<String> p1Habits = new List<String>();
        Dictionary<Individual, double> habitsDictionary = new Dictionary<Individual, double>();

        p1.Habits.Split(',').ToList().ForEach(habit => p1Habits.Add(habit));
        foreach (var p2 in individuals)
        {
            if (!p1.Equals(p2)) //不能匹配自己
            {
                int count = 0; //最大交集量
                var p2Habits = p2.Habits.Split(',').ToList();
                p1Habits.ForEach(delegate(string habit)
                {
                    if (p2Habits.Contains(habit))
                    {
                        count++;
                    }
                });
                habitsDictionary.Add(p2, count);
            }
        }

        Console.WriteLine("使用興趣先決");
        return habitsDictionary;
    }

    // public void Match(Individual p1)
    // {
    //     List<String> p1Habits = new List<String>();
    //     Dictionary<Individual,int>  habitsDictionary = new Dictionary<Individual,int>();
    //     
    //     p1.Habits.Split(',').ToList().ForEach(habit => p1Habits.Add(habit));
    //     foreach (var p2 in Individuals)
    //     {
    //         if (!p1.Equals(p2)) //不能匹配自己
    //         {
    //             int count = 0; //最大交集量
    //             var p2Habits = p2.Habits.Split(',').ToList();
    //             p1Habits.ForEach(delegate(string habit)
    //             {
    //                 if (p2Habits.Contains(habit))
    //                 {
    //                     count++;
    //                 }
    //             });
    //             habitsDictionary.Add(p2, count);
    //         }
    //         
    //     }
    //     var abc =  habitsDictionary.OrderBy(count => count.Value).ThenBy(distance => distance.Key.Id);
    //     var matchedPlayer = abc.First().Key;
    // }
}