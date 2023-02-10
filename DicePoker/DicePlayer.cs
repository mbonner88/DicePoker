using System;
namespace DicePoker
{
    public class DicePlayer
    {
        public int[]? PlayerDice { get; set; }
        public DiceHand PlayerHand { get; set; }
        public int Coins { get; set; } = 500;
        public int PlayerWins { get; set; }
    }
}

