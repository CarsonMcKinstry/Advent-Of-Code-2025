using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025.Day2;
using Range = System.Range;

public partial class Day2 : Node
{
    [Export(PropertyHint.File, "*.txt")] public String Input { get; private set; }
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
        var total = GetAllIds().Where(id =>
        {
            var idString = id.ToString();

            // if the id has an odd length, we don't include it
            if (idString.Length % 2 > 0) return false;
            var splitPoint = idString.Length / 2;
            var firstHalf = idString.Substring(0, splitPoint);
            var secondHalf = idString.Substring(splitPoint);
            return firstHalf == secondHalf;
        }).Sum();
        GD.Print(total);
    }

    private void PuzzleTwo()
    {
        var total = GetAllIds().Where(id =>
            {
                
                var idString = id.ToString();
                
                // An ID is invalid if it is made ONLY of some sequence of digits repeated  
                // _at least_ two times. Example: 12341234, 123123123, 1212121212, 1111111
                
                // https://www.baeldung.com/java-repeated-substring

                return (idString + idString).IndexOf(idString, 1, StringComparison.Ordinal) != idString.Length;
                
            })
            .Sum();
        GD.Print(total);
    }

    private IEnumerable<long> GetAllIds()
    {
        return Content.Split(",").Where(range => range.Length > 0).SelectMany(BuildRange);
    }

    private IEnumerable<long> BuildRange(string range)
    {
        var split = range.Split("-");
        var min = long.Parse(split[0]);
        var max = long.Parse(split[1]);
        var r = LongRange.From(min, max);
        return r;
    }
}