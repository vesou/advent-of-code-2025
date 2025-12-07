namespace AdventOfCode2025.Day_07;

public class Solution2
{
    private int gridWidth = 0;
    private int gridHeight = 0;
    private long[][] memo;

    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_07/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input)
    {
        long result = 0;
        char[][] grid = input.Select(line => line.ToCharArray()).ToArray();
        memo = new long[grid.Length][];
        for (int i = 0; i < grid.Length; i++)
        {
            memo[i] = new long[grid[0].Length];
        }

        int sIndex = -1;
        gridWidth = grid[0].Length;
        gridHeight = grid.Length;
        for (int y = 0; y < grid.Length; y++)
        {
            switch (y)
            {
                case 0:
                    sIndex = grid[y].IndexOf('S');
                    continue;
                case 1:
                    grid[y][sIndex] = '|';
                    continue;
            }

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
                            if(GoodWidth(x-1)) grid[y][x-1] = '|';
                            if(GoodWidth(x+1)) grid[y][x+1] = '|';
                            break;
                    }
                }
            }
        }

        // grid is populated so now i can count possible paths
        // for every ^ i can go left and right so need to return all possible paths from those two positions
        result = GetPossiblePaths(grid, 1, sIndex);


        return result;
    }

    private long GetPossiblePaths(char[][] grid, int yStart, int xStart)
    {
        if (yStart >= gridHeight - 1)
        {
            return 1;
        }

        if (xStart < 0 || xStart >= gridWidth)
        {
            return 0;
        }

        if (memo[yStart][xStart] != 0)
        {
            return memo[yStart][xStart];
        }

        if(grid[yStart+1][xStart] == '^')
        {
            var res = GetPossiblePaths(grid, yStart + 1, xStart - 1) + GetPossiblePaths(grid, yStart + 1, xStart + 1);
            memo[yStart][xStart] = res;
            return res;
        }

        var result = GetPossiblePaths(grid, yStart + 1, xStart);
        memo[yStart][xStart] = result;
        return result;
    }

    private bool GoodWidth(int x)
    {
        return x >= 0 && x < gridWidth;
    }
}
