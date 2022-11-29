using System.Text.RegularExpressions;

namespace DicePoker;
class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to dice poker. Here is your hand...");
        var rnd = new Random();
        var playerDice = DiceDealer.RollDice(rnd).ToArray();
        var playerHand = DiceChecker.CheckDice(playerDice);
        
        Console.WriteLine("And your opponent's hand...");
        var opponentDice = DiceDealer.RollDice(rnd).ToArray();
        var opponentHand = DiceChecker.CheckDice(opponentDice);

        DiceDealer.RerollPrompt(playerDice, playerHand);

        Console.WriteLine("Opponent's turn...");
        opponentDice = DiceOpponent.OpponentReroll(opponentDice, opponentHand);
        opponentHand = DiceChecker.CheckDice(opponentDice);

        Console.WriteLine($"Your hand: {playerHand}");
        Console.WriteLine($"Opponent's hand: {opponentHand}");
        if((int)playerHand > (int)opponentHand)
        {
            Console.WriteLine("You've won!");
        }
        else if((int)playerHand < (int)opponentHand)
        {
            Console.WriteLine("You've lost...");
        }
        else Console.WriteLine("The match ends in a draw.");

        DiceDealer.ReplayPrompt();
    }
}
