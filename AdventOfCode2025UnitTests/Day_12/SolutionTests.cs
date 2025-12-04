using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_12;

public class SolutionTests
{
    [Fact]
    public void Test_Solve1()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_12.Solution1();
        string[] input =
        [
            "",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_12.Solution1();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_12.Solution2();
        string[] input =
        [
            "",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_12.Solution2();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(0);
    }
}
