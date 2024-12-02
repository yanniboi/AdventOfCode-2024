namespace DayTwo;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Day 2 challenge:");

        var result = RunExample(2);
        // var result = RunChallenge(1);

        Console.WriteLine("The answer is: " + result);

    }

    public static int RunExample(int step = 1)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "day-2/example/input.txt");
        var input = ParseInput(filePath);

        int total = 0;
        foreach (var line in input)
        {
            var sequence = new Queue<int>(line);
            bool safe = CheckIsSafe(sequence);

            if (!safe && step == 1)
            {
                continue;
            }

            if (!safe && step == 2)
            {
                var dampSequence = new Queue<int>(line);
                safe = CheckIsSafeWithDampener(dampSequence);

                if (!safe)
                {
                    continue;
                }
            }

            total++;
        }

        return total;
    }

    public static int RunChallenge(int step = 1)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "day-2/challenge/input.txt");
        var input = ParseInput(filePath);

        int total = 0;
        foreach (var line in input)
        {
            var sequence = new Queue<int>(line);
            bool safe = CheckIsSafe(sequence);

            if (!safe && step == 1)
            {
                continue;
            }

            if (!safe && step == 2)
            {
                var dampSequence = new Queue<int>(line);
                safe = CheckIsSafeWithDampener(dampSequence);

                if (!safe)
                {
                    continue;
                }
            }

            total++;
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
                    throw new Exception("Invalid input file - no number 1.");
                }
                sequence.Enqueue(number);
            }
            sequences.Add(sequence);
        }

        return sequences;
    }


    private static bool CheckIsSafe(Queue<int> line)
    {
        Console.WriteLine("Sequence: " + string.Join(", ", line));

        int current = line.Dequeue();
        int direction = 0;
        while (line.Count > 0)
        {
            int next = line.Dequeue();

            int difference = next - current;
            bool isIncreasing = difference > 0;
            if (direction == 0)
            {
                direction = isIncreasing ? 1 : -1;
            }
            
            bool safe = CheckRules(current, next, direction);

            if (!safe)
            {
                return false;
            }

            current = next;
        }

        return true;
    }

    private static bool CheckIsSafeWithDampener(Queue<int> line)
    {
        var list = line.ToList();

        for (int i = 0; i < list.Count; i++)
        {
            var newList = list.ToList();
            newList.RemoveAt(i);

            var newQueue = new Queue<int>();
            foreach (int number in newList)
            {
                newQueue.Enqueue(number);
            }

            bool safe = CheckIsSafe(newQueue);
            if (safe)
            {
                Console.WriteLine("Safe Sequence: " + string.Join(", ", newQueue));
                return true;
            }
        }

        return false;
    }

    private static bool CheckRules(int current, int next, int direction)
    {

        int difference = next - current;
        int absDifference = int.Abs(difference);

        if (absDifference > 3)
        {
            Console.WriteLine("Not safe: Too large increase {0}, {1}", current, next);
            return false;
        }

        if (difference == 0)
        {
            Console.WriteLine("Not safe: No increase {0}, {1}", current, next);

            return false;
        }

        bool isIncreasing = difference > 0;
 
        if (isIncreasing && direction == -1)
        {
            Console.WriteLine("Not safe: Wrong direction ({0}) {1}, {2}", direction, current, next);

            return false;
        }

        if (!isIncreasing && direction == 1)
        {
            Console.WriteLine("Not safe: Wrong direction ({0}) {1}, {2}", direction, current, next);
            return false;
        }

        return true;
    }
}
