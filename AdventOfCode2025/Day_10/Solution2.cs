using Google.OrTools.LinearSolver;

namespace AdventOfCode2025.Day_10;

public class Solution2
{
    public string[] GetInput()
    {
        return File.ReadAllLines("Day_10/Input.txt");
    }

    public long Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public long Solve(string[] input)
    {
        long result = 0;
        var data = GetData(input);
        foreach (var item in data)
        {
            HashSet<string> memo = new HashSet<string>();
            memo.Add(ToKey(new int[item.Joltages.Count]));
            result += CalculatePresses3(item.Joltages.ToArray(), item.ButtonWirings);
        }

        return result;
    }

    private long CalculatePresses3(int[] numbersToReach, List<List<int>> listOfLinkedIndexes)
    {
        // Use Google OR-Tools to solve this as an Integer Linear Programming problem
        // Objective: minimize sum of button presses
        // Constraints: each counter must reach exactly its target value

        Solver solver = Solver.CreateSolver("SCIP");
        if (solver == null)
        {
            Console.WriteLine("Could not create solver SCIP");
            return -1;
        }

        int numButtons = listOfLinkedIndexes.Count;
        int numCounters = numbersToReach.Length;

        // Create variables: number of presses for each button
        Variable[] presses = new Variable[numButtons];
        for (int i = 0; i < numButtons; i++)
        {
            presses[i] = solver.MakeIntVar(0, 10000, $"button_{i}");
        }

        // Add constraints: each counter must reach its target value
        for (int counter = 0; counter < numCounters; counter++)
        {
            LinearExpr constraint = new LinearExpr();

            // Sum up contributions from all buttons that affect this counter
            for (int btn = 0; btn < numButtons; btn++)
            {
                if (listOfLinkedIndexes[btn].Contains(counter))
                {
                    constraint += presses[btn];
                }
            }

            // Constraint: this counter must equal its target
            solver.Add(constraint == numbersToReach[counter]);
        }

        // Objective: minimize total button presses
        LinearExpr objective = new LinearExpr();
        foreach (var pressVar in presses)
        {
            objective += pressVar;
        }
        solver.Minimize(objective);

        // Solve the problem
        Solver.ResultStatus resultStatus = solver.Solve();

        if (resultStatus == Solver.ResultStatus.OPTIMAL)
        {
            return (long)solver.Objective().Value();
        }
        else if (resultStatus == Solver.ResultStatus.FEASIBLE)
        {
            Console.WriteLine("Found feasible solution (not proven optimal)");
            return (long)solver.Objective().Value();
        }
        else
        {
            Console.WriteLine($"Could not solve problem. Status: {resultStatus}");
            return -1;
        }
    }

    private long CalculateDistance(int[] current, int[] target)
    {
        long distance = 0;
        for (int i = 0; i < current.Length; i++)
        {
            distance += target[i] - current[i];
        }
        return distance;
    }

    private long CalculatePresses4(int[] numbersToReach, List<List<int>> listOfLinkedIndexes)
    {
        // Greedy approach: work backwards, find buttons that affect fewest unfilled counters
        int[] current = new int[numbersToReach.Length];
        long totalPresses = 0;

        // Keep working until we reach the goal
        while (!current.SequenceEqual(numbersToReach))
        {
            // Find the best button to press
            int bestButton = -1;
            int bestPresses = int.MaxValue;
            double bestScore = double.MinValue;

            for (int buttonIdx = 0; buttonIdx < listOfLinkedIndexes.Count; buttonIdx++)
            {
                var buttonWiring = listOfLinkedIndexes[buttonIdx];

                // Calculate how many times we can press this button
                int maxPresses = int.MaxValue;
                int affectsNeeded = 0;
                int totalAffected = buttonWiring.Count;

                foreach (var index in buttonWiring)
                {
                    int remaining = numbersToReach[index] - current[index];
                    if (remaining > 0)
                    {
                        affectsNeeded++;
                        if (remaining < maxPresses)
                            maxPresses = remaining;
                    }
                    else if (remaining == 0)
                    {
                        maxPresses = 0;
                        break;
                    }
                }

                if (maxPresses <= 0)
                    continue;

                // Score: prefer buttons that affect more needed counters and fewer total counters
                // Also prefer to complete counters
                double score = (double)affectsNeeded / totalAffected;

                // Bonus for completing a counter
                bool completesCounter = false;
                foreach (var index in buttonWiring)
                {
                    int remaining = numbersToReach[index] - current[index];
                    if (remaining == maxPresses)
                    {
                        completesCounter = true;
                        break;
                    }
                }

                if (completesCounter)
                    score += 10.0;

                // Prefer pressing fewer times (tie breaker)
                score -= maxPresses * 0.001;

                if (score > bestScore)
                {
                    bestScore = score;
                    bestButton = buttonIdx;
                    bestPresses = maxPresses;
                }
            }

            if (bestButton == -1)
            {
                // No valid button found - shouldn't happen
                return -1;
            }

            // Press the best button
            foreach (var index in listOfLinkedIndexes[bestButton])
            {
                current[index] += bestPresses;
            }
            totalPresses += bestPresses;
        }

        return totalPresses;
    }


    private static string ToKey(int[] joltages)
    {
        return string.Join(",", joltages);
    }

    private static List<Data> GetData(string[] input)
    {
        var dataList = new List<Data>();
        foreach (var line in input)
        {
            var lightDiagramEnd = line.IndexOf(']');
            var lightDiagram = line[1..lightDiagramEnd].Trim();

            var buttonWiringsStart = line.IndexOf('(');
            var buttonWiringsEnd = line.IndexOf('{');
            var buttonWiringsPart = line[buttonWiringsStart..(buttonWiringsEnd - 1)].Trim();
            var buttonWiringsStrings = buttonWiringsPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var buttonWirings = new List<List<int>>();
            foreach (var wiringString in buttonWiringsStrings)
            {
                var wiringNumbers = wiringString.Trim('(', ')').Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToList();
                buttonWirings.Add(wiringNumbers);
            }

            var joltagesPart = line[buttonWiringsEnd..].Trim();
            var joltagesString = joltagesPart.Trim('{', '}');
            var joltages = joltagesString.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();

            byte[] lightDiagramBytes = new byte[lightDiagram.Length];
            for (int i = 0; i < lightDiagram.Length; i++)
            {
                lightDiagramBytes[i] = lightDiagram[i] == '#' ? (byte)1 : (byte)0;
            }

            dataList.Add(new Data
            {
                LightDiagram = lightDiagramBytes,
                ButtonWirings = buttonWirings,
                Joltages = joltages
            });
        }
        return dataList;
    }

    public class Data
    {
        public byte[] LightDiagram { get; set; }
        public List<List<int>> ButtonWirings { get; set; }
        public List<int> Joltages { get; set; }
    }
}
