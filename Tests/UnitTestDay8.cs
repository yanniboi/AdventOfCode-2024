
using AdventOfCode.Challenges;
using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDay8
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var challenge = new Day8();
        var result = challenge.RunExample(1);
        result.ShouldBe(8);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day8();
        var result = challenge.RunExample(2);
        result.ShouldBe(8);
    }

    [TestMethod]
    [DynamicData(nameof(Test_Method_Data), DynamicDataSourceType.Method)]
    public void Test_Method(List<Int64> input, List<Int64> expected)
    {
        var challenge = new Day7();
        expected.Sort();

//        var result = challenge.Method(input);
        input.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Test_Method_Data()
    {
        // No obstacle
        yield return new object[]
        {
            new List<Int64>() {2,4},
            new List<Int64>() {2,4},
        };
    }
}

