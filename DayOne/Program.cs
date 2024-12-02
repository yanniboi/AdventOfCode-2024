namespace DayOne;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Day 1 challenge:");

        // var result = RunExample();
        var result = RunChallenge();

        Console.WriteLine("The answer is: " + result);

    }

    public static int RunExample(int step = 1)
    {
        var lists = ParseInput("example/input.txt");

        var listOne = lists.Item1;
        var listTwo = lists.Item2;

        listOne.Sort();
        listTwo.Sort();

        if (step == 1)
        {
            return GetDifferenceScore(listOne, listTwo);
        }
        return GetSimilarityScore(listOne, listTwo);

    }

    private static int RunChallenge()
    {
        var lists = ParseInput("challenge/input.txt");

        var listOne = lists.Item1;
        var listTwo = lists.Item2;

        listOne.Sort();
        listTwo.Sort();

        // return GetDifferenceScore(listOne, listTwo);
        return GetSimilarityScore(listOne, listTwo);
    }

    private static int GetSimilarityScore(List<int> listOne, List<int> listTwo)
    {
        int total = 0;

        var listTwoDictionary = new Dictionary<int, int>();
        foreach (var item in listTwo)
        {
            if (!listTwoDictionary.ContainsKey(item))
            {
                listTwoDictionary.Add(item, 0);
            }

            listTwoDictionary[item]++;
        }

        while (listOne.Count > 0)
        {
            int compareOne = listOne.First();

            int listTwoValue = 0;
            if (listTwoDictionary.ContainsKey(compareOne))
            {
                listTwoValue = listTwoDictionary[compareOne];
            }

            int similarity = compareOne * listTwoValue;
            total += similarity;

            listOne.Remove(compareOne);
        }


        return total;
    }

    private static int GetDifferenceScore(List<int> listOne, List<int> listTwo)
    {
        int total = 0;

        while (listOne.Count > 0)
        {
            int compareOne = listOne.First();
            int compareTwo = listTwo.First();

            // remove first from list.
            listOne.Remove(compareOne);
            listTwo.Remove(compareTwo);


            int difference = compareOne - compareTwo;
            total += (int.Abs(difference));
        }


        return total;
    }

    private static Tuple<List<int>, List<int>> ParseInput(string filePath)
    {
        List<int> listOne = new List<int>();
        List<int> listTwo = new List<int>();

        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && int.TryParse(parts[0], out int num1) && int.TryParse(parts[1], out int num2))
            {
                listOne.Add(num1);
                listTwo.Add(num2);
            }
        }

        return new Tuple<List<int>, List<int>>(listOne, listTwo);
    }
}
