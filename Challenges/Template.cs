namespace AdventOfCode.Challenges;

public class Template : BaseChallenge
{
    protected override string GetExampleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-<DAY>/example/input.txt");
    }

    protected override string GetChallengeFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-<DAY>/challenge/input.txt");
    }

    protected override void SolveStep1()
    {
        // Do something.
    }

    protected override void SolveStep2()
    {
        // Do something.
    }
}
