
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
        result.ShouldBe(41);
    }

    // [TestMethod]
    // public void TestExamplePart2()
    // {
    //     var challenge = new Day6();
    //     var result = challenge.RunExample(2);
    //     result.ShouldBe(6);
    // }

    [TestMethod]
    public void Test_ParseInput()
    {
        var challenge = new Day6();
        var path = Path.Combine(AppContext.BaseDirectory, "day-6/example/input.txt");
        challenge.ParseInput(path);
        challenge._obstacles.Count.ShouldBe(8);
        challenge._currentGuard.Item1.ShouldBe(4);
        challenge._currentGuard.Item2.ShouldBe(6);

        path = Path.Combine(AppContext.BaseDirectory, "day-6/challenge/input.txt");
        challenge.ParseInput(path);
        challenge._obstacles.Count.ShouldBe(813);
        challenge._currentGuard.Item1.ShouldBe(81);
        challenge._currentGuard.Item2.ShouldBe(36);
    }

    [TestMethod]
    public void Test_TurnGuard()
    {
        var challenge = new Day6();
        challenge._currentDirection.ShouldBe(new Tuple<int, int>(0, -1));

        challenge.TurnGuard();
        challenge._currentDirection.ShouldBe(new Tuple<int, int>(1, 0));

        challenge.TurnGuard();
        challenge._currentDirection.ShouldBe(new Tuple<int, int>(0, 1));

        challenge.TurnGuard();
        challenge._currentDirection.ShouldBe(new Tuple<int, int>(-1, 0));

        challenge.TurnGuard();
        challenge._currentDirection.ShouldBe(new Tuple<int, int>(0, -1));
    }

    [TestMethod]
    [DynamicData(nameof(Test_MoveGuard_Data), DynamicDataSourceType.Method)]
    public void Test_MoveGuard(Tuple<int,int> direction, Tuple<int,int> start, Tuple<int,int> expected, Tuple<int, int>? obstacle = null)
    {
        var challenge = new Day6();

        challenge._currentGuard = start;
        challenge._currentDirection = direction;

        if (obstacle != null)
        {
            challenge._obstacles.Add(obstacle);
        }
        challenge.MoveGuard();

        challenge._currentGuard.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Test_MoveGuard_Data()
    {
        // No obstacle
        yield return new object[]
        {
            new Tuple<int,int>(1,0), // direction
            new Tuple<int,int>(3, 3), // start
            new Tuple<int,int>(4,3), // finish
        };
        // Obstacle not in way
        yield return new object[]
        {
            new Tuple<int,int>(0,1), // direction
            new Tuple<int,int>(2,3), // start
            new Tuple<int,int>(2,4), // finish
            new Tuple<int,int>(5,5), // obstacle
        };
        // obstacle blocking
        yield return new object[]
        {
            new Tuple<int,int>(-1,0), // direction
            new Tuple<int,int>(3,2), // start
            new Tuple<int,int>(3,2), // finish
            new Tuple<int,int>(2,2), // obstacle
        };
        yield return new object[]
        {
            new Tuple<int,int>(0,-1), // direction
            new Tuple<int,int>(3,4), // start
            new Tuple<int,int>(3,3), // finish
        };
        yield return new object[]
        {
            new Tuple<int,int>(1,0), // direction
            new Tuple<int,int>(0,3), // start
            new Tuple<int,int>(1,3), // finish
        };
        yield return new object[]
        {
            new Tuple<int,int>(1,0), // direction
            new Tuple<int,int>(3, 3), // start
            new Tuple<int,int>(4,3), // finish
        };
        yield return new object[]
        {
            new Tuple<int,int>(1,0), // direction
            new Tuple<int,int>(3, 3), // start
            new Tuple<int,int>(4,3), // finish
        };

    }
}

