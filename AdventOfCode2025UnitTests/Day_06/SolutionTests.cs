using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_06;

public class SolutionTests
{
    [Fact]
    public void Test_Solve1()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_06.Solution1();
        string[] input =
        [
            "123 328  51 64 ",
            " 45 64  387 23 ",
            "  6 98  215 314",
            "*   +   *   +  ",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(4277556);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_06.Solution1();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(5227286044585);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_06.Solution2();
        string[] input =
        [
            "123 328  51 64 ",
            " 45 64  387 23 ",
            "  6 98  215 314",
            "*   +   *   +  ",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(3263827);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_06.Solution2();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(10227753257799);
    }
}
