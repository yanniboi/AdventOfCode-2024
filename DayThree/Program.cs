namespace DayThree;

public class Program
{
    private static int _step = 1;
    private static bool _example = true;

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
        _step = step;
        Console.WriteLine("Running Example");
        var filePath = Path.Combine(AppContext.BaseDirectory, "day-3/example/input.txt");
        return Run(filePath);
    }

    public static int RunChallenge(int step)
    {
        _step = step;
        Console.WriteLine("Running Challenge");
        var filePath = Path.Combine(AppContext.BaseDirectory, "day-3/challenge/input.txt");
        return Run(filePath);
    }

    public static int Run(string filePath)
    {
        var input = ParseInput(filePath);

        int total = 0;
        foreach (var line in input)
        {
          // Do something.

          if (_step == 2)
          {
              // Do something else.
          }
        }

        return total;
    }

    private static List<Queue<int>> ParseInput(string filePath)
    {
        List<Queue<int>> sequences = new List<Queue<int>>();
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Queue<int> sequence = new Queue<int>();

            foreach (var part in parts)
            {
                if (!int.TryParse(part, out int number))
                {
                    throw new Exception("Invalid input file.");
                }
                sequence.Enqueue(number);
            }
            sequences.Add(sequence);
        }

        return sequences;
    }
}
