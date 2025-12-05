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

        var numFresh = range.Split('\n')
            .Select<string, (long start, long end)>(r =>
            {
                var s = r.Split('-');
                var start = long.Parse(s.First());
                var end = long.Parse(s.Last());

                return (start, end);
            })
            .OrderBy(r => r.start)
            .Aggregate(new Stack<(long start, long end)>(), SimplifyRanges)
            .Select(n =>
            {
                GD.PrintS(n.start, n.end);
                
                return n;
            })
            .Aggregate(0L, (n, r) => n + LongRange.From(r.start, r.end).Length());
        
        GD.Print(numFresh);
    }

    private Stack<(long start, long end)> SimplifyRanges(Stack<(long start, long end)> ranges, (long start, long end) current)
    {
        if (ranges.TryPop(out var last))
        {
            if (current.start <= last.end)
            {
                ranges.Push((last.start, current.end));
            }
            else
            {
                ranges.Push(last);
                ranges.Push(current);
            }
        }
        else
        {
            ranges.Push(current);
        }
    
        return new Stack<(long, long)>(ranges.OrderBy(s => s.start));
    }
}
