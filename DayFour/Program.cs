﻿namespace DayFour;

public class Program
{
    private static bool _debug = false;

    private static int _step = 2;
    private static bool _example = false;
    private static int _total = 0;
    
    private static Dictionary<Tuple<int, int>, char> _grid = new Dictionary<Tuple<int, int>, char>();
    private static Dictionary<Tuple<int, int>, int> _founds = new Dictionary<Tuple<int, int>, int>();
    private static Dictionary<Tuple<int, int>, int> _foundsUp = new Dictionary<Tuple<int, int>, int>();
    private static Dictionary<Tuple<int, int>, int> _foundsDown = new Dictionary<Tuple<int, int>, int>();
    private static int _totalX = 0;
    private static int _totalY = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Day 4 challenge:");

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
        _step = step;
        _grid = new Dictionary<Tuple<int, int>, char>();
        _founds = new Dictionary<Tuple<int, int>, int>();
        _foundsUp = new Dictionary<Tuple<int, int>, int>();
        _foundsDown = new Dictionary<Tuple<int, int>, int>();
        _total = 0;
        _totalY = 0;
        Console.WriteLine("Running Example");
        var filePath = Path.Combine(AppContext.BaseDirectory, "day-4/example/input.txt");
        return Run(filePath);
    }

    public static int RunChallenge(int step)
    {
        _total = 0;
        _step = step;
        _grid = new Dictionary<Tuple<int, int>, char>();
        _founds = new Dictionary<Tuple<int, int>, int>();
        _foundsUp = new Dictionary<Tuple<int, int>, int>();
        _foundsDown = new Dictionary<Tuple<int, int>, int>();
        _totalX = 0;
        _totalY = 0;
        Console.WriteLine("Running Challenge");
        var filePath = Path.Combine(AppContext.BaseDirectory, "day-4/challenge/input.txt");
        return Run(filePath);
    }

    public static int Run(string filePath)
    {
        ParseInput(filePath);

            if (_step == 1)
            {
                FindXmas();
            }
            else
            {
                FindMasX();
            }

        return _total;
    }

    private static void FindXmas()
    {
        var vectors = new[]
        {
            new [] {1,1},
            new [] {-1,-1},
            new [] {1,-1},
            new [] {-1,1},
            new [] {0,1},
            new [] {0,-1},
            new [] {1,0},
            new [] {-1,0},
        };
        FindByVector("XMAS", vectors);
        Log(_total.ToString());
    }

    private static void FindMasX()
    {
        var vectors = new[]
        {
            new [] {1,1},
            new [] {-1,-1},
            new [] {1,-1},
            new [] {-1,1},
        };
        FindByVector("MAS", vectors);

        Log(_foundsUp.Count.ToString());
        Log(_foundsDown.Count.ToString());

        foreach (var found in _founds)
        {
            if (_foundsUp.ContainsKey(found.Key) && _foundsDown.ContainsKey(found.Key))
            {
                _total++;
            }
        }
    }

    private static void FindByVector(string search, int[][] vectors)
    {
        int found = 0;
        var searchLength = search.Length;

        foreach (var vector in vectors)
        {
            Log($"Current direction checked for {vector[0]}, {vector[1]}");

            int directionChecked = 0;
            int directionValid = 0;
            for (int x = 0; x < _totalX; x++)
            {
                for (int y = 0; y < _totalY; y++)
                {
                    directionChecked++;
                    char currentCheckChar = _grid[new Tuple<int, int>(x, y)];

                    // Is this a starting character?
                    // if (currentCheckChar != search[0])
                    // {
                    //     continue;
                    // }

                    // Are there 4 diagonals?
                    var checkMaxX = x + (vector[0] * (searchLength - 1));
                    var checkMaxY = y + (vector[1] * (searchLength - 1));
                    if (!_grid.ContainsKey(new Tuple<int, int>(checkMaxX, checkMaxY)))
                    {
                        continue;
                    }

                    directionValid++;

                    // Log($"Current direction checked for {x}, {y}");


                    var success = true;
                    for (int s = 0; s < searchLength; s++)
                    {

                        currentCheckChar = _grid[new Tuple<int, int>(x + (vector[0] * (s)), y + (vector[1] * (s)))];
                        if (currentCheckChar != search[s])
                        {
                            success = false;
                            break;
                        }
                    }

                    if (success)
                    {
                        if (_step == 2)
                        {
                            var middle = new Tuple<int, int>(x + (vector[0]), y + (vector[1]));

                            Log($"Found middle {vector[0]},{vector[1]} for {middle.Item1}, {middle.Item2}");

                            Dictionary<Tuple<int, int>, int> tracker;

                            if (Math.Sign(vector[0]) == Math.Sign(vector[1]))
                            {
                                tracker = _foundsUp;
                            }
                            else
                            {
                                tracker = _foundsDown;
                            }

                            if (!tracker.ContainsKey(middle))
                            {
                                tracker[middle] = 0;
                            }
                            tracker[middle]++;

                            if (!_founds.ContainsKey(middle))
                            {
                                _founds[middle] = 0;
                            }
                            _founds[middle]++;
                        }

                        found++;
                        // Log($"Current direction {vector[0]},{vector[1]} checked for {x}, {y}");
                    }
                }
            }

            // Log($"Direction Checked for {directionChecked} and {directionValid}");
        }

        if (_step == 1)
        {
            _total += found;
        }
    }

    private static void ParseInput(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        var grid = new Dictionary<Tuple<int, int>, char>();

        _totalY = lines.Length;
        _totalX = lines[0].Length;
        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];
                _grid.Add(new Tuple<int, int>(x, y), c);
            }
        }
    }

    private static void Log(string log)
    {
        if (_debug)
        {
            Console.WriteLine(log);
        }
    }
}
