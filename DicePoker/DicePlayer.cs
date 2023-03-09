using System;
namespace DicePoker
{
    public class DicePlayer
    {
        public int[] PlayerDice { get; set; }
        public DiceHand PlayerHand { get; set; }
        public int PlayerCoins { get; set; } = 500;
        public int PlayerWager { get; set; }
        public int PlayerWins { get; set; }

        public void PlayerBet(int amount, DiceOpponent opponent)
        {
            if (amount > this.PlayerCoins)
            {
                Console.WriteLine($"Insufficient coins...\nBetting your remaining {this.PlayerCoins} coins.");
                this.PlayerWager += this.PlayerCoins;
                opponent.OpponentBet(this.PlayerCoins);
                this.PlayerCoins = 0;
            }
            else
            {
                Console.WriteLine($"Betting {amount} coins.");
                this.PlayerWager += amount;
                this.PlayerCoins -= amount;
                opponent.OpponentBet(amount);
            }
        }

        public void PlayerWin(DiceOpponent opponent)
        {
            Console.WriteLine($"You won the round! {opponent.OpponentWager} coins gained.");
            this.PlayerCoins += this.PlayerWager + opponent.OpponentWager;
            this.PlayerWager = 0;
            opponent.OpponentWager = 0;
            this.PlayerWins++;
        }
    }
}

