using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Day4 : Node
{
    private static Vector2I[] _neighborsToCheck =
    [
        Vector2I.Right, Vector2I.Right + Vector2I.Down, Vector2I.Down,
        Vector2I.Down + Vector2I.Left, Vector2I.Left, Vector2I.Left + Vector2I.Up, Vector2I.Up,
        Vector2I.Up + Vector2I.Right,
    ];

    [Export(PropertyHint.File, "*.txt")] public string Input { get; private set; }
    private string Content { get; set; }

    public override void _Ready()
    {
        var file = FileAccess.Open(Input, FileAccess.ModeFlags.Read);
        Content = file.GetAsText();
        PuzzleOne();
        PuzzleTwo();
    }

    private void PuzzleOne()
    {
        var rawGrid = Content.Split('\n').Where(n => n.Length > 0)
            .ToArray();
        var grid = new HashSet<Vector2I>();
        var height = rawGrid.Length;
        var width = rawGrid[0].Length;
        for (var i = 0; i < height * width; i++)
        {
            var x = i % width;
            var y = i / width;
            var value = rawGrid[y][x];
            if (value == '@')
            {
                grid.Add(new Vector2I(x, y));
            }
        }

        var accessible = grid
            .Count(point => _neighborsToCheck.Select(item => item + point).Count(grid.Contains) < 4);
        
        GD.Print(accessible);
    }

    private void PuzzleTwo()
    {
    }

    private static int CantorId(int x, int y)
    {
        var a = x >= 0 ? 2 * x : x * -2 - 1;
        var b = y >= 0 ? 2 * y : y * -2 - 1;
        return a >= b ? a * a + a + b : b * b + a;
    }
}