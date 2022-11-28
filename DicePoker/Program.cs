namespace DicePoker;
class Program
{
    static void Main(string[] args)
    {
        var rnd = new Random();
        var dice = RollDice(rnd).ToArray();
        foreach(int die in dice)
        {
            Console.WriteLine(die);
        }
        Console.WriteLine(DiceChecker.CheckDice(dice));
    }

    

    static IEnumerable<int> RollDice(Random rnd)
    {
        for(int i = 0; i < 5; i++)
        {
            yield return rnd.Next(1, 60) / 10 + 1;
        }        
    }

    
}
