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
            result += CalculatePresses2(item.Joltages.ToArray(), item.ButtonWirings);
        }

        return result;
    }

    private long CalculatePresses2(int[] joltages, List<List<int>> possibleButtonPresses)
    {
        long numPresses = 0;
        int[] previousJoltage = new int[joltages.Length];
        List<(int[], int)> possibleSolutions = new List<(int[], int)>();
        possibleSolutions.Add((previousJoltage, 0));
        for (int joltageIndex = 0; joltageIndex < joltages.Length; joltageIndex++)
        {
            List<(int[], int)> newSolutions = new List<(int[], int)>();
            foreach ((int[] currentState, int numberOfPressesSoFar) in possibleSolutions)
            {
                if(currentState[joltageIndex] == joltages[joltageIndex])
                {
                    newSolutions.Add((currentState, numberOfPressesSoFar));
                    continue;
                }

                var newSolutions2 = FindAllCombinationsThatAffectIndex(possibleButtonPresses, joltageIndex, currentState, joltages, numberOfPressesSoFar);
                newSolutions2 = FilterThoseThatHaveTooMuchPower(newSolutions2, joltages);

                newSolutions.AddRange(newSolutions2);
            }
            newSolutions = DeDuplicate(newSolutions);
            possibleSolutions = newSolutions;
        }
        numPresses = possibleSolutions.Min(x => x.Item2);
        return numPresses;
    }

    // deduplicate newSolutions2 just based on key and take the smallest number of presses
    private List<(int[], int)> DeDuplicate(List<(int[], int)> solutions)
    {
        Dictionary<string, int> deduplicated = new Dictionary<string, int>();
        foreach (var (joltageArray, numberOfPresses) in solutions)
        {
            string key = ToKey(joltageArray);
            if (!deduplicated.ContainsKey(key) || deduplicated[key] > numberOfPresses)
            {
                deduplicated[key] = numberOfPresses;
            }
        }

        return deduplicated.Select(kv => (kv.Key.Split(",").Select(int.Parse).ToArray(), kv.Value)).ToList();
    }

    private List<(int[], int)> FilterThoseThatHaveTooMuchPower(List<(int[], int)> possibleButtonPresses, int[] joltages)
    {
        return possibleButtonPresses.Where(x => x.Item1.Zip(joltages, (a, b) => a <= b).All(b => b)).ToList();
    }

    private List<(int[], int)> FindAllCombinationsThatAffectIndex(List<List<int>> possibleButtonPresses, int joltageIndex, int[] startingJoltage, int[] goalJoltage, int numberOfPressesSoFar)
    {
        int startingValue = startingJoltage[joltageIndex];
        int valueToReach = goalJoltage[joltageIndex];
        var filteredButtons = possibleButtonPresses.Where(bw => bw.Contains(joltageIndex)).ToList();

        if (filteredButtons.Count == 0)
        {
            return new List<(int[], int)>();
        }

        int needed = valueToReach - startingValue;
        if (needed <= 0)
        {
            return new List<(int[], int)>();
        }

        List<(int[], int)> result = new List<(int[], int)>();
        FindCombinationsRecursive(filteredButtons, needed, 0, startingJoltage.ToArray(), numberOfPressesSoFar, result, goalJoltage);

        return result;
    }

    private void FindCombinationsRecursive(List<List<int>> filteredButtonSequences, int pressesNeeded, int buttonIndex, int[] currentJoltage, int numberOfPressesSoFar, List<(int[], int)> result, int[] goalJoltage)
    {
        if (pressesNeeded == 0)
        {
            result.Add((currentJoltage.ToArray(), numberOfPressesSoFar));
            return;
        }

        if (buttonIndex >= filteredButtonSequences.Count)
        {
            return;
        }

        var currentButtons = filteredButtonSequences[buttonIndex];

        // Try pressing this button 0 to pressesNeeded times
        for (int pressCount = 0; pressCount <= pressesNeeded; pressCount++)
        {
            foreach (var index in currentButtons)
            {
                if(currentJoltage[index] + pressCount > goalJoltage[index])
                {
                    return;
                }
            }

            int[] newJoltage = currentJoltage.ToArray();

            // Apply the button presses
            foreach (var index in currentButtons)
            {
                newJoltage[index]+=pressCount;
            }


            // Calculate how many presses we still need for the target index
            int remainingPresses = pressesNeeded - pressCount;
            //
            // if(TooMuchPower(newJoltage, goalJoltage))
            // {
            //     return;
            // }

            // Recurse with the next button
            FindCombinationsRecursive(filteredButtonSequences, remainingPresses, buttonIndex + 1, newJoltage, numberOfPressesSoFar + pressCount, result, goalJoltage);
        }
    }

    private long CalculatePresses(int[] joltages, List<List<int>> itemButtonWirings, HashSet<string> previousMemo)
    {
        long numPresses = 0;

        while (true)
        {
            numPresses++;
            HashSet<string> newMemos = new HashSet<string>();
            if(previousMemo.Count == 0)
            {
                throw new Exception("No more states to explore");
            }
            foreach (var buttonsToPress in itemButtonWirings)
            {
                var singleButtonMemos = new HashSet<string>();
                foreach (var currentJoltage in previousMemo)
                {
                    int[] afterPressing = PressButton(currentJoltage, buttonsToPress);

                    if (TooMuchPower(afterPressing, joltages))
                    {
                        continue;
                    }

                    singleButtonMemos.Add(ToKey(afterPressing));
                    if (afterPressing[0] < joltages[0]) continue; // quick check
                    if (afterPressing.SequenceEqual(joltages))
                    {
                        return numPresses;
                    }
                }

                newMemos.UnionWith(singleButtonMemos);
            }

            previousMemo = newMemos;
        }
    }

    private bool TooMuchPower(int[] newJoltage, int[] goalJoaltage)
    {
        for (int i = 0; i < newJoltage.Length; i++)
        {
            if (newJoltage[i] > goalJoaltage[i])
                return true;
        }
        return false;
    }

    private static int[] PressButton(string currentJoltage, List<int> itemButtonWiring)
    {
        int[] newJoltage = currentJoltage.Split(",").Select(int.Parse).ToArray();
        foreach (var index in itemButtonWiring)
        {
            newJoltage[index]++;
        }

        return newJoltage;
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
