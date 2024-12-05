using System.Collections;

namespace AdventOfCode.Challenges;

public class Day5 : BaseChallenge
{
    public List<Tuple<int, int>> _rules = new List<Tuple<int, int>>();
    List<List<int>> _printRuns = new List<List<int>>();

    protected override string GetExampleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-5/example/input.txt");
    }

    protected override string GetChallengeFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-5/challenge/input.txt");
    }

    protected override void SolveStep1()
    {
        foreach (var pages in _printRuns)
        {
            bool allRulesPassed = true;
            foreach (var rule in _rules)
            {
                if (PageIsWrongOrder(pages, rule))
                {
                    allRulesPassed = false;
                    break;
                }
            }

            if (allRulesPassed)
            {
                var middleOfPages = GetMiddleOfPage(pages);
                _total += middleOfPages;
            }
        }
    }

    protected override void SolveStep2()
    {
        foreach (var pages in _printRuns)
        {
            bool allRulesPassed = CheckAllRules(pages);
            if (!allRulesPassed)
            {
                var fixedPage = FixPageOrder(pages, _rules);

                var middleOfPages = GetMiddleOfPage(fixedPage);
                _total += middleOfPages;
            }
        }
    }

    public bool CheckAllRules(List<int> pages)
    {
        bool allRulesPassed = true;
        foreach (var rule in _rules)
        {
            if (PageIsWrongOrder(pages, rule))
            {
                allRulesPassed = false;
                break;
            }
        }
        return allRulesPassed;
    }

    public List<int> FixPageOrder(List<int> pages, List<Tuple<int, int>> rules)
    {
        List<Tuple<int, int>> validRules = new List<Tuple<int, int>>();
        List<int> megaRule = new List<int>();
        foreach (var rule in rules)
        {
            int firstPage = rule.Item1;
            int secondPage = rule.Item2;
            if (!pages.Contains(firstPage) || !pages.Contains(secondPage))
            {
                // Does not apply.
                continue;
            }

            validRules.Add(rule);
        }

        var firstRules = new Dictionary<int, List<int>>();
        var lastRules = new Dictionary<int, List<int>>();
        foreach (var rule in validRules)
        {
            if (!firstRules.ContainsKey(rule.Item1))
            {
                firstRules.Add(rule.Item1, new List<int>());
            }
            firstRules[rule.Item1].Add(rule.Item2);

            if (!lastRules.ContainsKey(rule.Item2))
            {
                lastRules.Add(rule.Item2, new List<int>());
            }
            lastRules[rule.Item2].Add(rule.Item1);
        }

        foreach (var rule in firstRules)
        {
            var page = rule.Key;

            if (megaRule.Contains(page))
            {
                continue;
            }

            int index = megaRule.Count();

            var afterIndexes = new List<int>();
            foreach (int afterPage in rule.Value)
            {
                if (megaRule.Contains(afterPage))
                {
                    afterIndexes.Add(megaRule.IndexOf(afterPage));
                }
            }

            if (afterIndexes.Count > 0)
            {
                index = afterIndexes.Min();
            }

            // Check if we need to do beforeIndexes as well...
            megaRule.Insert(index, page);
        }

        foreach (var rule in lastRules)
        {
            var page = rule.Key;
            if (megaRule.Contains(page))
            {
                continue;
            }

            int index = 0;
            var beforeIndexes = new List<int>();
            foreach (int beforePage in rule.Value)
            {
                if (megaRule.Contains(beforePage))
                {
                    beforeIndexes.Add(megaRule.IndexOf(beforePage));
                }
            }

            if (beforeIndexes.Count > 0)
            {
                index = beforeIndexes.Max();
            }

            // Check if we need to do beforeIndexes as well...
            megaRule.Insert(index + 1, page);
        }

        if (megaRule.Count == pages.Count)
        {
            return megaRule;
        }

        List<int> newPages = new List<int>();
        foreach (var page in pages)
        {
            if (newPages.Contains(page))
            {
                // Already added.
                continue;
            }

            if (!megaRule.Contains(page))
            {
                newPages.Add(page);
                continue;
            }

            foreach (var megaRulePage in megaRule.GetRange(0, megaRule.IndexOf(page) + 1))
            {
                if (newPages.Contains(megaRulePage))
                {
                    continue;
                }

                newPages.Add(megaRulePage);
            }
        }

        return newPages;
    }

    public List<int> AnalysePrintPages(string line)
    {
        var parts = line.Split(',', StringSplitOptions.TrimEntries);
        var run = new List<int>();
        foreach (var part in parts)
        {
            run.Add(int.Parse(part));
        }

        return run;
    }

    public Tuple<int, int> AnalysePrintRules(string line)
    {
        var parts = line.Split('|', StringSplitOptions.TrimEntries);
        return new Tuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public bool PageIsWrongOrder(List<int> pages, Tuple<int, int> rule)
    {
        int firstPage = rule.Item1;
        int secondPage = rule.Item2;

        if (!pages.Contains(firstPage) || !pages.Contains(secondPage))
        {
            return false;
        }

        var firstPosition = pages.IndexOf(firstPage);
        var secondPosition = pages.IndexOf(secondPage);

        if (firstPosition < secondPosition)
        {
            return false;
        }

        return true;
    }

    public int GetMiddleOfPage(List<int> pages)
    {
        var half = pages.Count() / 2 + 1;
        int middle = pages[half - 1];
        return middle;
    }

    public override void ParseInput(string filePath)
    {
        base.ParseInput(filePath);

        bool isPrintRuns = false;
        foreach (var line in _rawLines)
        {
            if (!isPrintRuns)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isPrintRuns = true;
                    continue;
                }

                _rules.Add(AnalysePrintRules(line));
            }
            else
            {
                var run = AnalysePrintPages(line);
                _printRuns.Add(run);
            }
        }
    }
}
