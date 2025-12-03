using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_03;

public class SolutionTests
{
    [Fact]
    public void Test_Solve()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_03.Solution1();
        var input =
            new string[]
            {
                "987654321111111",
                "811111111111119",
                "234234234234278",
                "818181911112111"
            };

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(357);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_03.Solution1();
        // string[] input = File.ReadAllLines("AdventOfCode2025/Day_01/Input_01_1.txt");

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(17445);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_03.Solution2();
        string[] input =
        [
            "987654321111111",
            "811111111111119",
            "234234234234278",
            "818181911112111"
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(3121910778619);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_03.Solution2();
        // string[] input = File.ReadAllLines("AdventOfCode2025/Day_01/Input_01_1.txt");

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(173229689350551L);
    }
}
