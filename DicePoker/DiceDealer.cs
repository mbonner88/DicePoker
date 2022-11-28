using System;
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
    }
}

