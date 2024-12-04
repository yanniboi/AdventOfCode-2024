#!/bin/bash

# Ensure the script exits on errors
set -e

# Prompt for day input if not provided
if [ -z "$1" ]; then
    read -p "Enter the day number (e.g., 5): " day
else
    day=$1
fi

# Ensure the day is a two-digit number (e.g., "05" for day 5)
day=$(printf "%02d" $day)

# Remove leading zeros for test class names (e.g., "05" becomes "5")
day_no_zero=$((10#$day))

# Set the challenge template content
challenge_template='
namespace AdventOfCode.Challenges;

public class Day<DAY_NO_ZERO> : BaseChallenge
{
    protected override string GetExampleFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-<DAY_NO_ZERO>/example/input.txt");
    }

    protected override string GetChallengeFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "day-<DAY_NO_ZERO>/challenge/input.txt");
    }

    protected override void SolveStep1()
    {
        // Do something.
        _total = <DAY_NO_ZERO>;
    }

    protected override void SolveStep2()
    {
        // Do something.
        _total = <DAY_NO_ZERO>;
    }
}
'

# Set the test template content
test_template='
using AdventOfCode.Challenges;
using Shouldly;

namespace Tests;

[TestClass]
public class UnitTestDay<DAY_NO_ZERO>
{
    [TestMethod]
    public void TestExamplePart1()
    {
        var challenge = new Day<DAY_NO_ZERO>();
        var result = challenge.RunExample(1);
        result.ShouldBe(<DAY_NO_ZERO>);
    }

    [TestMethod]
    public void TestExamplePart2()
    {
        var challenge = new Day<DAY_NO_ZERO>();
        var result = challenge.RunExample(2);
        result.ShouldBe(<DAY_NO_ZERO>);
    }
}
'

# Replace placeholders in the templates
challenge_content="${challenge_template//<DAY>/$day}"
challenge_content="${challenge_template//<DAY_NO_ZERO>/$day_no_zero}"
test_content="${test_template//<DAY>/$day}"
test_content="${test_content//<DAY_NO_ZERO>/$day_no_zero}"

# Define the output directories and files
challenge_dir="Challenges"
challenge_file="$challenge_dir/Day${day_no_zero}.cs"
test_dir="Tests"
test_file="$test_dir/UnitTestDay${day_no_zero}.cs"
inputs_example_dir="inputs/day-${day_no_zero}/example"
inputs_challenge_dir="inputs/day-${day_no_zero}/challenge"
inputs_example_file="$inputs_example_dir/input.txt"
inputs_challenge_file="$inputs_challenge_dir/input.txt"

# Create directories if they don't exist
mkdir -p "$challenge_dir"
mkdir -p "$test_dir"
mkdir -p "$inputs_example_dir"
mkdir -p "$inputs_challenge_dir"

# Write the class and test content to their respective files
echo "$challenge_content" > "$challenge_file"
echo "$test_content" > "$test_file"
echo "1" > "$inputs_example_file"
echo "1" > "$inputs_challenge_file"

echo "Created challenge file: $challenge_file"
echo "Created test file: $test_file"
echo "Created input files."
