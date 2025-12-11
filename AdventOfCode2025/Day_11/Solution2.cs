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

        // Count paths using dynamic programming approach
        // We need paths that go through both point1 and point2
        // Two possible orderings: start -> point1 -> point2 -> target
        //                      or: start -> point2 -> point1 -> target

        int CountPaths(string from, string to, HashSet<string> forbidden)
        {
            int count = 0;
            var visited = new HashSet<string>();

            void DFS(string currentNode)
            {
                if (currentNode == to)
                {
                    count++;
                    return;
                }

                foreach (var neighbor in graph[currentNode])
                {
                    if (visited.Contains(neighbor) || forbidden.Contains(neighbor))
                        continue;

                    visited.Add(neighbor);
                    DFS(neighbor);
                    visited.Remove(neighbor);
                }
            }

            visited.Add(from);
            DFS(from);
            return count;
        }

        // Path 1: start -> point1 -> point2 -> target
        var forbiddenForStartToPoint1 = new HashSet<string> { point2, target };
        var forbiddenForPoint1ToPoint2 = new HashSet<string> { target };
        var forbiddenForPoint2ToTarget = new HashSet<string>();

        int pathsStartToPoint1 = CountPaths(startingPoint, point1, forbiddenForStartToPoint1);
        int pathsPoint1ToPoint2 = CountPaths(point1, point2, forbiddenForPoint1ToPoint2);
        int pathsPoint2ToTarget = CountPaths(point2, target, forbiddenForPoint2ToTarget);

        int ordering1 = pathsStartToPoint1 * pathsPoint1ToPoint2 * pathsPoint2ToTarget;

        // Path 2: start -> point2 -> point1 -> target
        var forbiddenForStartToPoint2 = new HashSet<string> { point1, target };
        var forbiddenForPoint2ToPoint1 = new HashSet<string> { target };
        var forbiddenForPoint1ToTarget = new HashSet<string>();

        int pathsStartToPoint2 = CountPaths(startingPoint, point2, forbiddenForStartToPoint2);
        int pathsPoint2ToPoint1 = CountPaths(point2, point1, forbiddenForPoint2ToPoint1);
        int pathsPoint1ToTarget = CountPaths(point1, target, forbiddenForPoint1ToTarget);

        int ordering2 = pathsStartToPoint2 * pathsPoint2ToPoint1 * pathsPoint1ToTarget;

        return ordering1 + ordering2;
    }
}
