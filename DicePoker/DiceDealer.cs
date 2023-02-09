using System;
using System.Text.RegularExpressions;

namespace DicePoker
{
	public static class DiceDealer
	{
        public static int Welcome(DicePlayer player, int pool)
        {
            //TODO:player instructions
            Console.WriteLine($"Welcome to dice poker. Place your starting bet.\nYou have {player.Coins} coins.\n1. 10 coins.\n2. 25 coins.\n3. 50 coins.");
            string userInput;
            while (true)
            {
                userInput = Console.ReadLine();
                if(userInput != "1" && userInput != "2" && userInput != "3")
                {
                    Console.WriteLine("Invalid input. Please enter a 1, 2, or a 3.");
                }
                else if(userInput == "1")
                {
                    player.Coins -= 10;
                    pool += 20;
                    Console.WriteLine($"This round's pool starts off at {pool} coins.");
                    return pool;
                }
                else if(userInput == "2")
                {
                    player.Coins -= 25;
                    pool += 50;
                    Console.WriteLine($"This round's pool starts off at {pool} coins.");
                    return pool;
                }
                else if(userInput == "3")
                {
                    player.Coins -= 50;
                    pool += 100;
                    Console.WriteLine($"This round's pool starts off at {pool} coins.");
                    return pool;
                }                
            }
        }

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

        public static void RerollPrompt(int[] dice, ref DiceHand playerHand)
        {
            Console.WriteLine("Would you like to reroll some dice?\n1. Yes.\n2. No.");
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
                    Console.WriteLine($"Rerolling dice {Program.SeparateRerollString(rerollInput)}");
                    dice = DiceDealer.RerollDice(rerollInput, dice, new Random());
                    Console.WriteLine("Your new hand...");
                    DiceChecker.PrintDice(dice);
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

        public static void BettingPrompt(DicePlayer player, ref int pool)
        {
            Console.WriteLine("Would you like to place a bet?\n1. Yes.\n2. No.");
            string userInput;
            while (true)
            {
                userInput = Console.ReadLine();
                if (userInput != "1" && userInput != "2")
                {
                    Console.WriteLine("Invalid input. Please enter a 1 or a 2.");
                }
                else if (userInput == "1")
                {
                    DiceDealer.PlaceBet(player, ref pool);
                    break;
                }
                else break;
            }
        }

        public static DicePlayer PlaceBet(DicePlayer player, ref int pool)
        {
            Console.WriteLine($"Select amount of coins to bet. You currently have {player.Coins}:\n1. 10 coins.\n2. 25 coins.\n3. 50 coins.");
            string userInput;
            while (true)
            {
                userInput = Console.ReadLine();
                if (userInput != "1" && userInput != "2" && userInput != "3")
                {
                    Console.WriteLine("Invalid input. Please enter a 1, 2, or a 3.");                    
                }
                else if (userInput == "1")
                {
                    player.Coins -= 10;
                    pool += 20;
                    Console.WriteLine($"Betting 10 coins. The pool has {pool} coins and your pockets now have {player.Coins}");
                    return player;
                }
                else if (userInput == "2")
                {
                    player.Coins -= 25;
                    pool += 50;
                    Console.WriteLine($"Betting 25 coins. The pool has {pool} coins and your pockets now have {player.Coins}");
                    return player;
                }
                else if (userInput == "3")
                {
                    player.Coins -= 50;
                    pool += 100;
                    Console.WriteLine($"Betting 50 coins. The pool has {pool} coins and your pockets now have {player.Coins}");
                    return player;
                }
            }
        }

        public static void ReplayPrompt(DicePlayer player)
        {
            Console.WriteLine("Play again?\n1. Yes.\n2. No.");
            string userInput;
            while (true)
            {
                userInput = Console.ReadLine();
                if (userInput != "1" && userInput != "2")
                {
                    Console.WriteLine("Invalid input. Please enter a 1 or a 2.");
                    continue;
                }
                else if (userInput == "1") Program.Game(player);
                else
                {
                    //TODO: coins won/lost
                    Console.WriteLine($"Thank you for playing. You won {player.RoundsWon} games and {player.Coins - 500} coins.");
                    break;
                }
            }
            return;
        }
    }
}

