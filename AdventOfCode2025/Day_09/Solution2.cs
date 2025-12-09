namespace AdventOfCode2025.Day_09;

public class Solution2
{
    public string[] GetInput()
    {
        return File.ReadAllLines("Day_09/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input)
    {
        var result = 0;
        var startingCoordinates = GetCoordinates(input);
        startingCoordinates = FillInRectangle(startingCoordinates);
        var distances = GenerateDistances(startingCoordinates).OrderByDescending(d => d.Item3).ToList();


        return distances.First().Item3;
    }

    private List<Coordinate> FillInRectangle(List<Coordinate> startingCoordinates)
    {
        for (int i = 0; i < startingCoordinates.Count - 1; i++)
        {
            if (i == 0)
            {
                ConnectCoordinates(startingCoordinates, startingCoordinates[i], startingCoordinates[i + 1]);
                ConnectCoordinates(startingCoordinates, startingCoordinates[i], startingCoordinates[^1]);
                continue;
            }

            ConnectCoordinates(startingCoordinates, startingCoordinates[i], startingCoordinates[i + 1]);
        }

        return startingCoordinates;
    }

    private void ConnectCoordinates(List<Coordinate> startingCoordinates, Coordinate firstCoordinate, Coordinate secondCoordinate)
    {
        var minX = Math.Min(firstCoordinate.X, secondCoordinate.X);
        var maxX = Math.Max(firstCoordinate.X, secondCoordinate.X);
        var minY = Math.Min(firstCoordinate.Y, secondCoordinate.Y);
        var maxY = Math.Max(firstCoordinate.Y, secondCoordinate.Y);

        for (int x = minX; x <= maxX; x++)
        for (int y = minY; y <= maxY; y++)
        {
            var newCoordinate = new Coordinate(x, y, false);
            if(newCoordinate.X == firstCoordinate.X && newCoordinate.Y == firstCoordinate.Y)
                continue;
            startingCoordinates.Add(newCoordinate);
        }
    }

    private List<Coordinate> GetCoordinates(string[] input)
    {
        var coordinates = new List<Coordinate>();
        foreach (var line in input)
        {
            var parts = line.Split(',');
            var x = int.Parse(parts[0]);
            var y = int.Parse(parts[1]);
            coordinates.Add(new Coordinate(x, y));
        }

        return coordinates;
    }

    private List<(Coordinate, Coordinate, long)> GenerateDistances(List<Coordinate> startingCoordinates)
    {
        var distances = new List<(Coordinate, Coordinate, long)>();
        foreach (var coordinate in startingCoordinates.Where(x => x.IsRed))
        foreach (var otherCoordinate in startingCoordinates.Where(x => x.IsRed))
        {
            if (coordinate == otherCoordinate) continue;
            if (AnotherXInsideArea(startingCoordinates, coordinate, otherCoordinate)) continue;
            if (!AtLeast3Walls(startingCoordinates, coordinate, otherCoordinate)) continue;
            var distance = coordinate.RectangleArea(otherCoordinate);
            distances.Add((coordinate, otherCoordinate, distance));
        }

        distances = distances
            .GroupBy(d => d.Item1.GetHashCode() < d.Item2.GetHashCode()
                ? (d.Item1, d.Item2)
                : (d.Item2, d.Item1))
            .Select(g => g.First())
            .ToList();

        return distances;
    }

    private bool AtLeast3Walls(List<Coordinate> startingCoordinates, Coordinate coordinate, Coordinate otherCoordinate)
    {
        int minX = Math.Min(coordinate.X, otherCoordinate.X);
        int maxX = Math.Max(coordinate.X, otherCoordinate.X);
        int minY = Math.Min(coordinate.Y, otherCoordinate.Y);
        int maxY = Math.Max(coordinate.Y, otherCoordinate.Y);

        int wallCount = 0;
        if(startingCoordinates.Any(c => (c.X == minX + 1 && c.Y == minY) || (c.X == maxX - 1 && c.Y == minY)))
            wallCount++; // top horizontal
        if(startingCoordinates.Any(c => (c.X == minX + 1 && c.Y == maxY) || (c.X == maxX - 1 && c.Y == maxY)))
            wallCount++; // bottom horizontal

        if(startingCoordinates.Any(c => (c.X == minX && c.Y == minY + 1) || (c.X == minX && c.Y == maxY - 1)))
            wallCount++; // left vertical
        if(startingCoordinates.Any(c => (c.X == maxX && c.Y == minY + 1) || (c.X == maxX && c.Y == maxY - 1)))
            wallCount++; // right vertical
        return wallCount >= 3;
    }

    private bool AnotherXInsideArea(List<Coordinate> startingCoordinates, Coordinate coordinate,
        Coordinate otherCoordinate)
    {
        return startingCoordinates.Any(c =>
            !c.IsRed &&
            c.X > Math.Min(coordinate.X, otherCoordinate.X) && c.X < Math.Max(coordinate.X, otherCoordinate.X) &&
            c.Y > Math.Min(coordinate.Y, otherCoordinate.Y) && c.Y < Math.Max(coordinate.Y, otherCoordinate.Y));
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsRed { get; set; }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        public Coordinate(int x, int y, bool isRed = true)
        {
            X = x;
            Y = y;
            IsRed = isRed;
        }

        public long RectangleArea(Coordinate other)
        {
            return (long)((Math.Abs(X - other.X) + 1) * 1.0) * (long)(Math.Abs(Y - other.Y) + 1);
        }
    }
}
