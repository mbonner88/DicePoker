﻿using System;
using System.Text.RegularExpressions;
using Extensions;

namespace DicePoker
{
	public static class DiceDealer
	{
        public static void Welcome(DicePlayer player, DiceOpponent opponent)
        {
            //TODO:more player instructions. simplify phrasing, word wrapping
            Console.WriteLine($"Welcome to dice poker! Place your starting bet:" +
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
                    Console.WriteLine($"This round's pot starts off at " +
                        $"{player.PlayerWager + opponent.OpponentWager} coins.");
                    return;
                }
                else if(userInput == "2")
                {
                    player.PlayerBet(25, opponent);
                    Console.WriteLine($"This round's pot starts off at " +
                        $"{player.PlayerWager + opponent.OpponentWager} coins.");
                    return;
                }
                else if(userInput == "3")
                {
                    player.PlayerBet(50, opponent);
                    Console.WriteLine($"This round's pot starts off at " +
                        $"{player.PlayerWager + opponent.OpponentWager} coins.");
                    return;
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
                    Console.WriteLine($"Rerolling dice {rerollInput.SeparateRerollString()}");
                    Thread.Sleep(1000);
                    player.PlayerDice = DiceDealer.RerollDice(rerollInput, player.PlayerDice, new Random());
                    Console.WriteLine("Your new hand...");
                    Thread.Sleep(1000);
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

        public static void BettingPrompt(DicePlayer player, DiceOpponent opponent)
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
                    DiceDealer.PlaceBet(player, opponent);
                    Thread.Sleep(1000);
                    break;
                }
                else break;
            }
        }

        public static DicePlayer PlaceBet(DicePlayer player, DiceOpponent opponent)
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
                    Console.WriteLine($"{player.PlayerWager} coins is your total wager. " +
                        $"The pot has {player.PlayerWager + opponent.OpponentWager} coins and your " +
                        $"pockets now have {player.PlayerCoins}");
                    return player;
                }
                else if (userInput == "2")
                {
                    player.PlayerBet(25, opponent);
                    Console.WriteLine($"{player.PlayerWager} coins is your total wager. " +
                        $"The pot has {player.PlayerWager + opponent.OpponentWager} coins and your " +
                        $"pockets now have {player.PlayerCoins}");
                    return player;
                }
                else if (userInput == "3")
                {
                    player.PlayerBet(50, opponent);
                    Console.WriteLine($"{player.PlayerWager} coins is your total wager. " +
                        $"The pot has {player.PlayerWager + opponent.OpponentWager} coins and your " +
                        $"pockets now have {player.PlayerCoins}");
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
                else if (userInput == "1")
                {
                    if (CheckCoinBalance(player))
                    {
                        Program.Game(player, opponent);
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Sorry, it looks like you can't afford another game.");
                        Thread.Sleep(1000);
                        CloseGame(player, opponent);
                        return;
                    }
                }
                else
                {
                    CloseGame(player, opponent);
                    return;
                }
            }
        }

        public static bool CheckCoinBalance(DicePlayer player)
        {
            if (player.PlayerCoins > 0) return true;
            else return false;
        }

        public static void CloseGame(DicePlayer player, DiceOpponent opponent)
        {
            Console.WriteLine($"Thank you for playing dice poker!\nYou won {player.PlayerWins} game(s) " +
                $"against your opponent, and your opponent won {opponent.OpponentWins} game(s).");
            Thread.Sleep(4000);
            if(player.PlayerCoins > 500)
            {
                Console.WriteLine($"You made a net profit of {player.PlayerCoins - 500} coins, well done!");
            }
            else if(player.PlayerCoins < 500)
            {
                Console.WriteLine($"Unfortunately you lost {500 - player.PlayerCoins} coins today...");
            }
            else { Console.WriteLine($"Looks like you broke even on your winnings."); }
            Thread.Sleep(4000);
            Console.WriteLine($"Come back soon to play more dice poker! Have a great day.");
            Thread.Sleep(4000);
            return;
        }
    }
}

