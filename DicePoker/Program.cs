namespace DicePoker;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to dice poker. Here is your hand...");
        var rnd = new Random();
        var playerDice = DiceDealer.RollDice(rnd).ToArray();
        var playerHand = DiceChecker.CheckDice(playerDice);
        
        Console.WriteLine("And your opponent's hand...");
        var opponentDice = DiceDealer.RollDice(rnd).ToArray();
        var opponentHand = DiceChecker.CheckDice(opponentDice);

        Console.WriteLine("Would you like to reroll some dice?");
        Console.WriteLine("1. Yes.");
        Console.WriteLine("2. No.");
        string userInput;
        while (true)
        {
            userInput = Console.ReadLine();
            if (userInput != "1" && userInput != "2")
            {
                Console.WriteLine("Invalid input. Please enter a 1 or a 2.");
                continue;
            }
            else if (userInput == "1")
            {
                Console.WriteLine("Enter the numbers of the dice you would like to reroll:");
                string rerollInput = Console.ReadLine();
                playerDice = DiceDealer.RerollDice(rerollInput, playerDice, rnd);
                Console.WriteLine("Your new hand...");
                playerHand = DiceChecker.CheckDice(playerDice);
                break;
            }
            else if (userInput == "2")
            {
                //compare
                break;
            }
        }

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

        Console.WriteLine("Play again?");
        Console.WriteLine("1. Yes.");
        Console.WriteLine("2. No.");
        while (true)
        {
            userInput = Console.ReadLine();
            if (userInput != "1" && userInput != "2")
            {
                Console.WriteLine("Invalid input. Please enter a 1 or a 2.");
            }
            else if (userInput == "1") Main(args);
            else break;
        }
    }
}
