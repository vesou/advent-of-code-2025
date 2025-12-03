namespace AdventOfCode2025.Day_03;

public class Solution2
{
    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_03/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input)
    {
        long result = 0;

        foreach (var line in input)
        {
            int[] biggestDigits = new int[12];
            int digitNumber = 1;
            int maxNumberOfDigits = 12;
            int foundIndex = -1;

            while (digitNumber <= maxNumberOfDigits)
            {
                for(int i = foundIndex + 1; i < line.Length - maxNumberOfDigits + digitNumber; i++)
                {
                    int digit = line[i] - '0';
                    if (digit > biggestDigits[digitNumber - 1])
                    {
                        biggestDigits[digitNumber - 1] = digit;
                        foundIndex = i;
                    }
                }
                digitNumber++;
            }


            result += CalculateNumberFromDigits(biggestDigits);
        }

        return result;
    }

    private long CalculateNumberFromDigits(int[] biggestDigits)
    {
        double number = 0;
        double multiplier = 10;
        int maxMultiplier = 11;

        for(int i = 0; i < biggestDigits.Length; i++)
        {
            number += biggestDigits[i] * (Math.Pow(multiplier, maxMultiplier));
            maxMultiplier--;
        }

        return (long)number;
    }
}
