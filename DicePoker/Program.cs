using System.Text.RegularExpressions;

namespace DicePoker;
class Program
{
    public static void Main(string[] args)
    {
        var player = new DicePlayer();
        Game(player);
    }

    public static void Game(DicePlayer player)
    {
        //TODO: add some Thread.Sleep
        var rnd = new Random();
        int pool = 0;

        pool = DiceDealer.Welcome(player, pool);
        Console.WriteLine("Press enter to roll the dice.");
        Console.ReadKey();
        Console.WriteLine("Here is your hand...");
        var playerDice = DiceDealer.RollDice(rnd).ToArray();
        var playerHand = DiceChecker.CheckDice(playerDice);
        DiceChecker.PrintDice(playerDice);
        Console.WriteLine();

        Console.WriteLine("And your opponent's hand...");
        var opponentDice = DiceDealer.RollDice(rnd).ToArray();
        var opponentHand = DiceChecker.CheckDice(opponentDice);
        DiceChecker.PrintDice(opponentDice);
        Console.WriteLine();

        DiceChecker.PrintHands(playerHand, opponentHand);
        DiceDealer.BettingPrompt(player, ref pool);

        DiceDealer.RerollPrompt(playerDice, ref playerHand);

        Console.WriteLine("Opponent's turn...");
        opponentDice = DiceOpponent.OpponentReroll(opponentDice, opponentHand);
        opponentHand = DiceChecker.CheckDice(opponentDice);

        DiceChecker.PrintHands(playerHand, opponentHand);
        if ((int)playerHand > (int)opponentHand)
        {
            player.RoundsWon++;
            player.Coins += pool;
            Console.WriteLine($"You've won! {pool} coins added!");
        }
        else if ((int)playerHand < (int)opponentHand)
        {
            Console.WriteLine($"You've lost... {pool / 2} coins removed.");
        }
        //TODO: ranking similar hands
        else
        {
            player.Coins += pool / 2;
            Console.WriteLine($"The match ends in a draw. {pool / 2} coins refunded.");
        }

        Console.WriteLine($"{player.Coins}");
        DiceDealer.ReplayPrompt(player);
    }

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
