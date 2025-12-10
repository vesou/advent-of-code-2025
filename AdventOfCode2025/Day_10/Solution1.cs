namespace AdventOfCode2025.Day_10;

public class Solution1
{
    public string[] GetInput()
    {
        return File.ReadAllLines("Day_10/Input.txt");
    }

    public int Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public int Solve(string[] input)
    {
        var result = 0;
        var data = GetData(input);
        foreach (var item in data)
        {
            Dictionary<string, int> memo = new Dictionary<string, int>();
            result += CalculatePresses(item.LightDiagram, item.ButtonWirings, memo);
        }

        return result;
    }

    private int CalculatePresses(byte[] itemLightDiagram, List<List<int>> itemButtonWirings, Dictionary<string, int> memo)
    {
        int numPresses = 0;

        memo[new string('.', itemLightDiagram.Length)] = 0;

        while (true)
        {
            numPresses++;
            foreach (var buttonsToPress in itemButtonWirings)
            {
                var newMemos = new Dictionary<string, int>();
                foreach (var (key, value) in memo.Where(kv => kv.Value == numPresses - 1))
                {
                    byte[] afterPressing = PressButton(FromKey(key), buttonsToPress);
                    if (memo.ContainsKey(ToKey(afterPressing)))
                    {
                        continue;
                    }
                    newMemos[ToKey(afterPressing)] = numPresses;
                    if (IsTheSame(afterPressing, itemLightDiagram))
                    {
                        return numPresses;
                    }
                }

                foreach (var (key, value) in newMemos)
                {
                    memo[key] = value;
                }
            }
        }
    }

    private bool IsTheSame(byte[] afterPressing, byte[] itemLightDiagram)
    {
        for (int i = 0; i < afterPressing.Length; i++)
        {
            if (afterPressing[i] != itemLightDiagram[i])
                return false;
        }
        return true;
    }

    private static byte[] PressButton(byte[] itemLightDiagram, List<int> itemButtonWiring)
    {
        var newLightDiagram = (byte[])itemLightDiagram.Clone();
        foreach (var index in itemButtonWiring)
        {
            newLightDiagram[index] ^= 1; // Toggle the light
        }
        return newLightDiagram;
    }

    private static string ToKey(byte[] itemLightDiagram)
    {
        string result = "";
        foreach (var b in itemLightDiagram)
        {
            if(b == 0)
                result += ".";
            else
                result += "#";
        }

        return result;
    }

    private static byte[] FromKey(string itemLightDiagram)
    {
        byte[] result = new byte[itemLightDiagram.Length];
        for (int i = 0; i < itemLightDiagram.Length; i++)
        {
            if(itemLightDiagram[i] == '.')
                result[i] = 0;
            else
                result[i] = 1;
        }

        return result;
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
