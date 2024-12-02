using Shouldly;

namespace Tests;



[TestClass]
public class UnitTestDayOne
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var result = DayOne.Program.RunExample(1);
        result.ShouldBe(11);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var result = DayOne.Program.RunExample(2);
        result.ShouldBe(31);
    }
}
