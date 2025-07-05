namespace Showdown
{
    public class Game
    {
        private readonly List<Player> players;
        private readonly Deck deck;

        public Game(Deck deck, List<Player> players)
        {
            this.deck = deck;
            this.players = players;
        }

        private Dictionary<Player, Card> playersCard { get; set; } = new Dictionary<Player, Card>();

        public void GetPlayerShowCard(Player player, Card card)
        {
            playersCard.Add(player, card);
            Console.WriteLine($"Player: {player.name} show {card.ToString()}");
        }

        public (Player?, Card?) CompareCard()
        {
            if (!playersCard.Any()) return (null, null);

            var maxEntry = playersCard
                .OrderByDescending(entry => entry.Value.Rank)
                .ThenByDescending(entry => entry.Value.Suit)
                .First();
            Console.WriteLine($"Player: {maxEntry.Key.name} has a biggest card => {maxEntry.Value.ToString()}");
            return (maxEntry.Key, maxEntry.Value);
        }

        public void SetPoint()
        {
            Player? player = CompareCard().Item1;
            if (player != null)
            {
                player.GivePoint();
            }
        }

        public void Winner()
        {
            PrintWinner(players.OrderByDescending(p => p.point).First());
        }

        public void PrintWinner(Player player)
        {
            Console.WriteLine($"Winner is {player.name}");
        }

        public void PlayersDrawCard() //輪流抽牌 
        {
            for (int round = 1; round <= 13; round++)
            {
                foreach (var player in players)
                {
                    player.DrawCard();
                }
            }
        }

        public void StartGame()
        {
            deck.CreateDeck();
            deck.Shuffle();

            for (int round = 1; round <= 13; round++)
            {
                foreach (var player in players)
                {
                    player.DrawCard();
                }
            }

            for (int round = 1; round <= 13; round++)
            {
                Console.WriteLine($"Round{round}");
                foreach (var player in players)
                {
                    if (player.exchangePartner != null && player.countRound > 0) //如果已經交換過手牌
                    {
                        player.EndExchange();
                    }

                    if (player.exchangePartner == null && player.countRound == 0) //如果沒有交換過手牌
                    {
                        player.TryExchange(players);
                    }
                    Card? card = player.Show();
                    if (card != null)
                    {
                        GetPlayerShowCard(player, card);
                    }
                }
                SetPoint();
                playersCard.Clear();// reset                
            }
            Winner();
        }
    }
}