using System;
using System.Text.RegularExpressions;

namespace DicePoker
{
	public static class DiceDealer
	{
        
        public static IEnumerable<int> RollDice(Random rnd)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return rnd.Next(1, 60) / 10 + 1;
            }
        }

        public static int[] RerollDice(string userInput, int[] dice, Random rnd)
        {
            var chars = userInput.ToCharArray();
            foreach (var c in chars)
            {
                dice[(int)char.GetNumericValue(c) - 1] = rnd.Next(1, 60) / 10 + 1;
            }
            return dice;
        }

        public static void RerollPrompt(int[] dice, DiceHand playerHand)
        {
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
                    string rerollInput;
                    while (true)
                    {
                        rerollInput = Console.ReadLine();
                        if (Regex.IsMatch(rerollInput, "^[1-5]{1,5}$")) break;
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter dice numbers with no separator.");
                            continue;
                        }
                    }
                    dice = DiceDealer.RerollDice(rerollInput, dice, new Random());
                    Console.WriteLine("Your new hand...");
                    playerHand = DiceChecker.CheckDice(dice);
                    break;
                }
                else if (userInput == "2")
                {
                    break;
                }
            }
            return;
        }

        public static void ReplayPrompt()
        {
            Console.WriteLine("Play again?");
            Console.WriteLine("1. Yes.");
            Console.WriteLine("2. No.");
            string userInput;
            while (true)
            {
                userInput = Console.ReadLine();
                if (userInput != "1" && userInput != "2")
                {
                    Console.WriteLine("Invalid input. Please enter a 1 or a 2.");
                }
                else if (userInput == "1") Program.Main(new string[0]);
                else break;
            }
            return;
        }
    }
}

