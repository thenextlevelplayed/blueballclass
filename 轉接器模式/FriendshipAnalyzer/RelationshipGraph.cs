using QuikGraph;
using QuikGraph.Algorithms.ConnectedComponents;

namespace FriendshipAnalyzer;

public interface RelationshipGraph
{
    bool HasConnection(string name1, string name2);
}

public class QuickGraphRelationshipGraph : RelationshipGraph
{
    private readonly IDictionary<string, int> _dictionary;

    public QuickGraphRelationshipGraph(IDictionary<string, int> dictionary)
    {
        _dictionary = dictionary;
    }

    public bool HasConnection(string name1, string name2)
    {
        try
        {
            if (_dictionary[name1] == _dictionary[name2])
            {
                Console.WriteLine("有關聯");
                return true;
            }

            Console.WriteLine("沒有關聯");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e}, 查無此人");
            return false;
        }
    }
}
