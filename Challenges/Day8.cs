namespace AdventOfCode.Challenges;

public class Day8 : BaseChallenge
{
    public Dictionary<char, List<Tuple<int, int>>> _antennas = new Dictionary<char, List<Tuple<int, int>>>();
    public List<Tuple<int, int>> _grid = new List<Tuple<int, int>>();
    public List<Tuple<int, int>> _nodes = new List<Tuple<int, int>>();

    public int maxX, maxY = 0;

    protected override string GetExampleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-8/example/input.txt");
    }

    protected override string GetChallengeFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-8/challenge/input.txt");
    }

    protected override void SolveStep1()
    {
        foreach (var antenna in _antennas)
        {
            FindNodes(antenna.Value);
        }
        _total = _nodes.Count;
        // _total = 14;
    }

    protected override void SolveStep2()
    {
        foreach (var antenna in _antennas)
        {
            FindNodes(antenna.Value);
        }
        _total = _nodes.Count;
    }

    public void FindNodes(List<Tuple<int, int>> antennas)
    {
        foreach (var antennaOne in antennas)
        {
            foreach (var antennaTwo in antennas)
            {
                if (_step == 1)
                {
                    AddNodes(antennaOne, antennaTwo);
                }
                else
                {
                    AddAllNodes(antennaOne, antennaTwo);
                }
            }    
        }
    }

    public void AddNodes(Tuple<int, int> antennaOne, Tuple<int, int> antennaTwo)
    {
        if (antennaOne == antennaTwo)
        {
            return;
        }
        
        var difference = Tuple.Create(antennaTwo.Item1 - antennaOne.Item1, antennaTwo.Item2 - antennaOne.Item2);
        var nodeOne = Tuple.Create(antennaOne.Item1 - difference.Item1, antennaOne.Item2 - difference.Item2);
        var nodeTwo = Tuple.Create(antennaTwo.Item1 + difference.Item1, antennaTwo.Item2 + difference.Item2);

        AddNode(nodeOne);
        AddNode(nodeTwo);
    }

    private void AddAllNodes(Tuple<int, int> antennaOne, Tuple<int, int> antennaTwo)
    {
        AddNode(antennaOne);
        AddNode(antennaTwo);
        
        if (antennaOne == antennaTwo)
        {
            return;
        }
        
        var difference = Tuple.Create(antennaTwo.Item1 - antennaOne.Item1, antennaTwo.Item2 - antennaOne.Item2);

        AddPositiveDifference(antennaTwo, difference);
        AddNegativeDifference(antennaOne, difference);
    }

    private void AddPositiveDifference(Tuple<int, int> antenna, Tuple<int, int> difference)
    {
        bool inGrid = true;
        var current = Tuple.Create(antenna.Item1, antenna.Item2);
        while (inGrid)
        {
            var node = Tuple.Create(current.Item1 + difference.Item1, current.Item2 + difference.Item2);
            if (!IsInGrid(node))
            {
                inGrid = false;
            }

            AddNode(node);
            current = node;
        }
    }

    private void AddNegativeDifference(Tuple<int, int> antenna, Tuple<int, int> difference)
    {
        bool inGrid = true;
        var current = Tuple.Create(antenna.Item1, antenna.Item2);
        while (inGrid)
        {
            var node = Tuple.Create(current.Item1 - difference.Item1, current.Item2 - difference.Item2);
            if (!IsInGrid(node))
            {
                inGrid = false;
            }

            AddNode(node);
            current = node;
        }
    }

    private void AddNode(Tuple<int, int> node)
    {
        if (!IsInGrid(node))
        {
            return;
        }
        
        if (_nodes.Contains(node))
        {
            return;
        }
        _nodes.Add(node);
    }

    private bool IsInGrid(Tuple<int, int> node)
    {
        if (node.Item1 < 0 || node.Item1 > maxX)
        {
            return false;
        }
        
        if (node.Item2 < 0 || node.Item2 > maxY)
        {
            return false;
        }

        return true;
    }

    public override void ParseInput(string filePath)
    {
        base.ParseInput(filePath);

        for (int y = 0; y < _rawLines.Length; y++)
        {
            maxY = y;
            string line = _rawLines[y];
            for (int x = 0; x < line.Length; x++)
            {
                maxX = x;
                _grid.Add(Tuple.Create(x, y));

                char position = line[x];

                if (position == '.')
                {
                    continue;
                }

                if (!_antennas.ContainsKey(position))
                {
                    _antennas.Add(position, new List<Tuple<int, int>>());
                }

                _antennas[position].Add(Tuple.Create(x, y));
            }
        }
    }
}