using System.Collections;

namespace AdventOfCode2025.Day_12;

public class Solution1
{
    private static int patternArea = 9;
    private static int patternWidth = 3;
    private static int patternHeight = 3;

    public string[] GetInput()
    {
        return File.ReadAllLines("Day_12/Input.txt");
    }

    public int Start()
    {
        var input = GetInput();
        return Solve(input);
    }

    public int Solve(string[] input)
    {
        var result = 0;
        var patterns = ParsePatterns(input);
        var grids = ParseGrids(input);
        foreach (var (width, height, patternRequirementCount) in grids)
        {
            var totalRequired = patternRequirementCount.Sum();
            var gridArea = width * height;

            if (gridArea > totalRequired * patternArea)
            {
                // we have more space than needed
                result++;
                continue;
            }

            // More complex fitting logic
            var grid = new BitArray[width];
            for (var c = 0; c < width; c++) grid[c] = new BitArray(height);
            result += FitPatternsToGrid(grid, patterns, patternRequirementCount) ? 1 : 0;
        }

        return result;
    }


    private bool FitPatternsToGrid(BitArray[] grid, List<PatternGroup> patterns, int[] patternRequirementCount)
    {
        if (patternRequirementCount.All(c => c == 0))
            return true;

        var gridWidth = grid.Length;
        var gridHeight = grid[0].Length;

        // Find first pattern that still needs to be placed
        var patternIndex = -1;
        for (var i = 0; i < patternRequirementCount.Length; i++)
        {
            if (patternRequirementCount[i] > 0)
            {
                patternIndex = i;
                break;
            }
        }

        if (patternIndex == -1)
            return true;

        var patternGroup = patterns[patternIndex];

        // Try placing this pattern at every position in the grid
        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                // Try all orientations of this pattern
                foreach (var shape in patternGroup.ShapeOrientations)
                {
                    if (!shape.Fits(grid, x, y))
                        continue;

                    // Place the pattern
                    shape.Place(grid, x, y);
                    patternRequirementCount[patternIndex]--;

                    // Recursively try to place remaining patterns
                    if (FitPatternsToGrid(grid, patterns, patternRequirementCount))
                        return true;

                    // Backtrack: remove the pattern
                    RemovePattern(grid, shape, x, y);
                    patternRequirementCount[patternIndex]++;
                }
            }
        }

        return false;
    }

    private void RemovePattern(BitArray[] grid, Shape shape, int startX, int startY)
    {
        for (var r = 0; r < patternHeight; r++)
        {
            for (var c = 0; c < patternWidth; c++)
            {
                var gridX = startX + c;
                var gridY = startY + r;
                var patternIndex = r * patternWidth + c;
                if (shape.Pattern[patternIndex])
                    grid[gridX][gridY] = false;
            }
        }
    }

    #region Patterns

    public class PatternGroup
    {
        public int Index { get; set; }
        public List<Shape> ShapeOrientations { get; set; }
    }

    public class Shape
    {
        public BitArray Pattern { get; set; } = new(9);

        public bool Fits(BitArray[] grid, int startX, int startY)
        {
            var gridWidth = grid.Length;
            var gridHeight = grid[0].Length;

            // check if pattern doesn't overlap with filled cells
            for (var r = 0; r < patternHeight; r++)
            for (var c = 0; c < patternWidth; c++)
            {
                var gridX = startX + c;
                var gridY = startY + r;
                if (gridX >= gridWidth || gridY >= gridHeight)
                    return false; // out of bounds

                var patternIndex = r * patternWidth + c;
                if (grid[gridX][gridY] && Pattern[patternIndex])
                    return false;
            }

            return true;
        }

        public void Place(BitArray[] grid, int startX, int startY)
        {
            for (var r = 0; r < patternHeight; r++)
            for (var c = 0; c < patternWidth; c++)
            {
                var gridX = startX + c;
                var gridY = startY + r;
                var patternIndex = r * patternWidth + c;
                if (Pattern[patternIndex]) grid[gridX][gridY] = true;
            }
        }
    }

    #endregion

    #region parsing

    private List<(int width, int height, int[] patternRequirementCount)> ParseGrids(string[] input)
    {
        var grids = new List<(int width, int height, int[] patternRequirementCount)>();
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line) || line.EndsWith(":"))
                continue;

            var parts = line.Split(':');
            if (parts.Length != 2)
                continue;
            var sizePart = parts[0].Trim();
            var requirementsPart = parts[1].Trim();

            var sizeTokens = sizePart.Split('x');
            var width = int.Parse(sizeTokens[0]);
            var height = int.Parse(sizeTokens[1]);

            var requirementTokens = requirementsPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var patternRequirementCount = requirementTokens.Select(int.Parse).ToArray();

            grids.Add((width, height, patternRequirementCount));
        }

        return grids;
    }

    private List<PatternGroup> ParsePatterns(string[] input)
    {
        List<PatternGroup> patterns = [];
        var index = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var patternGroup = new PatternGroup();
            if (string.IsNullOrWhiteSpace(input[i]))
                continue;

            if (!input[i].EndsWith(":")) continue;

            var patternLines = new List<string>();
            i++;
            while (i < input.Length && !string.IsNullOrWhiteSpace(input[i]))
            {
                patternLines.Add(input[i].Trim());
                i++;
            }

            var rows = patternLines.Count;
            var cols = patternLines[0].Length;

            var shape = new BitArray[rows];
            for (var col = 0; col < cols; col++) shape[col] = new BitArray(rows);

            for (var r = 0; r < rows; r++)
            for (var c = 0; c < cols; c++)
                shape[c][r] = patternLines[r][c] == '#';

            patternGroup.Index = index++;
            patternGroup.ShapeOrientations = RotateOrFlipShape(shape);
            patterns.Add(patternGroup);
        }

        return patterns;
    }

    private List<Shape> RotateOrFlipShape(BitArray[] shape)
    {
        var orientations = new List<Shape>();

        orientations.AddRange(GetAllRotations(shape));
        shape = FlipShapeHorizontally(shape);
        orientations.AddRange(GetAllRotations(shape));
        shape = FlipShapeVertically(shape);
        orientations.AddRange(GetAllRotations(shape));
        orientations = RemoveDuplicateShapes(orientations);

        return orientations;
    }

    // remove duplicate shapes that have the same Pattern meaning the bitArray inside has the same bits set
    private List<Shape> RemoveDuplicateShapes(List<Shape> orientations)
    {
        var uniqueShapes = new List<Shape>();
        var seenPatterns = new HashSet<string>();
        foreach (var shape in orientations)
        {
            var patternKey = string.Join(",", shape.Pattern.Cast<bool>().Select(b => b ? "1" : "0"));
            if (!seenPatterns.Contains(patternKey))
            {
                seenPatterns.Add(patternKey);
                uniqueShapes.Add(shape);
            }
        }

        return uniqueShapes;
    }

    private BitArray[] FlipShapeVertically(BitArray[] shape)
    {
        var rows = shape[0].Length;
        var cols = shape.Length;
        var flipped = new BitArray[cols];
        for (var c = 0; c < cols; c++)
        {
            flipped[c] = new BitArray(rows);
            for (var r = 0; r < rows; r++) flipped[c][r] = shape[c][rows - r - 1];
        }

        return flipped;
    }

    private BitArray[] FlipShapeHorizontally(BitArray[] shape)
    {
        var rows = shape[0].Length;
        var cols = shape.Length;
        var flipped = new BitArray[cols];
        for (var c = 0; c < cols; c++)
        {
            flipped[c] = new BitArray(rows);
            for (var r = 0; r < rows; r++) flipped[c][r] = shape[cols - c - 1][r];
        }

        return flipped;
    }

    private IEnumerable<Shape> GetAllRotations(BitArray[] shape)
    {
        var orientations = new List<Shape>();
        var currentShape = shape;
        for (var i = 0; i < 5; i++)
        {
            var newShape = new Shape();
            newShape.Pattern = FlattenShape(currentShape);
            orientations.Add(newShape);
            currentShape = RotateShape90Degrees(currentShape);
        }

        return orientations;
    }

    private BitArray[] RotateShape90Degrees(BitArray[] currentShape)
    {
        var rows = currentShape[0].Length;
        var cols = currentShape.Length;
        var rotated = new BitArray[rows];
        for (var r = 0; r < rows; r++)
        {
            rotated[r] = new BitArray(cols);
            for (var c = 0; c < cols; c++) rotated[r][c] = currentShape[cols - c - 1][r];
        }

        return rotated;
    }

    private BitArray FlattenShape(BitArray[] currentShape)
    {
        var rows = currentShape[0].Length;
        var cols = currentShape.Length;
        var flat = new BitArray(rows * cols);
        for (var r = 0; r < rows; r++)
        for (var c = 0; c < cols; c++)
            flat[r * cols + c] = currentShape[c][r];

        return flat;
    }

    #endregion
}
