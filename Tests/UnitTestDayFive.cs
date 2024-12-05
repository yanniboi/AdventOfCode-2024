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
        result.ShouldBe(143);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day5();
        var result = challenge.RunExample(2);
        result.ShouldBe(123);
    }

    [TestMethod]
    [DynamicData(nameof(Test_AnalysePrintPages_Data), DynamicDataSourceType.Method)]
    public void Test_AnalysePrintPages(string input, List<int> expected)
    {
        var challenge = new Day5();
        var result = challenge.AnalysePrintPages(input);
        result.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Test_AnalysePrintPages_Data()
    {
        yield return new object[]
        {
            "75,47,61,53,29",
            new List<int>
            {
                75, 47, 61, 53, 29
            }
        };
        yield return new object[]
        {
            "75,29,13",
            new List<int>
            {
                75, 29, 13
            }
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_AnalysePrintRules_Data), DynamicDataSourceType.Method)]
    public void Test_AnalysePrintRules(string input, Tuple<int, int> expected)
    {
        var challenge = new Day5();
        var result = challenge.AnalysePrintRules(input);
        result.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Test_AnalysePrintRules_Data()
    {
        yield return new object[]
        {
            "75|13",
            new Tuple<int, int>(75, 13)
        };
        yield return new object[]
        {
            "13|42",
            new Tuple<int, int>(13, 42)
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_PageIsWrongOrder_Data), DynamicDataSourceType.Method)]
    public void Test_PageIsWrongOrder(Tuple<int, int> rule, List<int> pages, bool expected)
    {
        var challenge = new Day5();
        var result = challenge.PageIsWrongOrder(pages, rule);
        result.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Test_PageIsWrongOrder_Data()
    {
        // Rule contains no relevant pages.
        yield return new object[]
        {
            new Tuple<int, int>(13, 42),
            new List<int>
            {
                75, 47, 61, 53, 29
            },
            false
        };
        // Rule contains one relevant pages.
        yield return new object[]
        {
            new Tuple<int, int>(75, 42),
            new List<int>
            {
                75, 47, 61, 53, 29
            },
            false
        };
        // Rule is in correct order - adjacent.
        yield return new object[]
        {
            new Tuple<int, int>(75, 47),
            new List<int>
            {
                75, 47, 61, 53, 29
            },
            false
        };
        // Rule is in wrong order - adjacent.
        yield return new object[]
        {
            new Tuple<int, int>(61, 47),
            new List<int>
            {
                75, 47, 61, 53, 29
            },
            true
        };
        // Rule is in correct order - with gap.
        yield return new object[]
        {
            new Tuple<int, int>(47, 29),
            new List<int>
            {
                75, 47, 61, 53, 29
            },
            false
        };
        // Rule is in wrong order - with gap.
        yield return new object[]
        {
            new Tuple<int, int>(53, 75),
            new List<int>
            {
                75, 47, 61, 53, 29
            },
            true
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_FixPageOrder_Data), DynamicDataSourceType.Method)]
    public void Test_FixPageOrder(List<int> pages, List<int> expected)
    {
        var challenge = new Day5();
        var path = Path.Combine(AppContext.BaseDirectory, "day-5/example/input.txt");
        challenge.ParseInput(path);
        var result = challenge.FixPageOrder(pages, challenge._rules);
        result.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Test_FixPageOrder_Data()
    {
        yield return new object[]
        {
            new List<int>
            {
                75, 97, 47, 61, 53
            },
            new List<int>
            {
                97, 75, 47, 61, 53
            },
        };
        yield return new object[]
        {
            new List<int>
            {
                61, 13, 29
            },
            new List<int>
            {
                61, 29, 13
            },
        };
        yield return new object[]
        {
            new List<int>
            {
                97, 13, 75, 29, 47
            },
            new List<int>
            {
                97, 75, 47, 29, 13
            },
        };
        yield return new object[]
        {
            new List<int>
            {
                1, 2, 3, 75, 97, 47, 61, 53
            },
            new List<int>
            {
                1, 2, 3, 97, 75, 47, 61, 53
            },
        };
        yield return new object[]
        {
            new List<int>
            {
                97, 13, 75, 29, 47, 1, 2, 3
            },
            new List<int>
            {
                97, 75, 47, 29, 13, 1, 2, 3
            },
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_FixPageOrderChallenge_Data), DynamicDataSourceType.Method)]
    public void Test_FixPageOrderChallenge(List<int> pages, List<int> expected)
    {
        var challenge = new Day5();
        var path = Path.Combine(AppContext.BaseDirectory, "day-5/challenge/input.txt");
        challenge.ParseInput(path);
        var result = challenge.FixPageOrder(pages, challenge._rules);

        // result.ShouldBe(expected);

        bool passes = challenge.CheckAllRules(result);
        passes.ShouldBeTrue();
    }

    public static IEnumerable<object[]> Test_FixPageOrderChallenge_Data()
    {
        yield return new object[]
        {
            new List<int>
            {
                65,95,37,49,18,33,76,39,83,85,35,92,75,88,91,67,12,74,69,79,22
            },
            new List<int>
            {
                91,79,65,22,37,67,88,69,18,12,83,85,76,74,33,35,75,49,92,95

            },
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_GetMiddleOfPage_Data), DynamicDataSourceType.Method)]
    public void Test_GetMiddleOfPage(List<int> input, int expected)
    {
        var challenge = new Day5();
        var result = challenge.GetMiddleOfPage(input);
        result.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Test_GetMiddleOfPage_Data()
    {
        yield return new object[]
        {
            new List<int>
            {
                75, 47, 61, 53, 29
            },
            61
        };
        yield return new object[]
        {
            new List<int>
            {
                61, 13, 29
            },
            13
        };
    }
}
