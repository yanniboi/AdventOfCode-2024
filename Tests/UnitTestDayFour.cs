using AdventOfCode.Challenges;
using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDayFour
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var challenge = new Day4();
        var result = challenge.RunExample(1);
        result.ShouldBe(18);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day4();
        var result = challenge.RunExample(2);
        result.ShouldBe(9);
    }
}
