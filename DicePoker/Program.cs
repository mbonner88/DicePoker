using System.Text.RegularExpressions;

namespace DicePoker;
class Program
{
    public static void Main(string[] args)
    {
        var player = new DicePlayer();
        var opponent = new DiceOpponent();
        Game(player, opponent);
    }

    public static void Game(DicePlayer player, DiceOpponent opponent)
    {
        //TODO: add some Thread.Sleep
        var rnd = new Random();
        int pool = 0;

        pool = DiceDealer.Welcome(player, pool);
        Console.WriteLine("Press enter to roll the dice.");
        Console.ReadKey();
        Console.WriteLine("Here is your hand...");
        
        player.PlayerDice = DiceDealer.RollDice(rnd).ToArray();
        player.PlayerHand = DiceChecker.CheckDice(player.PlayerDice);
        DiceChecker.PrintDice(player.PlayerDice);
        Console.WriteLine();

        Console.WriteLine("And your opponent's hand...");
        opponent.OpponentDice = DiceDealer.RollDice(rnd).ToArray();
        opponent.OpponentHand = DiceChecker.CheckDice(opponent.OpponentDice);
        DiceChecker.PrintDice(opponent.OpponentDice);
        Console.WriteLine();

        DiceChecker.PrintHands(player.PlayerHand, opponent.OpponentHand);
        //second bet after the reroll rather than before?
        DiceDealer.BettingPrompt(player, ref pool);
        
        DiceDealer.RerollPrompt(player);

        Console.WriteLine("Opponent's turn...");
        opponent.OpponentDice = DiceOpponent.OpponentReroll(opponent.OpponentDice, opponent.OpponentHand);
        opponent.OpponentHand = DiceChecker.CheckDice(opponent.OpponentDice);

        DiceChecker.PrintHands(player.PlayerHand, opponent.OpponentHand);
        //write a method for dicechecker.checkhands?
        if ((int)player.PlayerHand > (int)opponent.OpponentHand)
        {
            player.PlayerWins++;
            player.Coins += pool;
            Console.WriteLine($"You've won! {pool} coins added!");
        }
        else if ((int)player.PlayerHand < (int)opponent.OpponentHand)
        {
            opponent.OpponentWins++;
            Console.WriteLine($"You've lost... {pool} coins awarded to your opponent.");
        }
        //TODO: ranking similar hands
        else
        {
            player.Coins += pool / 2;
            Console.WriteLine($"The match ends in a draw. {pool / 2} coins refunded.");
        }

        Console.WriteLine($"{player.Coins} coins remaining.");
        DiceDealer.ReplayPrompt(player, opponent);
    }
    //probably put these in dealer. or extension methods static class
    public static string SeparateRerollString(string input)
    {
        string output = "";
        var chars = input.ToCharArray();
        if (input.Length > 2)
        {
            foreach (var c in chars)
            {
                if (Array.IndexOf(chars, c) == chars.Length - 1) output += "and " + c;
                else output += c + ", ";
            }
            return output;
        }
        else if (input.Length == 2)
        {
            return input.Insert(1, " and ");
        }
        else return input;
    }

    public static string SeparateDiceHandString(DiceHand hand)
    {
        if ((int)hand < 2) return hand.ToString();
        var str = hand.ToString();
        var chars = hand.ToString().ToCharArray();
        int count = 0;
        for(int i = 1; i < chars.Length; i++)
        {
            if (char.IsUpper(chars[i]))
            {
                str = str.Insert(i + count, " ");
                count++;
            }
        }
        return str;
    }
}
