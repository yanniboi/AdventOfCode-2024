
namespace AdventOfCode.Challenges;

public class Day6 : BaseChallenge
{
    public List<Tuple<int, int>> _obstacles = new List<Tuple<int, int>>();
    public Tuple<int, int> _guard = new Tuple<int, int>(0, 0);

    public bool _guardIsWalking = true;
    public bool _guardIsInArea = true;

    public List<Tuple<int, int>> _possibleDirections = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0, -1 ),
        new Tuple<int, int>(1, 0 ),
        new Tuple<int, int>(0, 1 ),
        new Tuple<int, int>(-1, 0 ),
    };

    public Tuple<int, int> _currentDirection = new Tuple<int, int>(0, -1);

    protected override string GetExampleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-6/example/input.txt");
    }

    protected override string GetChallengeFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-6/challenge/input.txt");
    }

    protected override void SolveStep1()
    {
        while (_guardIsInArea)
        {
            if (_guardIsWalking)
            {
                MoveGuard();
                continue;
            }

            TurnGuard();
            _guardIsWalking = true;
        }


        // Do something.
        _total = 6;
    }

    protected override void SolveStep2()
    {
        // Do something.
        _total = 6;
    }

    public void MoveGuard()
    {
        if (CanWalk())
        {
            DoMoveGuard();
        }
        else
        {
            _guardIsWalking = false;
        }
    }

    private bool CanWalk()
    {
        var next = GetNextPosition();
        if (_obstacles.Contains(next))
        {
            return false;
        }

        return true;
    }

    public void DoMoveGuard()
    {
        _guard = GetNextPosition();
    }

    private Tuple<int, int> GetNextPosition()
    {
        var newX = _guard.Item1 + _currentDirection.Item1;
        var newY = _guard.Item2 + _currentDirection.Item2;
        return new Tuple<int, int>(newX, newY);
    }

    public void TurnGuard()
    {
        int index = _possibleDirections.IndexOf(_currentDirection);
        index++;
        if (index == _possibleDirections.Count)
        {
            index = 0;
        }
        _currentDirection = _possibleDirections[index];
    }

    public override void ParseInput(string filePath)
    {
        base.ParseInput(filePath);

        _obstacles.Clear();
        _guard = new Tuple<int, int>(0, 0);

        // Find obstacles.
        // Find guard.

        for (int y = 0; y < _rawLines.Length; y++)
        {
            string line = _rawLines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char position = line[x];

                if (position == '.')
                {
                    continue;
                }


                if (position == '#')
                {
                    _obstacles.Add(new Tuple<int, int>(x, y));
                    continue;
                }

                if (position == '^')
                {
                    _guard = new Tuple<int, int>(x, y);
                }
            }
        }
    }
}

