namespace DayFour;

public class Program
{
    private static int _step = 1;
    private static bool _example = true;
    private static int _total = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Day 4 challenge:");

        int result;
        if (_example)
        {
           result = RunExample(_step);
        }
        else
        {
            result = RunChallenge(_step);
        }

        Console.WriteLine("The answer is: " + result);
    }

    public static int RunExample(int step)
    {
        _total = 0;
        _step = step;
        Console.WriteLine("Running Example");
        var filePath = Path.Combine(AppContext.BaseDirectory, "day-4/example/input.txt");
        return Run(filePath);
    }

    public static int RunChallenge(int step)
    {
        _total = 0;
        _step = step;
        Console.WriteLine("Running Challenge");
        var filePath = Path.Combine(AppContext.BaseDirectory, "day-4/challenge/input.txt");
        return Run(filePath);
    }

    public static int Run(string filePath)
    {
        var input = ParseInput(filePath);

        foreach (var line in input)
        {
            if (_step == 1)
            {
                SolveExample(line);
            }
            else
            {
                SolveChallenge(line);
            }
        }

        return _total;
    }

    private static void SolveExample(string line)
    {
        // Do something.
        _total = 1;
    }

    private static void SolveChallenge(string line)
    {
        // Do something harder.
        _total = 1;
    }

    private static string[] ParseInput(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        // Additional parsing if necessary.

        return lines;
    }
}
