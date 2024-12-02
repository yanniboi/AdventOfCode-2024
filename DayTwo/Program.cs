namespace DayTwo;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Day 2 challenge:");

        var result = RunExample();
        // var result = RunChallenge();

        Console.WriteLine("The answer is: " + result);

    }

    public static int RunExample(int step = 1)
    {
        // var lists = ParseInput("day-2/example/input.txt");
        return 2;
    }

    private static int RunChallenge()
    {
        throw new NotImplementedException();
    }

    private static Tuple<List<int>, List<int>> ParseInput(string filePath)
    {
        throw new NotImplementedException();
    }
}
