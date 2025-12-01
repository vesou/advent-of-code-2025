namespace AdventOfCode2025.Day_01;

public class Solution1
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
                currentPosition = (currentPosition - value) % 100;
            }
            else if (direction == 'R')
            {
                currentPosition = (currentPosition + value) % 100;
            }

            if (currentPosition == 0)
            {
                result++;
            }
        }

        return result;
    }
}
