namespace AdventOfCode2025.Day_11;

public class Solution2
{
    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_11/Input.txt");
    }

    public int Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public int Solve(string[] input)
    {
        int result = 0;
        string startingPoint = "you";
        string target = "out";
        var graph = new Dictionary<string, List<string>>();
        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var node = parts[0];
            var neighbors = parts[1].Split(' ').ToList();
            graph[node] = neighbors.Select(x => x.Trim()).ToList();
        }
        var queue = new Queue<(string node, int depth)>();
        var visited = new HashSet<string>();
        queue.Enqueue((startingPoint, 0));
        //visited.Add(startingPoint);
        while (queue.Count > 0)
        {
            var (currentNode, depth) = queue.Dequeue();
            if (currentNode == target)
            {
                // result = depth;
                result++;
                continue;
            }
            foreach (var neighbor in graph[currentNode])
            {
                // if (!visited.Contains(neighbor))
                // {
                //     visited.Add(neighbor);
                queue.Enqueue((neighbor, depth + 1));
                // }
            }
        }
        return result;
    }
}
