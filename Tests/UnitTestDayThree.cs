using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDayThree
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var result = DayThree.Program.RunExample(1);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var result = DayThree.Program.RunExample(2);
        result.ShouldBe(0);
    }
}
