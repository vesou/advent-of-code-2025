namespace AdventOfCode2025.Day_02;

public class Solution2
{
    public string GetInput()
    {
        return System.IO.File.ReadAllLines("Day_02/Input.txt").FirstOrDefault();
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string input)
    {
        long result = 0;
        List<(long left, long right)> inputs = input.Split(',').Select(x => new ValueTuple<long, long>()
        {
            Item1 = long.Parse(x.Split('-')[0]),
            Item2 = long.Parse(x.Split('-')[1])
        }).ToList();
        foreach (var (left, right) in inputs)
        {
            for (long i = left; i <= right; i++)
            {
                string s = i.ToString();
                if (IsRepeating(s))
                {
                    result += i;
                }
            }
        }
        return result;
    }

    // find out if a string is made of repeating substrings eg. 111 => true, 1212 => true, 123123123 => true, 1234 => false
    private bool IsRepeating(string s)
    {
        int length = s.Length;

        for (int subLength = 1; subLength <= length / 2; subLength++)
        {
            if (length % subLength != 0) continue;
            string pattern = s[..subLength];
            bool isRepeating = true;

            // Check if the pattern repeats throughout the string
            for (int i = subLength; i < length; i += subLength)
            {
                if (s.Substring(i, subLength) == pattern) continue;
                isRepeating = false;
                break;
            }

            if (isRepeating)
                return true;
        }

        return false;
    }
}
