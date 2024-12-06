using AdventOfCode.Challenges;

namespace AdventOfCode.Solver;

public class Program
{
    static void Main(string[] args)
    {
        var challenge = new Day6();
        var result = challenge.RunExample(1);

        Console.WriteLine("The answer is: " + result);
    }
}
