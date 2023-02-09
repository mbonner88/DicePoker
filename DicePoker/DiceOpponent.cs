using System;
namespace DicePoker
{
	public static class DiceOpponent
	{
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

