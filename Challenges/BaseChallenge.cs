namespace AdventOfCode.Challenges;

public abstract class BaseChallenge : IChallenge
{
    private static bool _debug = true;

    protected int _step = 1;
    protected int _total = 0;
    protected string[] _rawLines;

    protected abstract string GetExampleFilePath();
    protected abstract string GetChallengeFilePath();

    public int RunExample(int step = 1)
    {
        _step = step;
        Console.WriteLine("Running Example");
        return Run(GetExampleFilePath());
    }

    public int RunChallenge(int step = 1)
    {
        _step = step;
        Console.WriteLine("Running Challenge");
        return Run(GetChallengeFilePath());
    }

    public int Run(string filePath)
    {
        ParseInput(filePath);

            if (_step == 1)
            {
                SolveStep1();
            }
            else
            {
                SolveStep2();
            }

        return _total;
    }

    protected abstract void SolveStep1();
    protected abstract void SolveStep2();

    public virtual void ParseInput(string filePath)
    {
        _rawLines = File.ReadAllLines(filePath);

        // More parsing?
    }

    protected void Log(string log)
    {
        if (_debug)
        {
            Console.WriteLine(log);
        }
    }
}
