using System;
using System.Text.RegularExpressions;

namespace DicePoker
{
	public static class DiceDealer
	{
        public static int Welcome(DicePlayer player, DiceOpponent opponent, int pool)
        {
            //TODO:more player instructions
            Console.WriteLine($"Welcome to dice poker. Place your starting bet." +
                $"\nYou have {player.PlayerCoins} coins.\n1. 10 coins.\n2. 25 coins.\n3. 50 coins.");
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
                    player.PlayerBet(10, opponent);
                    pool += 20;
                    Console.WriteLine($"This round's pool starts off at {pool} coins.");
                    return pool;
                }
                else if(userInput == "2")
                {
                    player.PlayerBet(25, opponent);
                    pool += 50;
                    Console.WriteLine($"This round's pool starts off at {pool} coins.");
                    return pool;
                }
                else if(userInput == "3")
                {
                    player.PlayerBet(50, opponent);
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

        public static void RerollPrompt(DicePlayer player)
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
                    Console.WriteLine("Enter the numbers corresponding to the dice you would like to reroll:");
                    Console.WriteLine("(For example, to reroll the first, third, and fifth dice, enter 135)");
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
                    player.PlayerDice = DiceDealer.RerollDice(rerollInput, player.PlayerDice, new Random());
                    Console.WriteLine("Your new hand...");
                    DiceChecker.PrintDice(player.PlayerDice);
                    player.PlayerHand = DiceChecker.CheckDice(player.PlayerDice);
                    break;
                }
                else if (userInput == "2")
                {
                    break;
                }
            }
            return;
        }

        public static void BettingPrompt(DicePlayer player, DiceOpponent opponent, ref int pool)
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
                    DiceDealer.PlaceBet(player, opponent, ref pool);
                    break;
                }
                else break;
            }
        }

        public static DicePlayer PlaceBet(DicePlayer player, DiceOpponent opponent, ref int pool)
        {
            Console.WriteLine($"Select amount of coins to bet. You currently have {player.PlayerCoins}:" +
                $"\n1. 10 coins.\n2. 25 coins.\n3. 50 coins.");
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
                    player.PlayerBet(10, opponent);
                    pool += 20;
                    Console.WriteLine($"{player.PlayerWager} coins is your total wager. The pool has {pool} coins and your pockets now have {player.PlayerCoins}");
                    return player;
                }
                else if (userInput == "2")
                {
                    player.PlayerBet(25, opponent);
                    pool += 50;
                    Console.WriteLine($"{player.PlayerWager} coins is your total wager. The pool has {pool} coins and your pockets now have {player.PlayerCoins}");
                    return player;
                }
                else if (userInput == "3")
                {
                    player.PlayerBet(50, opponent);
                    pool += 100;
                    Console.WriteLine($"{player.PlayerWager} coins is your total wager. The pool has {pool} coins and your pockets now have {player.PlayerCoins}");
                    return player;
                }
            }
        }

        public static void ReplayPrompt(DicePlayer player, DiceOpponent opponent)
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
                else if (userInput == "1") Program.Game(player, opponent);
                else
                {
                    //minus 500 here is to compensate for the starting balance. "you won -500" on player loss
                    //inconsistently closes the game or keeps it open sometimes...?
                    Console.WriteLine($"Thank you for playing. You won {player.PlayerWins} game(s) and {player.PlayerCoins - 500} coins.");
                    break;
                }
            }
            return;
        }
    }
}

