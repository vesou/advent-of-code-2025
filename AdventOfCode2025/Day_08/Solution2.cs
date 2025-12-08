namespace AdventOfCode2025.Day_08;

public class Solution2
{
    public string[] GetInput()
    {
        return File.ReadAllLines("Day_08/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input, int numberOfConnections = 1000, int numberOfMultipliers = 3)
    {
        var startingCoordinates = GetCoordinates(input);
        var distances = GenerateDistances(startingCoordinates).OrderBy(d => d.Item3).ToList();

        var circuits = startingCoordinates
            .Select(c =>
            {
                var circuit = new Circuit();
                circuit.AddConnection(c);
                return circuit;
            }).ToList();

        (Coordinate, Coordinate, double) bestPair = (new Coordinate(0,0,0), new Coordinate(0,0,0), 0);
        int i = 0;
        while(circuits.Count > 1)
        {
            bestPair = distances[i++];
            circuits = OrganiseCircuits(circuits, bestPair.Item1, bestPair.Item2);
        }

        double result = bestPair.Item1.X * 1.0 * bestPair.Item2.X;

        return (long)result;
    }


    private List<(Coordinate, Coordinate, double)> GenerateDistances(List<Coordinate> startingCoordinates)
    {
        var distances = new List<(Coordinate, Coordinate, double)>();
        foreach (var coordinate in startingCoordinates)
        foreach (var otherCoordinate in startingCoordinates)
        {
            if (coordinate == otherCoordinate) continue;
            var distance = coordinate.DistanceTo(otherCoordinate);
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

    private static List<Circuit> OrganiseCircuits(List<Circuit> circuits, Coordinate bestPairItem1,
        Coordinate bestPairItem2)
    {
        var circuit1 = circuits.FirstOrDefault(c => c.Connections.Contains(bestPairItem1));
        var circuit2 = circuits.FirstOrDefault(c => c.Connections.Contains(bestPairItem2));

        if (circuit1 == null && circuit2 == null)
        {
            var newCircuit = new Circuit();
            newCircuit.AddConnection(bestPairItem1);
            newCircuit.AddConnection(bestPairItem2);
            circuits.Add(newCircuit);
        }
        else if (circuit1 != null && circuit2 == null)
        {
            circuit1.AddConnection(bestPairItem2);
        }
        else if (circuit1 == null && circuit2 != null)
        {
            circuit2.AddConnection(bestPairItem1);
        }
        else if (circuit1 != null && circuit2 != null && circuit1 != circuit2)
        {
            foreach (var connection in circuit2.Connections) circuit1.AddConnection(connection);
            circuits.Remove(circuit2);
        }

        return circuits;
    }

    private List<Coordinate> GetCoordinates(string[] input)
    {
        var coordinates = new List<Coordinate>();
        foreach (var line in input)
        {
            var parts = line.Split(',');
            var x = int.Parse(parts[0]);
            var y = int.Parse(parts[1]);
            var z = int.Parse(parts[2]);
            coordinates.Add(new Coordinate(x, y, z));
        }

        return coordinates;
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override int GetHashCode()
        {
            return (X, Y, Z).GetHashCode();
        }

        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double DistanceTo(Coordinate other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
        }
    }

    public class Circuit
    {
        public List<Coordinate> Connections { get; set; }

        public long GetSize()
        {
            return Connections.Count;
        }

        public void AddConnection(Coordinate coordinate)
        {
            Connections.Add(coordinate);
        }

        public Circuit()
        {
            Connections = new List<Coordinate>();
        }
    }
}
