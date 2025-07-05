using System.Drawing;

namespace MatchmakingSystem;

public class Individual
{
    public int Id { get; private set; }
    public string Gender { get; private set; }
    public int Age { get; private set; }
    public string Intro { get; private set; }
    public string Habits { get; private set; }
    public Point Coord { get; private set; }

    private static readonly HashSet<int> Ids = new HashSet<int>();

    public Individual(int id, string gender, int age, string intro, string habits, Point coord)
    {
        Id = SetId(id);
        Gender = gender == "Male" ? "Male" : "Female";
        Age = age >= 18 ? age : throw new Exception("Age must be 18 or older");
        Intro = intro.Length <= 180 ? intro : throw new Exception("Intro length must be less than 181 characters");
        Habits = SetHabits(habits);
        Coord = coord;
    }
    
    // 驗證 Habits
    private string SetHabits(string habits)
    {
        string[] habitArray = habits.Split(",");
        foreach (string habit in habitArray)
        {
            if (habit.Length < 1 || habit.Length > 10)
            {
                throw new Exception("Each habit must be between 1 and 10 characters long");
            }
        }

        return habits;
    }

    private int SetId(int id)
    {
        if (id <= 0)
        {
            throw new Exception("Id must be greater than 0");
        }

        if (!Ids.Add(id))
        {
            throw new Exception("Id must be unique; this ID is already in use\"");
        }

        return id;
    }
    // private System System { get; set; }
    // public string GetMatch()
    // {
    //     var p2 =this.MatchmakingSystem.Match(this);
    //     var p2Info = $"{p2.Age} {p2.Habits} {p2.Id} {p2.Intro} 距離:{p2.Coord.X} {p2.Coord.Y}";
    //     return p2Info;
    // }
}