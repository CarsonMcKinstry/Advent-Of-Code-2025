using Godot;
using System;
using AdventOfCode2025.Day1;

public partial class Day1 : Node
{
    [Export(PropertyHint.File, "*.txt")] public String Input { get; private set; }

    [Export] public int StartAt = 50;

    [Export] public Control Dial { get; private set; }
    [Export] public RigidBody2D ZerothPosition { get; private set; }
    
    private string Content { get; set; }

    public override void _Ready()
    {
        var file = FileAccess.Open(Input, FileAccess.ModeFlags.Read);
        Content = file.GetAsText();

        PuzzleOne();
        PuzzleTwo();
        PuzzleTwoVisualization();
    }

    private void PuzzleOne()
    {
        var current = StartAt;
        var count = 0;

        foreach (var line in Content.Split('\n'))
        {
            if (line.Length < 2) continue;
            var (direction, distance) = ParseLine(line);
            current = Rotate(current, direction, distance);
            if (current == 0)
            {
                count++;
            }
        }

        GD.Print($"Part 1: {count}");
    }

    private void PuzzleTwo()
    {
        var current = StartAt;
        var count = 0;

        foreach (var line in Content.Split('\n'))
        {
            if (line.Length < 2) continue;

            var (direction, distance) = ParseLine(line);

            for (var i = 0; i < distance; i++)
            {
                current = Rotate(current, direction, 1);
                if (current == 0)
                {
                    count++;
                }
            }
        }
        
        GD.Print($"Part 2: {count}");
    }

    private void PuzzleTwoVisualization()
    {

        var startRotation = Dial.RotationDegrees + (3.6f * StartAt);
        
        Dial.SetRotationDegrees(startRotation);

        // ZerothPosition.BodyEntered += ReportBodyShapeEntered;
        //
        // var tween = CreateTween();
        // tween.TweenProperty(Dial, "rotation_degrees", startRotation + 3.6f * 200, 2.0);
    }

    // private void ReportBodyShapeEntered(Node body)
    // {
    //     GD.Print("Body entered.");
    // }

    private int Rotate(int current, Direction direction, int distance)
    {
        return direction switch
        {  
            Direction.Left => (current - distance % 100 + 100) % 100,
            Direction.Right => (current + distance) % 100,
            _ => current
        };
    }

    private (Direction direction, int distance) ParseLine(string line)
    {
        var d = line[..1];
        var n = line[1..];

        var direction = d.ToDirection();
        var distance = int.Parse(n);
        return (direction, distance);
    }
}