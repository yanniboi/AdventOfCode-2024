using System.Text.RegularExpressions;

namespace DayThree;

public class Program
{
    private static int _step = 2;
    private static bool _example = false;
    private static bool _isDisabled = false;
    private static int _total = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Day 3 challenge:");

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
        _isDisabled = false;
        _step = step;
        Console.WriteLine("Running Example");
        var filePath = Path.Combine(AppContext.BaseDirectory, "day-3/example/input.txt");
        return Run(filePath);
    }

    public static int RunChallenge(int step)
    {
        _total = 0;
        _isDisabled = false;
        _step = step;
        Console.WriteLine("Running Challenge");
        var filePath = Path.Combine(AppContext.BaseDirectory, "day-3/challenge/input.txt");
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
        AddMatchesToScore(line);
    }

    private static void SolveChallenge(string line)
    {
        Console.WriteLine("");
        Console.WriteLine($"Full line: {line}");


        // Take the first input.

        // Split on dont.
        string[] donts = line.Split("don't()");
        string first = donts[0];

        Console.WriteLine($"First chunk: {first}");


        string[] nextDonts;
        if (!_isDisabled)
        {
            Console.WriteLine($"Is Valid, searching...");
            AddMatchesToScore(first);
            nextDonts = donts.Skip(1).ToArray();
        }
        else
        {
            nextDonts = donts.ToArray();
        }

        _isDisabled = true;


        foreach (var dont in nextDonts)
        {
            Console.WriteLine("");
            Console.WriteLine($"Next chunk: {dont}");

            Console.WriteLine($"Is not Valid, checking for dos...");
            string[] dos = dont.Split("do()");
            if (dos.Length > 1)
            {
                Console.WriteLine($"Some dos found, checking for matches");

                string[] doChunks = dos.Skip(1).ToArray();
                foreach (var doChunk in doChunks)
                {
                    Console.WriteLine($"Do chunk: {doChunk}");
                    AddMatchesToScore(doChunk);
                }
                _isDisabled = false;
            }
            else
            {
                Console.WriteLine($"No dos found.");
                _isDisabled = true;
            }
        }
    }


    private static void AddMatchesToScore(string matchSearch)
    {
        MatchCollection matches = GetMatches(matchSearch);

        if (matches.Count > 0)
        {
            Console.WriteLine($"Found {matches.Count} matches.");
            _total = AddScores(_total, matches);
        }
    }

    private static int AddScores(int total, MatchCollection matches)
    {
        foreach (Match match in matches)
        {
            // Console.WriteLine($"Found: {match.Value}");

            string[] parts = GetMultiples(match.Value);

            // Parse the numbers
            int num1 = int.Parse(parts[0]);
            int num2 = int.Parse(parts[1]);

            // Multiply the numbers
            int result = num1 * num2;
            // Console.WriteLine($"Result: {result}");

            total += result;
        }

        return total;
    }

    private static MatchCollection GetMatches(string line)
    {
        string pattern = @"mul\(\d+,\d+\)";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(line);
        return matches;
    }

    private static string[] GetMultiples(string matchValue)
    {
        string fullMatch = matchValue;

        // Extract the numbers using another regex or string manipulation
        string numbers = fullMatch.Substring(4, fullMatch.Length - 5); // Remove "mul(" and ")"
        string[] parts = numbers.Split(',');

        return parts;
    }

    private static string[] ParseInput(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        return lines;
    }
}
