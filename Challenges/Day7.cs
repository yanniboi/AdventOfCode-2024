
namespace AdventOfCode.Challenges;

public class Day7 : BaseChallenge
{
    public Dictionary<int, List<int>> _equations = new Dictionary<int, List<int>>();
    
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
                _total += equation.Key;
            }
        }
        
        // _total = 3749;
    }

    protected override void SolveStep2()
    {
        // Do something.
        _total = 7;
    }

    public List<int> ApplyOperators(List<int> parts)
    {
        var progress = new List<int>();

        foreach (var part in parts)
        {
            if (progress.Count == 0)
            {
                progress.Add(part);
                continue;
            }

            var newProgress = new List<int>();
            foreach (var current in progress)
            {
                newProgress.Add(current + part);
                newProgress.Add(current * part);
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
            var result = int.Parse(parts[0]);
            var subparts = parts[1].Split(' ', StringSplitOptions.TrimEntries);

            _equations.Add(result, subparts.Select(x => int.Parse(x)).ToList());
        }
    }

}

