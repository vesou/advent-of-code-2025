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
            result += CalculatePresses(item.Joltages.ToArray(), item.ButtonWirings, memo);
            result += CalculatePresses2(item.Joltages.ToArray(), item.ButtonWirings);
        }

        return result;
    }

    private long CalculatePresses2(int[] joltages, List<List<int>> possibleButtonPresses)
    {
        long numPresses = 0;
        int[] previousJoltage = new int[joltages.Length];
        for (int joltageIndex = 0; joltageIndex < joltages.Length; joltageIndex++)
        {
            List<(List<int>, int[])> filteredButtonWirings = FindAllCombinationsThatAffectIndex(possibleButtonPresses, joltageIndex, joltages[joltageIndex], previousJoltage);
        }

        while (true)
        {
            numPresses++;
            HashSet<string> newMemos = new HashSet<string>();
            if(previousMemo.Count == 0)
            {
                throw new Exception("No more states to explore");
            }
            foreach (var buttonsToPress in possibleButtonPresses)
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

    private List<(List<int>, int[])> FindAllCombinationsThatAffectIndex(List<List<int>> possibleButtonPresses, int joltageIndex, int valueToReach, int[] startingJoltage)
    {
        var result = new List<(List<int>, int[])>();
        int startingValue = startingJoltage[joltageIndex];
        var filteredButtons = possibleButtonPresses.Where(bw => bw.Contains(joltageIndex)).ToList();


        return result;
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

    private bool TooMuchPower(int[] afterPressing, int[] joltages)
    {
        for (int i = 0; i < afterPressing.Length; i++)
        {
            if (afterPressing[i] > joltages[i])
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
