using System;
using System.Collections.Generic;
using System.Linq;

namespace UnoGame
{
    public enum CardColor { Red, Green, Blue, Yellow, Wild }
    public enum ActionType { Skip, DrawTwo, Wild, WildDrawFour }

    public abstract class Card
    {
        public CardColor Color { get; set; }
        public abstract void PlayCard(Game game, Player player);
        public abstract override string ToString();
    }

    public class NumberCard : Card
    {
        public int Value { get; set; }
        public NumberCard(CardColor color, int value) { Color = color; Value = value; }
        public override void PlayCard(Game game, Player player) { game.LastPlayedCard = this; }
        public override string ToString() => $"{Color} {Value}";
    }

    public class ActionCard : Card
    {
        public ActionType Action { get; }
        public ActionCard(CardColor color, ActionType action) { Color = color; Action = action; }
        public override void PlayCard(Game game, Player player)
        {
            game.LastPlayedCard = this;
            game.ApplyAction(Action, player);
        }
        public override string ToString() => $"{Color} {Action}";
    }

    public class Deck
    {
        public List<Card> Cards { get; private set; } = new List<Card>();
        public void GenerateDeck()
        {
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                if (color == CardColor.Wild) continue;
                for (int i = 0; i <= 9; i++)
                {
                    Cards.Add(new NumberCard(color, i));
                    if (i != 0) Cards.Add(new NumberCard(color, i));
                }
                Cards.Add(new ActionCard(color, ActionType.Skip));
                Cards.Add(new ActionCard(color, ActionType.Skip));
                Cards.Add(new ActionCard(color, ActionType.DrawTwo));
                Cards.Add(new ActionCard(color, ActionType.DrawTwo));
            }
            for (int i = 0; i < 4; i++)
            {
                Cards.Add(new ActionCard(CardColor.Wild, ActionType.Wild));
                Cards.Add(new ActionCard(CardColor.Wild, ActionType.WildDrawFour));
            }
        }
        public void Shuffle() => Cards = Cards.OrderBy(_ => Guid.NewGuid()).ToList();
        public Card DrawCard() { var card = Cards[0]; Cards.RemoveAt(0); return card; }
    }

    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; } = new List<Card>();
        public bool IsAI { get; set; }
        public Player(string name, bool isAI = false) { Name = name; IsAI = isAI; }
        public void DrawCard(Deck deck) => Hand.Add(deck.DrawCard());
        public void ShowHand() { if (!IsAI) Console.WriteLine($"{Name}'s hand: {string.Join(", ", Hand)}"); }
        public bool ShouldSayUNO() => IsAI ? new Random().NextDouble() < 0.7 : AskForUNO();
        private bool AskForUNO()
        {
            Console.WriteLine("You have one card left! Type 'UNO' or receive a penalty card.");
            return Console.ReadLine()?.Trim().ToUpper() == "UNO";
        }
        public Card ChooseCard(Game game)
        {
            if (IsAI) return Hand.FirstOrDefault(game.IsValidMove) ?? null;
            Card chosenCard = null;
            while (chosenCard == null)
            {
                Console.WriteLine("Enter the card to play (e.g., 'Red 5' or 'Wild Blue') or type 'draw':");
                var input = Console.ReadLine();
                if (input?.ToLower() == "draw") return null;
                chosenCard = Hand.Find(card => card.ToString().Equals(input, StringComparison.OrdinalIgnoreCase));
                if (chosenCard == null) Console.WriteLine("Invalid card, try again.");
            }
            return chosenCard;
        }
    }

    public class Game
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public Deck GameDeck { get; set; } = new Deck();
        public Card LastPlayedCard { get; set; }
        public int CurrentPlayerIndex { get; set; } = 0;

        public void DisplayRules()
        {
            Console.WriteLine("UNO Game Rules:");
            Console.WriteLine("- Number cards (0-9): Can be played if the color matches the last played card or if the number matches.");
            Console.WriteLine("- Skip card: Skips the next player's turn.");
            Console.WriteLine("- Draw Two card: The next player draws two cards and loses their turn.");
            Console.WriteLine("- Wild card: Allows the player to choose any color by typing 'Wild' followed by the chosen color (e.g., 'Wild Blue').");
            Console.WriteLine("- Wild Draw Four card: Allows the player to choose any color and the next player draws four cards (can only be played if you have no matching color cards).");
            Console.WriteLine("- Say 'UNO' when you have one card left, or draw a penalty card if caught.");
            Console.WriteLine("- The first card cannot be a special card. If a special card is drawn first, another card will be drawn.");

        }

        public bool IsValidMove(Card card) =>
            card.Color == LastPlayedCard.Color ||
            (card is NumberCard n && LastPlayedCard is NumberCard l && n.Value == l.Value) ||
            card.Color == CardColor.Wild;

        public void ApplyAction(ActionType action, Player player)
        {
            switch (action)
            {
                case ActionType.Skip:
                    CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
                    break;
                case ActionType.DrawTwo:
                    Players[(CurrentPlayerIndex + 1) % Players.Count].DrawCard(GameDeck);
                    Players[(CurrentPlayerIndex + 1) % Players.Count].DrawCard(GameDeck);
                    break;
                case ActionType.Wild:
                case ActionType.WildDrawFour:
                    Console.WriteLine("Choose a color: Red, Green, Blue, Yellow");
                    string chosenColor = Console.ReadLine();
                    if (!Enum.TryParse(chosenColor, true, out CardColor newColor) || newColor == CardColor.Wild)
                        newColor = CardColor.Red;
                    LastPlayedCard = new NumberCard(newColor, -1);
                    if (action == ActionType.WildDrawFour)
                        for (int i = 0; i < 4; i++) Players[(CurrentPlayerIndex + 1) % Players.Count].DrawCard(GameDeck);
                    break;
            }
        }

        public void RunGameLoop()
        {
            DisplayRules();
            GameDeck.GenerateDeck();
            GameDeck.Shuffle();
            do { LastPlayedCard = GameDeck.DrawCard(); } while (LastPlayedCard is ActionCard);
            Console.WriteLine($"First card: {LastPlayedCard}");

            while (Players.All(p => p.Hand.Count > 0))
            {
                var player = Players[CurrentPlayerIndex];
                player.ShowHand();
                var card = player.ChooseCard(this);
                if (card != null && IsValidMove(card))
                {
                    player.Hand.Remove(card);
                    card.PlayCard(this, player);
                    if (player.Hand.Count == 0)
                    {
                        Console.WriteLine($"{player.Name} wins the game!");
                        return;
                    }
                }
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
            }
        }
    }
}