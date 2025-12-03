namespace AdventOfCode2025.Day_03;

public class Solution1
{
    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_03/Input.txt");
    }

    public int Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public int Solve(string[] input)
    {
        int result = 0;

        foreach (var line in input)
        {
            int firstBiggestDigit = 0;
            int firstIndex = 0;
            int secondBiggestDigit = 0;

            // find first biggest digit
            for(int i = 0; i < line.Length - 1; i++)
            {
                int digit = line[i] - '0';
                if (digit > firstBiggestDigit)
                {
                    firstBiggestDigit = digit;
                    firstIndex = i;
                }
            }

            // find first biggest digit
            for(int i = firstIndex + 1; i < line.Length; i++)
            {
                int digit = line[i] - '0';
                if (digit > secondBiggestDigit)
                {
                    secondBiggestDigit = digit;
                }
            }

            result += firstBiggestDigit * 10 + secondBiggestDigit;
        }

        return result;
    }
}
