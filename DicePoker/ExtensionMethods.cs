using System;
using DicePoker;

namespace Extensions
{
	public static class ExtensionMethods
	{
        public static string SeparateRerollString(this string input)
        {
            string output = "";
            var chars = input.ToCharArray();
            if (input.Length > 2)
            {
                foreach (var c in chars)
                {
                    if (Array.IndexOf(chars, c) == chars.Length - 1) output += "and " + c;
                    else output += c + ", ";
                }
                return output;
            }
            else if (input.Length == 2)
            {
                return input.Insert(1, " and ");
            }
            else return input;
        }

        public static string SeparateDiceHandString(this DiceHand hand)
        {
            if ((int)hand < 2) return hand.ToString();
            var str = hand.ToString();
            var chars = hand.ToString().ToCharArray();
            int count = 0;
            for (int i = 1; i < chars.Length; i++)
            {
                if (char.IsUpper(chars[i]))
                {
                    str = str.Insert(i + count, " ");
                    count++;
                }
            }
            return str;
        }
    }
}

