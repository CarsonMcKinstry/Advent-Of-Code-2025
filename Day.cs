using System;
using Godot;

namespace AdventOfCode2025;

[GlobalClass]
public partial class Day : Node
{
    [Export(PropertyHint.File, "*.txt")] public String Input { get; private set; }
    protected string Content { get; set; }

    public override void _Ready()
    {
        var file = FileAccess.Open(Input, FileAccess.ModeFlags.Read);
        Content = file.GetAsText().TrimEnd();
        PuzzleOne();
        PuzzleTwo();
    }
    
    protected virtual void PuzzleOne()
    {
        GD.Print($"Puzzle One Unimplemented for {Name}");
    }

    protected virtual void PuzzleTwo()
    {
        GD.Print($"Puzzle Two Unimplemented for {Name}");
    }

}