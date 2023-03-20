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
        var rnd = new Random();
        DiceDealer.Welcome(player, opponent);
        Thread.Sleep(1000);
        Console.WriteLine("Press any key to roll the dice.");
        Console.ReadKey();
        Console.WriteLine("Here is your hand...");
        Thread.Sleep(1000);

        player.PlayerDice = DiceDealer.RollDice(rnd).ToArray();
        player.PlayerHand = DiceChecker.CheckDice(player.PlayerDice);
        DiceChecker.PrintDice(player.PlayerDice);
        Console.WriteLine();
        Thread.Sleep(1000);

        Console.WriteLine("And your opponent's hand...");
        Thread.Sleep(1000);
        opponent.OpponentDice = DiceDealer.RollDice(rnd).ToArray();
        opponent.OpponentHand = DiceChecker.CheckDice(opponent.OpponentDice);
        DiceChecker.PrintDice(opponent.OpponentDice);
        Console.WriteLine();
        Thread.Sleep(1000);

        DiceChecker.PrintHands(player.PlayerHand, opponent.OpponentHand);
        Thread.Sleep(1000);
        
        if (DiceDealer.CheckCoinBalance(player)) { DiceDealer.BettingPrompt(player, opponent); }
        DiceDealer.RerollPrompt(player);
        Thread.Sleep(1000);

        Console.WriteLine("Opponent's turn...");
        Thread.Sleep(1000);

        opponent.OpponentDice = DiceOpponent.OpponentReroll(opponent.OpponentDice, opponent.OpponentHand);
        opponent.OpponentHand = DiceChecker.CheckDice(opponent.OpponentDice);
        DiceChecker.PrintHands(player.PlayerHand, opponent.OpponentHand);
        DiceChecker.CheckHands(player, opponent);

        Console.WriteLine($"{player.PlayerCoins} coins remaining.");
        Thread.Sleep(1000);
        DiceDealer.ReplayPrompt(player, opponent);
    }
}
