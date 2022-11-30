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
        
        var rnd = new Random();
        int pool = 0;

        pool = DiceDealer.Welcome(player, pool);
        Console.WriteLine("Press enter to roll the dice.");
        Console.ReadKey();
        Console.WriteLine("Here is your hand...");
        var playerDice = DiceDealer.RollDice(rnd).ToArray();
        var playerHand = DiceChecker.CheckDice(playerDice);
        DiceChecker.PrintDice(playerDice);

        Console.WriteLine("And your opponent's hand...");
        var opponentDice = DiceDealer.RollDice(rnd).ToArray();
        var opponentHand = DiceChecker.CheckDice(opponentDice);
        DiceChecker.PrintDice(opponentDice);

        //TODO: betting        
        //Console.WriteLine($"");
        DiceChecker.PrintHands(playerHand, opponentHand);
        DiceDealer.BettingPrompt(player, ref pool);

        DiceDealer.RerollPrompt(playerDice, playerHand);

        Console.WriteLine("Opponent's turn...");
        opponentDice = DiceOpponent.OpponentReroll(opponentDice, opponentHand);
        opponentHand = DiceChecker.CheckDice(opponentDice);

        Console.WriteLine($"Your hand: {playerHand}");
        Console.WriteLine($"Opponent's hand: {opponentHand}");
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
            Console.WriteLine($"The match ends in a draw. {pool/2} coins refunded.");
        }

        Console.WriteLine($"{player.Coins}");
        DiceDealer.ReplayPrompt(player);
    }
}
