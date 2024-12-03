using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDayFour
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var result = DayFour.Program.RunExample(1);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var result = DayFour.Program.RunExample(2);
        result.ShouldBe(1);
    }
}
