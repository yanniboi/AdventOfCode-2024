using AdventOfCode.Challenges;

namespace AdventOfCode.Solver;

public class Program
{
    static void Main(string[] args)
    {
        var challenge = new Day9();
        var result = challenge.RunChallenge(1);

        Console.WriteLine("The answer is: " + result);
    }
}
