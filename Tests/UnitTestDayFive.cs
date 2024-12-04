using AdventOfCode.Challenges;
using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDayFive
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var challenge = new Day5();
        var result = challenge.RunExample(1);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day5();
        var result = challenge.RunExample(2);
        result.ShouldBe(0);
    }
}
