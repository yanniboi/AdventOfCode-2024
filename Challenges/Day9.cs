namespace AdventOfCode.Challenges;

public class Day9 : BaseChallenge
{
    public Dictionary<int, int> _files = new Dictionary<int, int>();
    public List<string> _disk = new List<string>();

    protected override string GetExampleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-9/example/input.txt");
    }

    protected override string GetChallengeFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-9/challenge/input.txt");
    }

    protected override void SolveStep1()
    {
        //Move files
        MoveFiles();

        CalculateChecksum();

        // string current = string.Join("", _disk);
        // Log(current); 
    }

    public void MoveFiles()
    {
        var reverse = _disk.ToList();
        reverse.Reverse();
        for (int i = 0; i < reverse.Count; i++)
        {
            var file = reverse[i];
            if (file == ".")
            {
                continue;
            }

            var fileIndex = reverse.Count - 1 - i;
            var space = FindFirstSpace();


            if (space > fileIndex)
            {
                break;
            }

            MoveFileToFirstSpace(fileIndex);

            string current = string.Join("", _disk);
            // Log(current);
            // var test = "";
        }
    }

    private void CalculateChecksum()
    {
        for (int i = 0; i < _disk.Count; i++)
        {
            if (_disk[i] == ".")
            {
                continue;
            }

            int value = int.Parse(_disk[i]);
            _total += (i * value);
            // Log($"i {i.ToString()}, value {value.ToString()}, total {_total.ToString()}");
        }
    }

    private int FindFirstSpace()
    {
        return _disk.IndexOf(".");
    }

    public void MoveFileToFirstSpace(int fileIndex)
    {
        int firstSpace = FindFirstSpace();
        var file = _disk[fileIndex];
        _disk[firstSpace] = file;
        _disk[fileIndex] = ".";
    }

    protected override void SolveStep2()
    {
        // Do something.
        _total = 9;
    }

    public override void ParseInput(string filePath)
    {
        base.ParseInput(filePath);


        bool isFile = true;
        int fileId = 0;
        var line = _rawLines[0];
        foreach (char c in line)
        {
            for (int i = 0; i < int.Parse(c.ToString()); i++)
            {
                _disk.Add(isFile ? fileId.ToString() : ".");
            }


            if (isFile)
            {
                _files.Add(fileId, c);
                fileId++;
            }

            isFile = !isFile;
        }

        // Extra parsing...
    }
}