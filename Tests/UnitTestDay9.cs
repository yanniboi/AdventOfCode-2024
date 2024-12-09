
using AdventOfCode.Challenges;
using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDay9
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var challenge = new Day9();
        var result = challenge.RunExample(1);
        result.ShouldBe(1928);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day9();
        var result = challenge.RunExample(2);
        result.ShouldBe(9);
    }

    [TestMethod]
    public void Test_Parse()
    {
        var challenge = new Day9();
        var path = Path.Combine(AppContext.BaseDirectory, "day-9/example/input.txt");
        challenge.ParseInput(path);

        challenge._files.Count().ShouldBe(10);
        string.Join("", challenge._disk).ShouldBe("00...111...2...333.44.5555.6666.777.888899");
    }

    [TestMethod]
    public void Test_MoveFileToFirstSpace()
    {
        var challenge = new Day9();
        var path = Path.Combine(AppContext.BaseDirectory, "day-9/example/input.txt");
        challenge.ParseInput(path);

        int firstSpace = challenge._disk.IndexOf(".");
        firstSpace.ShouldBe(2);
        challenge._disk[41].ShouldBe("9");

        challenge.MoveFileToFirstSpace(41);
        
        firstSpace = challenge._disk.IndexOf(".");
        firstSpace.ShouldBe(3);
        challenge._disk[41].ShouldBe(".");
        string.Join("", challenge._disk).ShouldBe("009..111...2...333.44.5555.6666.777.88889.");
    }
    
    [TestMethod]
    public void Test_MoveFiles()
    {
        var challenge = new Day9();
        var path = Path.Combine(AppContext.BaseDirectory, "day-9/example/input.txt");
        challenge.ParseInput(path);

        challenge.MoveFiles();
        string.Join("", challenge._disk).ShouldBe("0099811188827773336446555566..............");
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

