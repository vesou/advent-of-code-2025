namespace AdventOfCode2025.Day_02;

public class Solution2
{
    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_01/Input.txt");
    }

    public int Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public int Solve(string[] input)
    {
        int result = 0;
        int currentPosition = 50;

        foreach (var line in input)
        {
            char direction = line[0];
            int value = int.Parse(line.Substring(1));

            if (direction == 'L')
            {
                int midStep = currentPosition - value;
                if ((currentPosition == 0 && value > 0) || (currentPosition != 0 && midStep <= 0))
                {
                    midStep = Math.Abs(midStep);
                    double numberOfTimesCrossed = Math.Floor(midStep / 100.0) + (currentPosition == 0 ? 0 : 1);
                    result += (int)numberOfTimesCrossed;
                }
                currentPosition = ((currentPosition - value) % 100 + 100) % 100;
            }
            else if (direction == 'R')
            {
                int midStep = currentPosition + value;
                if (midStep >= 100)
                {
                    double numberOfTimesCrossed = Math.Floor(midStep / 100.0);
                    result += (int)numberOfTimesCrossed;
                }
                currentPosition = (currentPosition + value) % 100;
            }
        }

        return result;
    }
}
