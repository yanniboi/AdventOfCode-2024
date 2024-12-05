
using AdventOfCode.Challenges;
using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDay6
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var challenge = new Day6();
        var result = challenge.RunExample(1);
        result.ShouldBe(6);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day6();
        var result = challenge.RunExample(2);
        result.ShouldBe(6);
    }
}

