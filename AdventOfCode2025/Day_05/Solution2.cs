namespace AdventOfCode2025.Day_05;

public class Solution2
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
        long result = 0;
        int indexOfEmptyLine = Array.IndexOf(input, "");
        List<(long,long)> validRanges = input[0..indexOfEmptyLine]
            .Select(line => line.Split('-'))
            .Select(parts => (long.Parse(parts[0]), long.Parse(parts[1])))
            .OrderBy(range => range.Item1)
            .ToList();

        List<(long, long)> consolidatedRanges = new List<(long, long)>();

        long previousMax = -1;
        for(int i = 0; i < validRanges.Count; i++)
        {
            if(validRanges[i].Item1 <= previousMax)
            {
                continue;
            }

            var currentRange = validRanges[i];
            var previousRange = new ValueTuple<long, long>(-1, -1);
            while(currentRange != previousRange)
            {
                previousRange = currentRange;
                List<(long, long)> overlappingRanges = validRanges[(i + 1)..]
                    .Where(range => range.Item1 <= currentRange.Item2)
                    .ToList();

                foreach (var overlap in overlappingRanges)
                {
                    long newMax = Math.Max(currentRange.Item2, overlap.Item2);
                    currentRange = (currentRange.Item1, newMax);
                }
            }

            consolidatedRanges.Add(currentRange);
            previousMax = currentRange.Item2;
        }

        foreach (var range in consolidatedRanges)
        {
            result += (range.Item2 - range.Item1 + 1);
        }

        return result;
    }
}
