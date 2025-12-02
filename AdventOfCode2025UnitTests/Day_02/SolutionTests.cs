
using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_02;

public class SolutionTests
{
    [Fact]
    public void Test_Solve()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_02.Solution1();
        string input =
            new string(
                "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124");

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(1227775554);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_02.Solution1();
        // string[] input = File.ReadAllLines("AdventOfCode2025/Day_01/Input_01_1.txt");

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(19219508902);
    }
}
