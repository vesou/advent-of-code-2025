namespace AdventOfCode2025.Day_12;

public class Solution1
{

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
            var gridArea = width * height;
            var totalPatternCount = patternRequirementCount.Sum();

            // Optimization: if grid has more space than total patterns * 9 (bounding box),
            // we're guaranteed to fit them regardless of arrangement (only valid at start with empty grid)
            if (gridArea > totalPatternCount * 9)
            {
                result++;
                continue;
            }

            // Calculate total filled cells needed based on actual pattern sizes
            var totalFilledCellsNeeded = 0;
            for (var i = 0; i < patternRequirementCount.Length; i++)
            {
                if (patternRequirementCount[i] > 0 && i < patterns.Count)
                {
                    var filledCellsInPattern = patterns[i].ShapeOrientations[0].FilledCells.Count;
                    totalFilledCellsNeeded += patternRequirementCount[i] * filledCellsInPattern;
                }
            }

            // If we don't have enough space for the filled cells, skip immediately
            if (gridArea < totalFilledCellsNeeded)
            {
                continue;
            }

            // Use backtracking to verify the fit
            var filledCells = new HashSet<(int x, int y)>();
            result += FitPatternsToGrid(filledCells, width, height, patterns, patternRequirementCount) ? 1 : 0;
        }

        return result;
    }


    private bool FitPatternsToGrid(HashSet<(int x, int y)> filledCells, int gridWidth, int gridHeight,
        List<PatternGroup> patterns, int[] patternRequirementCount)
    {
        if (patternRequirementCount.All(c => c == 0))
            return true;

        // Early pruning: check if remaining space can fit remaining patterns
        var remainingFilledCellsNeeded = 0;
        for (var i = 0; i < patternRequirementCount.Length; i++)
        {
            if (patternRequirementCount[i] > 0 && i < patterns.Count)
            {
                var filledCellsInPattern = patterns[i].ShapeOrientations[0].FilledCells.Count;
                remainingFilledCellsNeeded += patternRequirementCount[i] * filledCellsInPattern;
            }
        }

        var availableSpace = (gridWidth * gridHeight) - filledCells.Count;
        if (availableSpace < remainingFilledCellsNeeded)
            return false;

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

        // Optimization: only try positions adjacent to already placed patterns, or (0,0) if grid is empty
        var positionsToTry = new HashSet<(int x, int y)>();

        if (filledCells.Count == 0)
        {
            // Empty grid - only try top-left corner
            positionsToTry.Add((0, 0));
        }
        else
        {
            // Try positions adjacent to filled cells (including diagonals)
            foreach (var (fx, fy) in filledCells)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    for (var dx = -1; dx <= 1; dx++)
                    {
                        var nx = fx + dx;
                        var ny = fy + dy;
                        if (nx >= 0 && nx < gridWidth && ny >= 0 && ny < gridHeight && !filledCells.Contains((nx, ny)))
                        {
                            positionsToTry.Add((nx, ny));
                        }
                    }
                }
            }
        }

        // Try placing this pattern at candidate positions
        foreach (var (x, y) in positionsToTry)
        {
            // Try all orientations of this pattern
            foreach (var shape in patternGroup.ShapeOrientations)
            {
                if (!CanPlaceShape(filledCells, shape, x, y, gridWidth, gridHeight))
                    continue;

                // Place the pattern
                PlaceShape(filledCells, shape, x, y);
                patternRequirementCount[patternIndex]--;

                // Recursively try to place remaining patterns
                if (FitPatternsToGrid(filledCells, gridWidth, gridHeight, patterns, patternRequirementCount))
                    return true;

                // Backtrack: remove the pattern
                RemoveShape(filledCells, shape, x, y);
                patternRequirementCount[patternIndex]++;
            }
        }

        return false;
    }

    private bool CanPlaceShape(HashSet<(int x, int y)> filledCells, Shape shape, int startX, int startY,
        int gridWidth, int gridHeight)
    {
        foreach (var (dx, dy) in shape.FilledCells)
        {
            var x = startX + dx;
            var y = startY + dy;

            if (x >= gridWidth || y >= gridHeight)
                return false; // out of bounds

            if (filledCells.Contains((x, y)))
                return false; // overlap
        }
        return true;
    }

    private void PlaceShape(HashSet<(int x, int y)> filledCells, Shape shape, int startX, int startY)
    {
        foreach (var (dx, dy) in shape.FilledCells)
        {
            filledCells.Add((startX + dx, startY + dy));
        }
    }

    private void RemoveShape(HashSet<(int x, int y)> filledCells, Shape shape, int startX, int startY)
    {
        foreach (var (dx, dy) in shape.FilledCells)
        {
            filledCells.Remove((startX + dx, startY + dy));
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
        public List<(int dx, int dy)> FilledCells { get; set; } = new();
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

            var cells = new List<(int x, int y)>();
            for (var r = 0; r < rows; r++)
            for (var c = 0; c < cols; c++)
                if (patternLines[r][c] == '#')
                    cells.Add((c, r));

            patternGroup.Index = index++;
            patternGroup.ShapeOrientations = RotateOrFlipShape(cells);
            patterns.Add(patternGroup);
        }

        return patterns;
    }

    private List<Shape> RotateOrFlipShape(List<(int x, int y)> cells)
    {
        var orientations = new List<Shape>();

        orientations.AddRange(GetAllRotations(cells));
        cells = FlipShapeHorizontally(cells);
        orientations.AddRange(GetAllRotations(cells));
        cells = FlipShapeVertically(cells);
        orientations.AddRange(GetAllRotations(cells));
        orientations = RemoveDuplicateShapes(orientations);

        return orientations;
    }

    // remove duplicate shapes that have the same filled cells
    private List<Shape> RemoveDuplicateShapes(List<Shape> orientations)
    {
        var uniqueShapes = new List<Shape>();
        var seenPatterns = new HashSet<string>();
        foreach (var shape in orientations)
        {
            var sortedCells = shape.FilledCells.OrderBy(c => c.dx).ThenBy(c => c.dy).ToList();
            var patternKey = string.Join(",", sortedCells.Select(c => $"{c.dx}:{c.dy}"));
            if (!seenPatterns.Contains(patternKey))
            {
                seenPatterns.Add(patternKey);
                uniqueShapes.Add(shape);
            }
        }

        return uniqueShapes;
    }

    private List<(int x, int y)> FlipShapeVertically(List<(int x, int y)> cells)
    {
        var maxY = cells.Max(c => c.y);
        return cells.Select(c => (c.x, maxY - c.y)).ToList();
    }

    private List<(int x, int y)> FlipShapeHorizontally(List<(int x, int y)> cells)
    {
        var maxX = cells.Max(c => c.x);
        return cells.Select(c => (maxX - c.x, c.y)).ToList();
    }

    private IEnumerable<Shape> GetAllRotations(List<(int x, int y)> cells)
    {
        var orientations = new List<Shape>();
        var currentCells = cells;
        for (var i = 0; i < 4; i++)
        {
            var shape = new Shape();
            // Normalize so the minimum x and y are both 0
            var minX = currentCells.Min(c => c.x);
            var minY = currentCells.Min(c => c.y);
            shape.FilledCells = currentCells.Select(c => (c.x - minX, c.y - minY)).ToList();
            orientations.Add(shape);
            currentCells = RotateShape90Degrees(currentCells);
        }

        return orientations;
    }

    private List<(int x, int y)> RotateShape90Degrees(List<(int x, int y)> cells)
    {
        // Rotate 90 degrees clockwise: (x, y) -> (y, -x)
        return cells.Select(c => (c.y, -c.x)).ToList();
    }

    #endregion
}
