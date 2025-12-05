using System;

namespace AdventOfCode2025.Day1;

public enum Direction
{
    Left,
    Right
}

public static class DirectionExtensions
{
    public static Direction ToDirection(this string direction)
    {
        return direction switch
        {
            "L" => Direction.Left,
            "R" => Direction.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}