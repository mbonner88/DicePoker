namespace DiceTest;


public class DiceCheckerTest
{
    //TODO: more tests here, create more test classes for dealer,
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
