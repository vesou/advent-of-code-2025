using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_07;

public class SolutionTests
{
    [Fact]
    public void Test_Solve1()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_07.Solution1();
        string[] input =
        [
".......S.......",
"...............",
".......^.......",
"...............",
"......^.^......",
"...............",
".....^.^.^.....",
"...............",
"....^.^...^....",
"...............",
"...^.^...^.^...",
"...............",
"..^...^.....^..",
"...............",
".^.^.^.^.^...^.",
"...............",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(21);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_07.Solution1();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(1573);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_07.Solution2();
        string[] input =
        [
            ".......S.......",
            "...............",
            ".......^.......",
            "...............",
            "......^.^......",
            "...............",
            ".....^.^.^.....",
            "...............",
            "....^.^...^....",
            "...............",
            "...^.^...^.^...",
            "...............",
            "..^...^.....^..",
            "...............",
            ".^.^.^.^.^...^.",
            "...............",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(40);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_07.Solution2();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(15093663987272);
    }
}
