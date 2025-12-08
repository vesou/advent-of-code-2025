using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_08;

public class SolutionTests
{
    [Fact]
    public void Test_Solve1()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_08.Solution1();
        string[] input =
        [
            "162,817,812",
            "57,618,57",
            "906,360,560",
            "592,479,940",
            "352,342,300",
            "466,668,158",
            "542,29,236",
            "431,825,988",
            "739,650,466",
            "52,470,668",
            "216,146,977",
            "819,987,18",
            "117,168,530",
            "805,96,715",
            "346,949,466",
            "970,615,88",
            "941,993,340",
            "862,61,35",
            "984,92,344",
            "425,690,689",
        ];

        // Act
        long result = solution.Solve(input, 10, 3);

        // Assert
        result.Should().Be(40);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_08.Solution1();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(117000);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_08.Solution2();
        string[] input =
        [
            "162,817,812",
            "57,618,57",
            "906,360,560",
            "592,479,940",
            "352,342,300",
            "466,668,158",
            "542,29,236",
            "431,825,988",
            "739,650,466",
            "52,470,668",
            "216,146,977",
            "819,987,18",
            "117,168,530",
            "805,96,715",
            "346,949,466",
            "970,615,88",
            "941,993,340",
            "862,61,35",
            "984,92,344",
            "425,690,689",
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(25272);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_08.Solution2();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(8368033065);
    }
}
