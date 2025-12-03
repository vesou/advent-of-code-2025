
using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_01;

public class SolutionTests
{
    [Fact]
    public void Test_Solve()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_01.Solution1();
        string[] input = new string[]
        {
            "L68",
            "L30",
            "R48",
            "L5",
            "R60",
            "L55",
            "L1",
            "L99",
            "R14",
            "L82"
        };

        // Act
        int result = solution.Solve(input);

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_01.Solution1();
        // string[] input = File.ReadAllLines("AdventOfCode2025/Day_01/Input_01_1.txt");

        // Act
        int result = solution.Start();

        // Assert
        result.Should().Be(1076);
    }

    [Fact]
    public void Test_SolveB()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_01.Solution2();
        string[] input = new string[]
        {
            "L68",
            "L30",
            "R48",
            "L5",
            "R60",
            "L55",
            "L1",
            "L99",
            "R14",
            "L82"
        };

        // Act
        int result = solution.Solve(input);

        // Assert
        result.Should().Be(6);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_01.Solution2();
        // string[] input = File.ReadAllLines("AdventOfCode2025/Day_01/Input_01_1.txt");

        // Act
        int result = solution.Start();

        // Assert
        result.Should().Be(6379);
    }

    [Fact]
    public void Test_SolveC()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_01.Solution2();
        string[] input = new string[]
        {
            "L50",
            "L100",
            "L200",
            "R100",
            "R200",
        };

        // Act
        int result = solution.Solve(input);

        // Assert
        result.Should().Be(7);
    }
}
