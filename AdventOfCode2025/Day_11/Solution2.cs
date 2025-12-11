namespace AdventOfCode2025.Day_11;

public class Solution2
{
    Dictionary<string, List<string>> graph = new();
    Dictionary<(string, bool, bool), long> memo = new();
    string point1 = "dac";
    string point2 = "fft";
    string target = "out";

    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_11/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input)
    {
        string startingPoint = "svr";

        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var node = parts[0];
            var neighbors = parts[1].Split(' ').ToList();
            graph[node] = neighbors.Select(x => x.Trim()).ToList();
        }

        return DFS(startingPoint, false, false);
    }

    long DFS(string currentNode, bool visited1, bool visited2)
    {
        long result = 0;

        if (currentNode == target)
        {
            if(visited1 && visited2)
            {
                return 1;
            }
            return 0;
        }

        visited1 |= currentNode == point1;
        visited2 |= currentNode == point2;

        if (memo.ContainsKey((currentNode, visited1, visited2)))
        {
            return memo[(currentNode, visited1, visited2)];
        }

        foreach (var neighbor in graph[currentNode])
        {
            result += DFS(neighbor, visited1, visited2);
        }

        memo[(currentNode, visited1, visited2)] = result;
        return result;
    }
}
