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
        string startingPoint = "svr";
        string point1 = "dac";
        string point2 = "fft";
        string target = "out";
        var graph = new Dictionary<string, List<string>>();
        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var node = parts[0];
            var neighbors = parts[1].Split(' ').ToList();
            graph[node] = neighbors.Select(x => x.Trim()).ToList();
        }

        // Each queue item now includes the path taken so far to detect cycles
        var queue = new Queue<(string currentNode,HashSet<string> visitedNodes)>();

        queue.Enqueue((startingPoint, [startingPoint]));

        while (queue.Count > 0)
        {
            var (currentNode, pathVisited) = queue.Dequeue();
            if (currentNode == target)
            {
                if (pathVisited.Contains(point1) && pathVisited.Contains(point2))
                {
                    result++;
                }
                continue;
            }

            foreach (var neighbor in graph[currentNode])
            {
                if (pathVisited.Contains(neighbor)) continue;
                var newPathVisited = new HashSet<string>(pathVisited) { neighbor };
                queue.Enqueue((neighbor, newPathVisited));
            }
        }
        return result;
    }
}
