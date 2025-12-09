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

    public long Solve(string[] input, int matrixSize = 100_002)
    {
        var result = 0;
        (List<Coordinate> startingCoordinates, byte[][] matrix) = GetCoordinates(input, matrixSize);
        FillInRectangle(startingCoordinates, matrix);
        var distances = GenerateDistances(startingCoordinates, matrix).OrderByDescending(d => d.Item3).ToList();


        return distances.First().Item3;
    }

    private void FillInRectangle(List<Coordinate> startingCoordinates, byte[][] matrix)
    {
        for (int i = 0; i < startingCoordinates.Count - 1; i++)
        {
            if (i == 0)
            {
                ConnectCoordinates(matrix, startingCoordinates[i], startingCoordinates[i + 1]);
                ConnectCoordinates(matrix, startingCoordinates[i], startingCoordinates[^1]);
                continue;
            }

            ConnectCoordinates(matrix, startingCoordinates[i], startingCoordinates[i + 1]);
        }
    }

    private void ConnectCoordinates(byte[][] matrix, Coordinate firstCoordinate, Coordinate secondCoordinate)
    {
        var minX = Math.Min(firstCoordinate.X, secondCoordinate.X);
        var maxX = Math.Max(firstCoordinate.X, secondCoordinate.X);
        var minY = Math.Min(firstCoordinate.Y, secondCoordinate.Y);
        var maxY = Math.Max(firstCoordinate.Y, secondCoordinate.Y);

        for (int x = minX; x <= maxX; x++)
        for (int y = minY; y <= maxY; y++)
        {
            if(matrix[y][x] == 1) continue;
            matrix[y][x] = 2;
        }
    }

    private (List<Coordinate>, byte[][]) GetCoordinates(string[] input, int matrixSize)
    {
        byte[][] matrix = new byte[matrixSize][];
        var coordinates = new List<Coordinate>();
        for (int i = 0; i < matrix.Length; i++)
        {
            matrix[i] = new byte[matrixSize];
        }
        foreach (var line in input)
        {
            var parts = line.Split(',');
            var x = int.Parse(parts[0]);
            var y = int.Parse(parts[1]);

            matrix[y][x] = 1;
            coordinates.Add(new Coordinate(x, y));
        }

        return (coordinates, matrix);
    }

    private List<(Coordinate, Coordinate, long)> GenerateDistances(List<Coordinate> startingCoordinates, byte[][] matrix)
    {
        var distances = new List<(Coordinate, Coordinate, long)>();
        foreach (var coordinate in startingCoordinates.Where(x => x.IsRed))
        foreach (var otherCoordinate in startingCoordinates.Where(x => x.IsRed))
        {
            if (coordinate == otherCoordinate) continue;
            if (AnotherXInsideArea(startingCoordinates, coordinate, otherCoordinate)) continue;
            if (!AtLeast3Walls(matrix, coordinate, otherCoordinate)) continue;
            if (AnotherXInsideArea2(matrix, coordinate, otherCoordinate)) continue;
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

    private bool AnotherXInsideArea2(byte[][] matrix, Coordinate coordinate, Coordinate otherCoordinate)
    {
        int minX = Math.Min(coordinate.X, otherCoordinate.X);
        int maxX = Math.Max(coordinate.X, otherCoordinate.X);
        int minY = Math.Min(coordinate.Y, otherCoordinate.Y);
        int maxY = Math.Max(coordinate.Y, otherCoordinate.Y);
        for (int x = minX + 1; x < maxX; x++)
        {
            for (int y = minY + 1; y < maxY; y++)
            {
                if (matrix[y][x] == 2)
                    return true;
            }
        }

        return false;
    }

    private bool AtLeast3Walls(byte[][] matrix, Coordinate coordinate, Coordinate otherCoordinate)
    {
        int minX = Math.Min(coordinate.X, otherCoordinate.X);
        int maxX = Math.Max(coordinate.X, otherCoordinate.X);
        int minY = Math.Min(coordinate.Y, otherCoordinate.Y);
        int maxY = Math.Max(coordinate.Y, otherCoordinate.Y);

        int wallCount = 0;
        if(matrix[minY][minX+1] != 0 || matrix[minY][maxX-1] != 0)
            wallCount++; // top horizontal
        if(matrix[maxY][minX+1] != 0 || matrix[maxY][maxX-1] != 0)
            wallCount++; // bottom horizontal

        if(matrix[minY+1][minX] != 0 || matrix[maxY -1][minX] != 0)
            wallCount++; // left vertical
        if(matrix[minY+1][maxX] != 0 || matrix[maxY -1][maxX] != 0)
            wallCount++; // right vertical
        return wallCount >= 3;
    }

    private bool AnotherXInsideArea(List<Coordinate> startingCoordinates, Coordinate coordinate,
        Coordinate otherCoordinate)
    {
        return startingCoordinates.Any(c =>
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
