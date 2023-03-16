using System;
namespace DicePoker
{
	public class DiceOpponent
	{
		/*store props here? dice, hand, coins, roundswon
		 make class nonstatic, keep methods static*/
		 
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
				this.OpponentWager += this.OpponentCoins;
				this.OpponentCoins = 0;
			}
			else
			{
				Console.WriteLine($"Opponent betting {amount} coins.");
				this.OpponentWager += amount;
				this.OpponentCoins -= amount;
			}
		}

		public void OpponentWin(DicePlayer player)
		{
			Console.WriteLine($"Opponent wins the round. You lost {player.PlayerWager} coins.");
			this.OpponentCoins += this.OpponentWager + player.PlayerWager;
			player.PlayerWager = 0;
			this.OpponentWager = 0;
			this.OpponentWins++;
		}

		public static int[] OpponentReroll(int[] dice, DiceHand hand)
		{
			if((int)hand < 4)
			{
				string rerollInput = "";
				var rerollDice = dice.GroupBy(x => x).Where(x=>x.Count() == 1).Select(x=>x.Key);
				foreach(var die in rerollDice)
				{
					rerollInput += Array.IndexOf(dice, die) + 1;
				}
				Console.WriteLine($"Opponent rerolling dice {Program.SeparateRerollString(rerollInput)}");
				int[] newDice = DiceDealer.RerollDice(rerollInput, dice, new Random());
				Console.WriteLine("Opponent's new hand...");
				DiceChecker.PrintDice(newDice);
				return newDice;
			}
			else if ((int)hand == 7)
			{
                string rerollInput = "";
                var rerollDice = dice.GroupBy(x => x).Where(x => x.Count() == 1).Select(x => x.Key);
                foreach (var die in rerollDice)
                {
                    rerollInput += Array.IndexOf(dice, die) + 1;
                }
                Console.WriteLine($"Opponent rerolling die {rerollInput}");
                int[] newDice = DiceDealer.RerollDice(rerollInput, dice, new Random());
                Console.WriteLine("Opponent's new hand...");
                DiceChecker.PrintDice(newDice);
                return newDice;
            }
			Console.WriteLine("Opponent declined rerolling any dice.");
			return dice;
		}

		//TODO: opponent betting logic
	}
}

