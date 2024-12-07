
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
        challenge.RunExample(1);
        challenge._longTotal.ShouldBe(3749);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day7();
        challenge.RunExample(2);
        challenge._longTotal.ShouldBe(11387);
    }
    
    [TestMethod]
    public void Test_Parse()
    {
        var challenge = new Day7();
        var path = Path.Combine(AppContext.BaseDirectory, "day-7/example/input.txt");
        challenge.ParseInput(path);
        
        challenge._equations.Count.ShouldBe(9);
        challenge._equations.Keys.ToList().Contains(1).ShouldBe(false);
        challenge._equations.Keys.ToList().Contains(7290).ShouldBe(true);
        challenge._equations[7290].Contains(1).ShouldBe(false);
        challenge._equations[7290].Contains(8).ShouldBe(true);
        challenge._equations[7290].Count().ShouldBe(4);
        challenge._equations[7290].Where(x => x == 6).ToList().Count.ShouldBe(2);
    }
    
    
    [TestMethod]
    [DynamicData(nameof(Test_ApplyOperators_Data), DynamicDataSourceType.Method)]
    public void Test_ApplyOperators(List<Int64> parts, List<Int64> expected)
    {
        var challenge = new Day7();
        expected.Sort();

        var result = challenge.ApplyOperators(parts);
       
        result.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Test_ApplyOperators_Data()
    {
        // No obstacle
        yield return new object[]
        {
            new List<Int64>() {2,4},
            new List<Int64>() {6,8},
        };
        yield return new object[]
        {
            new List<Int64>() {2,4,6},
            new List<Int64>() {12,48,36,14},
        };
    }
}

