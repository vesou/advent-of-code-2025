using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_05;

public class SolutionTests
{
    [Fact]
    public void Test_Solve1()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_05.Solution1();
        string[] input =
        [
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_05.Solution1();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(789);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_05.Solution2();
        string[] input =
        [
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(14);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_05.Solution2();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(343329651880509);
    }
}
