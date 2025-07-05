using System.Diagnostics;
using Showdown;
class Program
{
    static void Main(string[] args)
    {


        // 1. 建立  遊戲物件
        Deck deck = new Deck();
        // 2. 建立玩家列表
        List<Player> players = new List<Player>
            {
                new AI("AI1", new Hand(), 0, null, 0,deck),
                new AI("AI2", new Hand(), 0, null, 0,deck),
                new AI("AI3", new Hand(), 0, null, 0,deck),
                new Human("Human", new Hand(), 0, null, 0,deck)
            };
        Game game = new Game(deck, players);
        //deck = deck.CreateDeck();
        //deck.CreateDeck();
        //deck.Shuffle();

        for (int i = 0; i < players.Count(); i++)
        {
            Console.WriteLine($"Player {i + 1} (P{i + 1}):");
            players[i].NameHimSelf(i + 1);
        }

        // 4. 開始遊戲
        game.StartGame();
    }
}