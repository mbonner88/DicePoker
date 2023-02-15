using System;
namespace DicePoker
{
	public class DiceOpponent
	{
		/*store props here? dice, hand, coins, roundswon
		 make class nonstatic, keep methods static
		 primary objective of draining opponent balance to 0
		 secondary objectives on the way*/
		public int[]? OpponentDice { get; set; }
		public DiceHand OpponentHand { get; set; }
		public int OpponentCoins { get; set; }
		public int OpponentWins { get; set; }

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
				string rerollInput = Array.IndexOf(dice, dice.Distinct()).ToString();
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

