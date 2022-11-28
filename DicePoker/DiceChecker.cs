using System;
namespace DicePoker
{
	public static class DiceChecker
	{
        public static string CheckDice(int[] dice)
        {
            if (FiveOfAKind(dice)) return "Five of a Kind";
            if (FourOfAKind(dice)) return "Four of a Kind";
            if (FullHouse(dice)) return "Full House";
            if (SixHighStraight(dice)) return "Six High Straight";
            if (FiveHighStraight(dice)) return "Five High Straight";
            if (ThreeOfAKind(dice)) return "Three of a Kind";
            if (TwoPairs(dice)) return "Two Pairs";
            if (Pair(dice)) return "Pair";
            return "Nothing...";
        }

        public static bool FiveOfAKind(int[] dice)
        {
            for (int i = 1; i < dice.Length; i++)
            {
                if (dice[0] != dice[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool FourOfAKind(int[] dice)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                int count = 1;
                for (int j = i + 1; j < dice.Length; j++)
                {
                    if (dice[i] == dice[j]) count++;
                }
                if (count == 4) return true;
            }
            return false;
        }

        public static bool ThreeOfAKind(int[] dice)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                int count = 1;
                for (int j = i + 1; j < dice.Length; j++)
                {
                    if (dice[i] == dice[j]) count++;
                }
                if (count == 3) return true;
            }
            return false;
        }

        public static bool FullHouse(int[] dice)
        {
            if (ThreeOfAKind(dice) && Pair(dice)) return true;
            return false;
        }

        public static bool SixHighStraight(int[] dice)
        {
            if (dice.Max() != 6) return false;
            var sortedDice = dice.Order().ToArray();
            for (int i = 1; i < sortedDice.Length; i++)
            {
                if (sortedDice[i] != sortedDice[i - 1] + 1) return false;
            }
            return true;
        }

        public static bool FiveHighStraight(int[] dice)
        {
            if (dice.Max() != 5) return false;
            var sortedDice = dice.Order().ToArray();
            for (int i = 1; i < sortedDice.Length; i++)
            {
                if (sortedDice[i] != sortedDice[i - 1] + 1) return false;
            }
            return true;
        }

        public static bool TwoPairs(int[] dice)
        {
            int pairs = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                int count = 1;
                for (int j = i + 1; j < dice.Length; j++)
                {
                    if (dice[i] == dice[j]) count++;
                }
                if (count == 2) pairs++;
            }
            if (pairs == 2) return true;
            return false;
        }

        public static bool Pair(int[] dice)
        {            
            int pairs = 0;
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice.Count(x => x == dice[i]) == 2 && dice[i] != temp)
                {
                    pairs++;
                    temp = dice[i];
                }                
            }
            if (pairs == 1) return true;
            return false;
        }
    }
}

