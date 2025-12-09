namespace AdventOfCode2025.Day_09;

public class Solution1
{
    public string[] GetInput()
    {
        return System.IO.File.ReadAllLines("Day_09/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input)
    {
        int result = 0;
        var startingCoordinates = GetCoordinates(input);
        var distances = GenerateDistances(startingCoordinates).OrderByDescending(d => d.Item3).ToList();


        return distances.First().Item3;
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
        foreach (var coordinate in startingCoordinates)
        foreach (var otherCoordinate in startingCoordinates)
        {
            if (coordinate == otherCoordinate) continue;
            var distance = coordinate.RectangleArea(otherCoordinate);
            distances.Add((coordinate, otherCoordinate, distance));
        }

        distances = distances
            .GroupBy(d => (d.Item1.GetHashCode() < d.Item2.GetHashCode())
                ? (d.Item1, d.Item2)
                : (d.Item2, d.Item1))
            .Select(g => g.First())
            .ToList();

        return distances;
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public long RectangleArea(Coordinate other)
        {
            return (long)((Math.Abs(X - other.X) + 1) * 1.0) * (long)(Math.Abs(Y - other.Y) + 1);
        }
    }
}
