namespace AdventOfCode2025.Day_05;

public class Solution1
{
    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_05/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input)
    {
        int result = 0;
        int indexOfEmptyLine = Array.IndexOf(input, "");
        List<(long,long)> validRanges = input[0..indexOfEmptyLine]
            .Select(line => line.Split('-'))
            .Select(parts => (long.Parse(parts[0]), long.Parse(parts[1])))
            .ToList();
        List<long> numbersToCheck = input[(indexOfEmptyLine + 1)..]
            .Select(long.Parse)
            .ToList();

        foreach (var number in numbersToCheck)
        {
            bool numberIsValid = validRanges.Any(range => number >= range.Item1 && number <= range.Item2);
            if (numberIsValid) result++;
        }

        return result;
    }
}
