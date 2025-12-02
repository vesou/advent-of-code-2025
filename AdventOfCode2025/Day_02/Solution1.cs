namespace AdventOfCode2025.Day_02;

public class Solution1
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
                if(s.Length % 2 != 0) continue;
                if (IsRepeating(s))
                {
                    result += i;
                }
            }
        }
        return result;
    }

    private bool IsRepeating(string s)
    {
        int len = s.Length;
        for (int i = 0; i < len / 2; i++)
        {
            if (s[i] != s[i + len / 2])
            {
                return false;
            }
        }
        return true;
    }
}
