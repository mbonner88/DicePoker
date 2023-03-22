namespace DiceTest;


public class DiceCheckerTest
{
    //TODO: more tests here, create new test class for dicedealer
    [Fact]
    public void CheckHandsTest()
    {
        var expected = true;
        var actual = DiceChecker.DeclareWinner(new DicePlayer { PlayerDice = new int[] { 6, 6, 4, 4, 2 }, PlayerHand = DiceHand.TwoPairs },
            new DiceOpponent { OpponentDice = new int[] { 6, 6, 4, 4, 1 }, OpponentHand = DiceHand.TwoPairs });
        Assert.Equal(expected, actual);
        
    }

    [Fact]
    public void FourOfAKindTest()
    {
        //Arrange
        var expected = true;
        //Act
        var actual = DiceChecker.FourOfAKind(new int[] { 3, 4, 4, 4, 4 });
        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PairTest()
    {
        //Arrange
        var expected = false;
        //Act
        var actual = DiceChecker.Pair(new int[] { 6, 5, 6, 4, 6 });
        //Assert
        Assert.Equal(expected, actual);
    }
}
