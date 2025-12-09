using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_09;

public class SolutionTests
{
    [Fact]
    public void Test_Solve1()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_09.Solution1();
        string[] input =
        [
            "7,1",
            "11,1",
            "11,7",
            "9,7",
            "9,5",
            "2,5",
            "2,3",
            "7,3"
        ];

        // Act
        var result = solution.Solve(input);

        // Assert
        result.Should().Be(50);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_09.Solution1();

        // Act
        var result = solution.Start();

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_09.Solution2();
        string[] input =
        [
            "7,1",
            "11,1",
            "11,7",
            "9,7",
            "9,5",
            "2,5",
            "2,3",
            "7,3"
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(24);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_09.Solution2();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(0);
    }
}
