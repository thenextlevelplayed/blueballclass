using QuikGraph;
using QuikGraph.Algorithms.ConnectedComponents;

namespace FriendshipAnalyzer.基礎;

public interface RelationshipAnalyzer
{
    RelationshipGraph Parse(string script);
    List<string> GetMutualFriends(string name1, string name2);
}

public class AnalyzerAdapter : RelationshipAnalyzer
{
    private SuperRelationshipAnalyzer _superRelationshipAnalyzer { get; set; } = new SuperRelationshipAnalyzer();
    private HashSet<string> _allPeople = new HashSet<string>();

    public RelationshipGraph Parse(string script)
    {
        var superRelationshipAnalyzerString = "";
        var graph = new UndirectedGraph<string, Edge<string>>();
        var lines = script.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine(lines);
        foreach (var line in lines)
        {
            var parts = line.Split(new[] { ":" }, StringSplitOptions.None);
            if (parts.Length != 2) continue;
            var key = parts[0].Trim();
            _allPeople.Add(key);
            graph.AddVertex(key); //魔王題目
            var manyName = parts[1].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var value in manyName)
            {
                string friendName = value.Trim();
                if (string.IsNullOrEmpty(friendName)) continue;
                _allPeople.Add(friendName);
                superRelationshipAnalyzerString += key + " -- " + friendName + "\r\n";
                //魔王題目
                graph.AddVertex(friendName);
                graph.AddEdge(new Edge<string>(key, friendName));
            }
        }
        var connection = new ConnectedComponentsAlgorithm<string, Edge<string>>(graph);
        connection.Compute();
        var dict = connection.Components;
        return new QuickGraphRelationshipGraph(dict);
    }

    public List<string> GetMutualFriends(string name1, string name2)
    {
        List<string> mutalFriends = new List<string>();
        foreach (var targetName in _allPeople)
        {
            if (targetName == name1 || targetName == name2)
            {
                continue;
            }
            if (_superRelationshipAnalyzer.IsMutualFriend(targetName, name1, name2))
            {
                mutalFriends.Add(targetName);
            }
        }
        return mutalFriends;
    }
}