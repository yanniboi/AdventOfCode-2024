using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDayTwo
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var result = DayTwo.Program.RunExample(1);
        result.ShouldBe(2);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var result = DayTwo.Program.RunExample(2);
        result.ShouldBe(4);
    }
}
