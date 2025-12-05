using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using Godot;


public partial class Day3 : Node
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
        var banks = Content.Split('\n')
            .Where(line => line.Length > 0)
            .Select(FindMaxJoltage(2))
            .Sum();

        GD.Print(banks);
    }
    
    private void PuzzleTwo()
    {
        var banks = Content.Split('\n')
            .Where(line => line.Length > 0)
            .Select(FindMaxJoltage(12))
            .Sum();

        GD.Print(banks);
    }
    
    private Func<string, long> FindMaxJoltage(int capacity)
    {
        return (bank) =>
        {
            var batteries = new List<char>(capacity);
            var n = bank.Length;
            var startIndex = 0;
        
            for (var i = 0; i < batteries.Capacity; i++)
            {
                var remainingDigits = batteries.Capacity - i - 1;
            
                var searchEnd = n - remainingDigits;
            
                var maxDigit = bank[startIndex];
                var maxIndex = startIndex;

                for (var j = startIndex; j < searchEnd; j++)
                {
                    if (bank[j] <= maxDigit) continue;
                
                    maxDigit = bank[j];
                    maxIndex = j;
                }

                batteries.Add(maxDigit);
                startIndex = maxIndex + 1;
            }
        

            return batteries
                .Select(c => long.Parse(c.ToString()))
                .Aggregate(0L, (acc, digit) => acc * 10 + digit);
        };
    }
}
