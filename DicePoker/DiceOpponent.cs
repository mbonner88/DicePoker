using System;
using Extensions;

namespace DicePoker
{
	public class DiceOpponent
	{
		public int[]? OpponentDice { get; set; }
		public DiceHand OpponentHand { get; set; }
		public int OpponentCoins { get; set; } = 500;
		public int OpponentWager { get; set; }
		public int OpponentWins { get; set; }

		public void OpponentBet(int amount)
		{
			if (amount > this.OpponentCoins)
			{
				Console.WriteLine($"Opponent cannot call your bet...\nOpponent betting {this.OpponentCoins} coins.");
                Thread.Sleep(1000);
                this.OpponentWager += this.OpponentCoins;
				this.OpponentCoins = 0;
			}
			else
			{
				Console.WriteLine($"Opponent betting {amount} coins.");
                Thread.Sleep(1000);
                this.OpponentWager += amount;
				this.OpponentCoins -= amount;
			}
		}

		public void OpponentWin(DicePlayer player)
		{
			Console.WriteLine($"Opponent wins the round. You lost {player.PlayerWager} coins.");
            Thread.Sleep(1000);
            this.OpponentCoins += this.OpponentWager + player.PlayerWager;
			player.PlayerWager = 0;
			this.OpponentWager = 0;
			this.OpponentWins++;
		}

		public static int[] OpponentReroll(int[] dice, DiceHand hand)
		{
			if ((int)hand >= 4 && (int)hand != 7)
			{
				Console.WriteLine("Opponent declined rerolling any dice.");
				Thread.Sleep(1000);
				return dice;
			}
            string rerollInput = "";
            var rerollDice = dice.GroupBy(x => x).Where(x => x.Count() == 1).Select(x => x.Key);
            foreach (var die in rerollDice)
            {
                rerollInput += Array.IndexOf(dice, die) + 1;
            }
            if (rerollInput.Length == 1) Console.WriteLine($"Opponent rerolling die {rerollInput}");
            else Console.WriteLine($"Opponent rerolling dice {rerollInput.SeparateRerollString()}");
            Thread.Sleep(1000);
            int[] newDice = DiceDealer.RerollDice(rerollInput, dice, new Random());
            Console.WriteLine("Opponent's new hand...");
            Thread.Sleep(1000);
            DiceChecker.PrintDice(newDice);
            Thread.Sleep(1000);
            return newDice;
            
            //two of a kind logic, discard lower pair if player hand is stronger, else keep both pairs
        }

		//TODO: opponent betting logic
		//must add raising and folding features to do this
	}
}

