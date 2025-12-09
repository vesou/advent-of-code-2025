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
        List<Coordinate> redTiles = GetCoordinates(input);
        var greenTiles = AddGreenTiles(redTiles);
        var distances = GenerateDistances(redTiles).OrderByDescending(d => d.Item3).ToList();

        foreach (var (point1, point2, distance) in distances)
        {
            if (!AnotherTileInsideArea(greenTiles, point1, point2))
            {
                return distance;
            }
        }
        return result;
    }

    private List<Coordinate> AddGreenTiles(List<Coordinate> redTiles)
    {
        List<Coordinate> allGreenTiles = new List<Coordinate>();
        for (int i = 0; i < redTiles.Count - 1; i++)
        {
            if (i == 0)
            {
                allGreenTiles.AddRange(ConnectCoordinates(redTiles[i], redTiles[^1]));
            }

            allGreenTiles.AddRange(ConnectCoordinates(redTiles[i], redTiles[i + 1]));
        }

        return allGreenTiles;
    }

    private List<Coordinate> ConnectCoordinates(Coordinate point1, Coordinate point2)
    {
        List<Coordinate> greenTiles = new List<Coordinate>();
        if (point1.X == point2.X)
        {
            // go vertical
            var minY = Math.Min(point1.Y, point2.Y);
            var maxY = Math.Max(point1.Y, point2.Y);
            for (int y = minY + 1; y < maxY; y++)
            {
                greenTiles.Add(new Coordinate(point1.X, y, isRed: false));
            }

            return greenTiles;
        }

        // go horizontal
        var minX = Math.Min(point1.X, point2.X);
        var maxX = Math.Max(point1.X, point2.X);
        for (int x = minX + 1; x < maxX; x++)
        {
            greenTiles.Add(new Coordinate(x, point1.Y, isRed: false));
        }
        return greenTiles;
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

    private List<(Coordinate, Coordinate, long)> GenerateDistances(List<Coordinate> redTiles)
    {
        var distances = new List<(Coordinate, Coordinate, long)>();
        foreach (var point1 in redTiles)
        foreach (var point2 in redTiles)
        {
            if (point1 == point2) continue;
            if (point1.X == point2.X || point1.Y == point2.Y) continue;
            if(AnotherTileInsideArea(redTiles, point1, point2)) continue;
            var distance = point1.RectangleArea(point2);
            distances.Add((point1, point2, distance));
        }

        distances = distances
            .GroupBy(d => d.Item1.GetHashCode() < d.Item2.GetHashCode()
                ? (d.Item1, d.Item2)
                : (d.Item2, d.Item1))
            .Select(g => g.First())
            .ToList();

        return distances;
    }

    private static bool AnotherTileInsideArea(List<Coordinate> tiles, Coordinate point1,
        Coordinate point2)
    {
        return tiles.Any(c =>
            c.X > Math.Min(point1.X, point2.X) && c.X < Math.Max(point1.X, point2.X) &&
            c.Y > Math.Min(point1.Y, point2.Y) && c.Y < Math.Max(point1.Y, point2.Y));
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
            return (long)((Math.Abs(X - other.X) + 1) * 1.0) * (Math.Abs(Y - other.Y) + 1);
        }
    }
}
