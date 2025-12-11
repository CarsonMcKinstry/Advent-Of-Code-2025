using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AdventOfCode2025.Day2;

public partial class Day5 : Node
{
    [Export(PropertyHint.File, "*.txt")] 
    public string Input { get; private set; }
    
    
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
        var split = Content.Split("\n\n").ToArray();
        var range = split[0];
        var ids = split[1];

        var fullRange = range.Split('\n')
            .Select(LongRange.FromInclusive)
            .ToHashSet();

        var fresh = ids.Split('\n')
            .Where(s => s.Length > 0)
            .Select(long.Parse)
            .Count(value => fullRange.Any(r => r.ContainsInclusive(value)));
        
        GD.Print(fresh);
    }

    private void PuzzleTwo()
    {
        var split = Content.Split("\n\n").ToArray();
        var range = split[0];

        var ranges = range.Split("\n")
            .Select<string, (long start, long end)>(r =>
            {
                var s = r.Split('-');
                var start = long.Parse(s[0]);
                var end = long.Parse(s[1]);
                return (start, end);
            })
            .OrderBy(r => r.start)
            .ToList();

        var merged = new List<(long start, long end)>();

        foreach (var current in ranges)
        {
            if (merged.Count == 0)
            {
                merged.Add(current);
            }
            else
            {
                var last = merged[^1];

                if (current.start <= last.end + 1)
                {
                    merged[^1] = (last.start, Math.Max(last.end, current.end));
                } else
                {
                    merged.Add(current);
                }
            }
        }

        var numFresh = merged.Sum(r => r.end - r.start + 1);
        
        GD.Print(numFresh);
    }
    
}
