using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_04;

public class SolutionTests
{
    [Fact]
    public void Test_Solve1()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_04.Solution1();
        string[] input =
        [
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@.",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(13);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_04.Solution1();
        // string[] input = File.ReadAllLines("AdventOfCode2025/Day_01/Input_01_1.txt");

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(1560);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_04.Solution2();
        string[] input =
        [
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@.",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(43);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_04.Solution2();
        // string[] input = File.ReadAllLines("AdventOfCode2025/Day_01/Input_01_1.txt");

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(9609);
    }
}
