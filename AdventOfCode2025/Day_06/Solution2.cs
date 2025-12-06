namespace AdventOfCode2025.Day_06;

public class Solution2
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
        long[][] numberLines = GetNumberLines(input);
        string[] operatorLine = input[^1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int numberOfOperations = operatorLine.Length;
        for (int col = 0; col < numberOfOperations; col++)
        {
            switch (operatorLine[col])
            {
                case "+":
                    result += numberLines[col].Sum();
                    break;
                case "*":
                    long multiplyResult = 1;
                    foreach (var row in numberLines[col])
                    {
                        multiplyResult *= row;
                    }
                    result += multiplyResult;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown operator: {operatorLine[col]}");
            }
        }

        return result;
    }

    private long[][] GetNumberLines(string[] input)
    {
        char[][] data = input[..^1].Select(line => line.ToCharArray()).ToArray();
        List<long[]> numberLines = new();
        int previousSplitIndex = -1;
        for(int columnIndex = 0; columnIndex < data[0].Length; columnIndex++)
        {
            // check if this is a split position and if it is extract the numbers
            if (data.Any(line => line[columnIndex] != ' ') && columnIndex < data[0].Length - 1) continue;
            // extract numbers
            int maxLengthModifier = columnIndex < data[0].Length - 1 ? 1 : 0; // check if it's the last column
            int maxNumberLength = columnIndex - previousSplitIndex - maxLengthModifier;
            long[] numbers = new long[maxNumberLength];
            for (int digitIndex = 0; digitIndex < maxNumberLength; digitIndex++)
            {
                string numberString = GetDigitsAtPositionAsNumber(previousSplitIndex + 1 + digitIndex, data);
                numbers[digitIndex] = long.Parse(numberString);
            }
            numberLines.Add(numbers);
            previousSplitIndex = columnIndex;
        }

        return numberLines.ToArray();
    }


    private string GetDigitsAtPositionAsNumber(int columnIndex, char[][] initialSplit)
    {
        string result = "";
        try
        {
            for (int lineIndex = 0; lineIndex < initialSplit.Length; lineIndex++)
            {
                char digit = initialSplit[lineIndex][columnIndex];
                result += digit;
            }

            result = result.Trim();

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
