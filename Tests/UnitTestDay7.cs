
using AdventOfCode.Challenges;
using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDay7
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var challenge = new Day7();
        var result = challenge.RunExample(1);
        result.ShouldBe(7);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day7();
        var result = challenge.RunExample(2);
        result.ShouldBe(7);
    }
}

