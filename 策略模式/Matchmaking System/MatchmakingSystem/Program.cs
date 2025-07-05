using System.Drawing;
using MatchmakingSystem;

var p1 = new Individual(1, "Male", 18, "123", "吃飯,睡覺,132465789", new Point(10, 20));
var p2 = new Individual(2, "Female", 25, "123", "吃飯,睡覺,看電影,主菜,打電腦,打籃球,打排球", new Point(50, 1000));
var p3 = new Individual(3, "Female", 36, "123", "主菜,打電腦", new Point(50, 120));
var p4 = new Individual(4, "Female", 18, "123", "吃飯,睡覺,打排球", new Point(50, 100));

List<Individual> individuals = new List<Individual>()
{
    p1, p2, p3, p4
};

MatchmakingSystem.System habitSystem =
    new MatchmakingSystem.System(new HabitBasedStrategy(), individuals, new AscendingSorter());
MatchmakingSystem.System distanceSystem =
    new MatchmakingSystem.System(new DistanceBasedStrategy(), individuals, new DescendingSorter());
habitSystem.StartMatch(p1);
distanceSystem.StartMatch(p1);

habitSystem.StartMatch(p3);
distanceSystem.StartMatch(p3);


//old code
// var p1Matchplayer = matchmakingSystem.Match(p1);
// matchmakingSystem.ShowPlayerMatchInfo(p1,p1Matchplayer);
//
// matchmakingSystem = new MatchmakingSystem.MatchmakingSystem(1,Individuals);
// var p2Matchplayer = matchmakingSystem.Match(p2);
// matchmakingSystem.ShowPlayerMatchInfo(p2,p2Matchplayer);
// var p4Matchplayer = matchmakingSystem.Match(p4);
// matchmakingSystem.ShowPlayerMatchInfo(p4,p4Matchplayer);
// p1.GetMatch();
// matchmakingSystem.Match();