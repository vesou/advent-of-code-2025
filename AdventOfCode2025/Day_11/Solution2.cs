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

        int result = 0;
        var visited = new HashSet<string>();

        void DFS(string currentNode)
        {
            if (currentNode == target)
            {
                if (visited.Contains(point1) && visited.Contains(point2))
                {
                    result++;
                }
                return;
            }

            foreach (var neighbor in graph[currentNode])
            {
                if (visited.Contains(neighbor)) continue;

                visited.Add(neighbor);
                DFS(neighbor);
                visited.Remove(neighbor);
            }
        }

        visited.Add(startingPoint);
        DFS(startingPoint);

        return result;
    }
}
