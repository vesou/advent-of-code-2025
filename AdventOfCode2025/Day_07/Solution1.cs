namespace AdventOfCode2025.Day_07;

public class Solution1
{
    private int gridWidth = 0;
    private int gridHeight = 0;

    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_07/Input.txt");
    }

    public int Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public int Solve(string[] input)
    {
        int result = 0;
        char[][] grid = input.Select(line => line.ToCharArray()).ToArray();
        int sIndex = -1;
        gridWidth = grid[0].Length;
        gridHeight = grid.Length;
        for (int y = 0; y < grid.Length; y++)
        {
            if (y == 0)
            {
                sIndex = grid[y].IndexOf('S');
                continue;
            }

            if (y == 1)
            {
                grid[y][sIndex] = '|';
                continue;
            }

            // if this position has ^ then check for pipe above anf if there is one then add pipe before and after this char;
            // else if there is a | above, continue it down
            for (int x = 0; x < grid[y].Length; x++)
            {
                if (grid[y - 1][x] == '|')
                {
                    switch (grid[y][x])
                    {
                        case '.':
                            grid[y][x] = '|';
                            continue;
                        case '^':
                            result++;
                            if(GoodWidth(x-1)) grid[y][x-1] = '|';
                            if(GoodWidth(x+1)) grid[y][x+1] = '|';
                            break;
                    }
                }

            }
        }

        return result;
    }

    private bool GoodWidth(int x)
    {
        return x >= 0 && x < gridWidth;
    }
}
