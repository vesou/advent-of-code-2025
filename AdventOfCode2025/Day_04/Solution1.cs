namespace AdventOfCode2025.Day_04;

public class Solution1
{
    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_04/Input.txt");
    }

    public int Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public int Solve(string[] input)
    {
        int result = 0;
        for(int y = 0; y < input.Length; y++)
        {
            for(int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '@')
                {
                    int numberOfSurroundingPositions = GetNumberOfSurroundingPositions(x, y, input);
                    if (numberOfSurroundingPositions < 4)
                    {
                        result++;
                    }
                }
            }
        }
        return result;
    }

    private int GetNumberOfSurroundingPositions(int x, int y, string[] input)
    {
        int numberOfSurroundingPositions = 0;
        for(int checkY = y - 1; checkY <= y + 1; checkY++)
        {
            for(int checkX = x - 1; checkX <= x + 1; checkX++)
            {
                if (checkY < 0 || checkY >= input.Length || checkX < 0 || checkX >= input[y].Length || (checkX == x && checkY == y))
                {
                    continue;
                }
                if (input[checkY][checkX] == '@')
                {
                    numberOfSurroundingPositions++;
                }
            }
        }

        return numberOfSurroundingPositions;
    }
}
