namespace AdventOfCode.Challenges;

public class Day7 : BaseChallenge
{
    public Dictionary<Int64, List<Int64>> _equations = new Dictionary<Int64, List<Int64>>();
    public Int64 _longTotal = 0;
    protected override string GetExampleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-7/example/input.txt");
    }

    protected override string GetChallengeFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-7/challenge/input.txt");
    }

    protected override void SolveStep1()
    {
        // Take equation.
        // Loop over parts.
        // Apply both operators.
        // Check if result is in possible outcomes.
        foreach (var equation in _equations)
        {
            var outcomes = ApplyOperators(equation.Value);

            if (outcomes.Contains(equation.Key))
            {
                _longTotal += equation.Key;
            }
        }

        Log($"the result is {_longTotal}");
    }

    protected override void SolveStep2()
    {
        // Take equation.
        // Loop over parts.
        // Apply both operators.
        // Check if result is in possible outcomes.
        foreach (var equation in _equations)
        {
            var outcomes = ApplyOperators(equation.Value);

            if (outcomes.Contains(equation.Key))
            {
                _longTotal += equation.Key;
            }
        }

        Log($"the result is {_longTotal}");
    }

    public List<Int64> ApplyOperators(List<Int64> parts)
    {
        var progress = new List<Int64>();

        foreach (var part in parts)
        {
            if (progress.Count == 0)
            {
                progress.Add(part);
                continue;
            }

            var newProgress = new List<Int64>();
            foreach (var current in progress)
            {
                newProgress.Add(current + part);
                newProgress.Add(current * part);

                if (_step == 2)
                {
                    newProgress.Add(Int64.Parse(current.ToString() + part.ToString()));
                }
            }

            progress = newProgress;
        }

        progress.Sort();
        return progress;
    }

    public override void ParseInput(string filePath)
    {
        base.ParseInput(filePath);

        foreach (var line in _rawLines)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            try
            {
                var result = Int64.Parse(parts[0]);
                var subparts = parts[1].Split(' ', StringSplitOptions.TrimEntries);

                _equations.Add(result, subparts.Select(x => Int64.Parse(x)).ToList());
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
