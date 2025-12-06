namespace AdventOfCode2025.Day_06;

public class Solution1
{
    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_06/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input)
    {
        long result = 0;
        long[][] numberLines = input[..^1]
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray())
            .ToArray();
        string[] operatorLine = input[^1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int columns = numberLines[0].Length;
        for (int col = 0; col < columns; col++)
        {
            switch (operatorLine[col])
            {
                case "+":
                    result += numberLines.Sum(row => row[col]);
                    break;
                case "*":
                    long multiplyResult = 1;
                    foreach (var row in numberLines)
                    {
                        multiplyResult *= row[col];
                    }
                    result += multiplyResult;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown operator: {operatorLine[col]}");
            }
        }

        return result;
    }
}
