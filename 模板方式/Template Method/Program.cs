using Template_Method.Challenge_3.TemplateGame;


// UNO 測試
var unoAI1 = new UNOAIPlayer("AI1", new List<UNOCard>());
var unoAI2 = new UNOAIPlayer("AI2", new List<UNOCard>());
var unoAI3 = new UNOAIPlayer("AI3", new List<UNOCard>());
var unoHuman = new UNOHumanPlayer("Bob", new List<UNOCard>());
List<Player<UNOCard>> UNOPlayers = new List<Player<UNOCard>>
{
    unoAI1, unoAI2, unoAI3, unoHuman
};
//game start!

var uno = new UNOGame(UNOPlayers);
uno.TemplateGameStart();


Console.WriteLine("\n---\n");

// Showdown 測試
var showdownAI1 = new ShowdownAIPlayer("AI1", new List<ShowdownCard>());
var showdownAI2 = new ShowdownHumanPlayer("Alice", new List<ShowdownCard>());
var showdownAI3 = new ShowdownAIPlayer("AI3", new List<ShowdownCard>());
var showdownHuman = new ShowdownHumanPlayer("Alice", new List<ShowdownCard>());
List<Player<ShowdownCard>> ShowdownPlayers =
    new List<Player<ShowdownCard>>
    {
        showdownAI1, showdownAI2, showdownAI3, showdownHuman
    };
//game start!
var showdown = new ShowdownGame(ShowdownPlayers);
showdown.TemplateGameStart();