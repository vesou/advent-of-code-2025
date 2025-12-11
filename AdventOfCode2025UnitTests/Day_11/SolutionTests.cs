using FluentAssertions;

namespace AdventOfCode2025UnitTests.Day_11;

public class SolutionTests
{
    [Fact]
    public void Test_Solve1()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_11.Solution1();
        string[] input =
        [
            "aaa: you hhh",
            "you: bbb ccc",
            "bbb: ddd eee",
            "ccc: ddd eee fff",
            "ddd: ggg",
            "eee: out",
            "fff: out",
            "ggg: out",
            "hhh: ccc fff iii",
            "iii: out"
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(5);
    }

    [Fact]
    public void Real_Solve_01()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_11.Solution1();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(658);
    }

    [Fact]
    public void Test_Solve2()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_11.Solution2();
        string[] input =
        [
            "svr: aaa bbb",
            "aaa: fft",
            "fft: ccc",
            "bbb: tty",
            "tty: ccc",
            "ccc: ddd eee",
            "ddd: hub",
            "hub: fff",
            "eee: dac",
            "dac: fff",
            "fff: ggg hhh",
            "ggg: out",
            "hhh: out"
        ];

        // Act
        long result = solution.Solve(input);

        // Assert
        result.Should().Be(2);
    }

    [Fact]
    public void Real_Solve_02()
    {
        // Arrange
        var solution = new AdventOfCode2025.Day_11.Solution2();

        // Act
        long result = solution.Start();

        // Assert
        result.Should().Be(0);
    }
}
