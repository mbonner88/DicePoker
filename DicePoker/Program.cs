namespace DicePoker;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to dice poker. Here is your first hand...");
        var rnd = new Random();
        var dice = RollDice(rnd).ToArray();        
        Console.WriteLine(DiceChecker.CheckDice(dice));
        Console.WriteLine("Enter the numbers of the dice you would like to reroll:");
        var userInput = Console.ReadLine();
        dice = RerollDice(userInput, dice, rnd);
        Console.WriteLine($"New hand...");
        Console.WriteLine(DiceChecker.CheckDice(dice));
    }
    
    static IEnumerable<int> RollDice(Random rnd)
    {
        for(int i = 0; i < 5; i++)
        {
            yield return rnd.Next(1, 60) / 10 + 1;
        }        
    }

    static int[] RerollDice(string userInput, int[] dice, Random rnd)
    {
        var chars = userInput.ToCharArray();
        foreach(var c in chars)
        {
            dice[(int)char.GetNumericValue(c) - 1] = rnd.Next(1, 60) / 10 + 1;
        }
        return dice;
    }
}
