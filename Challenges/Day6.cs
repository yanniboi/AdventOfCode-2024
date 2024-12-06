using System.Diagnostics;

namespace AdventOfCode.Challenges;

public class Day6 : BaseChallenge
{

    public Dictionary<Tuple<int, int>, List<Tuple<int, int>>> _passed =
        new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();

    public List<Tuple<int, int>> _grid = new List<Tuple<int, int>>();
    public List<Tuple<int, int>> _allOptions = new List<Tuple<int, int>>();
    public List<Tuple<int, int>> _validOptions = new List<Tuple<int, int>>();
    public List<Tuple<int, int>> _obstacles = new List<Tuple<int, int>>();

    public Tuple<int, int> _guardOrigin = new Tuple<int, int>(0, 0);
    public Tuple<int, int> _currentGuard = new Tuple<int, int>(0, 0);

    public bool _guardIsWalking = true;
    public bool _guardIsInArea = true;

    public List<Tuple<int, int>> _possibleDirections = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(1, 0),
        new Tuple<int, int>(0, 1),
        new Tuple<int, int>(-1, 0),
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
        CompleteCycle();

        // Do something.
        _total = _passed.Count - 1;
    }

    protected override void SolveStep2()
    {
        CompleteCycle();
        _allOptions = new List<Tuple<int, int>>();
        foreach (var option in _passed.Keys.ToList())
        {
            if (!_allOptions.Contains(option))
            {
                _allOptions.Add(option);
            }

            foreach (var direction in _possibleDirections)
            {
                var neighbour = new Tuple<int, int>(option.Item1 + direction.Item1, option.Item2 + direction.Item2);

                if (!_allOptions.Contains(neighbour))
                {
                    _allOptions.Add(neighbour);
                }
            }
        }

        Log($"Found {_allOptions.Count} possible options");
        int key = 0;
        foreach (var option in _allOptions)
        {
            if (_obstacles.Contains(option))
            {
                continue;
            }

            Log($"Adding obstacle at: {option.Item1} {option.Item2}");
            _obstacles.Add(option);

            if (!DoesCycleComplete())
            {
                _validOptions.Add(option);
            }

            _obstacles.Remove(option);

            key++;
            Log($"Completed {key} of {_allOptions.Count} possible options");

        }

        // Do something.
        _total = _validOptions.Count;
    }

    private bool DoesCycleComplete()
    {
        // Reset values
        _passed.Clear();
        _guardIsWalking = true;
        _guardIsInArea = true;
        _currentGuard = _guardOrigin;
        _currentDirection = _possibleDirections[0];

        try
        {
            CompleteCycle();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    private void CompleteCycle()
    {
        AddPassed(_guardOrigin);
        while (_guardIsInArea)
        {
            if (_guardIsWalking)
            {
                bool didMove = MoveGuard();

                if (!didMove)
                {
                    continue;
                }

                if (WasPassedBefore(_currentGuard, _currentDirection))
                {
                    throw new Exception("The position was passed before.");
                }

                AddPassed(_currentGuard);

                if (!_grid.Contains(_currentGuard))
                {
                    _guardIsInArea = false;
                }

                continue;
            }

            TurnGuard();
            _guardIsWalking = true;
        }

        Log($"Walk completed and left at: {_currentGuard.Item1} {_currentGuard.Item2}");
    }

    private bool WasPassedBefore(Tuple<int, int> position, Tuple<int, int> direction)
    {
        if (!_passed.ContainsKey(position))
        {
            return false;
        }

        if (!_passed[position].Contains(direction))
        {
            return false;
        }

        return true;
    }

    private void AddPassed(Tuple<int, int> position)
    {
        if (!_passed.ContainsKey(position))
        {
            _passed[position] = new List<Tuple<int, int>>();
        }

        _passed[position].Add(_currentDirection);
    }

    public bool MoveGuard()
    {
        if (CanWalk())
        {
            DoMoveGuard();
            return true;
        }

        _guardIsWalking = false;
        return false;
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
        _currentGuard = GetNextPosition();
    }

    private Tuple<int, int> GetNextPosition()
    {
        var newX = _currentGuard.Item1 + _currentDirection.Item1;
        var newY = _currentGuard.Item2 + _currentDirection.Item2;
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
        _guardOrigin = _currentGuard = new Tuple<int, int>(0, 0);

        // Find obstacles.
        // Find guard.

        for (int y = 0; y < _rawLines.Length; y++)
        {
            string line = _rawLines[y];
            for (int x = 0; x < line.Length; x++)
            {
                _grid.Add(Tuple.Create(x, y));

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
                    _guardOrigin = _currentGuard = new Tuple<int, int>(x, y);
                }
            }
        }
    }
}
