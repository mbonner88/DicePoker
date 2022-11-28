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
				Console.WriteLine($"Opponent rerolling dice {rerollInput}");
				return DiceDealer.RerollDice(rerollInput, dice, new Random());
			}
			else if ((int)hand == 7)
			{
				string rerollInput = Array.IndexOf(dice, dice.Distinct()).ToString();
				return DiceDealer.RerollDice(rerollInput, dice, new Random());
			}
			return dice;
		}

		//opponent betting
	}
}

