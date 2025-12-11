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
        var allPaths = new List<string>();
        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var node = parts[0];
            var neighbors = parts[1].Split(' ').ToList();
            graph[node] = neighbors.Select(x => x.Trim()).ToList();
        }
        var queue = new Queue<(string node, int depth, bool visited1, bool visited2)>();

        queue.Enqueue((startingPoint, 0, false, false));
        //visited.Add(startingPoint);
        while (queue.Count > 0)
        {
            var (currentNode, depth, visited1, visited2) = queue.Dequeue();
            if (currentNode == target)
            {
                // result = depth;
                if (visited1 && visited2)
                {
                    result++;
                }

                continue;
            }

            if (currentNode == point1)
            {
                visited1 = true;
            }
            if (currentNode == point2)
            {
                visited2 = true;
            }

            foreach (var neighbor in graph[currentNode])
            {
                // if (!visited.Contains(neighbor))
                // {
                //     visited.Add(neighbor);
                queue.Enqueue((neighbor, depth + 1, visited1, visited2));
                // }
            }
        }
        return result;
    }
}
