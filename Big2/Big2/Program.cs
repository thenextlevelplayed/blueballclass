// See https://aka.ms/new-console-template for more information

using Big2;
using Big2.CompareManyCardPattern;
using Big2.DistinguishingCardPattern;
using Big2.DistinguishingGameAndPlayerStatus;

string testInput = Console.ReadLine();
Deck deck = new Deck(testInput);
Game game = new Game(deck,
    new BeginToPlayRoundAction(
        new DistinguishingFullHouseCardPattern(
            new DistinguishingStraightCardPattern(
                new DistinguishingPairCardPattern(new DistinguishingSingleCardPattern(null)))),
        new FirstRoundAction(
            new DistinguishingFullHouseCardPattern(
                new DistinguishingStraightCardPattern(
                    new DistinguishingPairCardPattern(new DistinguishingSingleCardPattern(null)))),
            new PlayRoundAction(
                new DistinguishingFullHouseCardPattern(
                    new DistinguishingStraightCardPattern(
                        new DistinguishingPairCardPattern(new DistinguishingSingleCardPattern(null)))), null,
                new CompareFullHouseCardPattern(
                    new CompareStraightCardPattern(new ComparePairCardPattern(new CompareSingleCardPattern(null))))),
            new CompareFullHouseCardPattern(
                new CompareStraightCardPattern(new ComparePairCardPattern(new CompareSingleCardPattern(null))))),
        new CompareFullHouseCardPattern(
            new CompareStraightCardPattern(new ComparePairCardPattern(new CompareSingleCardPattern(null))))));
game.GameStart();

//測試
// dotnet run<test\always-play-first-card.in > test\always-play-first-card.out
// dotnet run<test\fullhouse.in > test\fullhouse.out
// dotnet run<test\illegal-actions.in > test\illegal-actions.out
// dotnet run<test\normal-no-error-play1.in > test\normal-no-error-play1.out
// dotnet run<test\normal-no-error-play2.in > test\normal-no-error-play2.out
// dotnet run<test\straight.in > test\straight.out