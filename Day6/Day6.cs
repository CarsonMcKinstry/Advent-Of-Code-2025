using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace AdventOfCode2025.Day6;

public partial class Day6 : Day
{
    protected override void PuzzleOne()
    {
        var problemSets = Content.Split('\n')
            .Select(row => row.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries))
            .ToList();
        
        var numRows = problemSets.Count;

        var total = 0L;
        
        for (var i = 0; i < problemSets[0].Length; i++)
        {
            var operation = problemSets[numRows - 1][i];

            var values = problemSets.Take(numRows - 1)
                .Select(row => long.Parse(row[i]))
                .ToArray();
            
            total += operation == "+" ? values.Sum() : values.Aggregate(1L, (acc, val) => acc * val);
        }
        
        GD.Print(total);
    }

    protected override void PuzzleTwo()
    {
        var problemSets = Content.Split('\n').ToList();
        
        var numColumns = problemSets.Max(row => row.Length);
        var numRows = problemSets.Count;

        var total = 0L;
        
        var currentColumn = new List<char[]>();
        
        // go column by column...
        for (var c = 0; c < numColumns; c++)
        {
            var column = problemSets.Select(row => c < row.Length ? row[c] : ' ').ToArray();

            if (column.All(value => value is ' '))
            {
            
                var value = Problem.From(currentColumn).Solve();
                
                total += value;
                
                currentColumn.Clear();
            }
            else
            {
                currentColumn.Add(column);
            }
        }
        
        total += Problem.From(currentColumn).Solve();
        
        GD.Print(total);
    }

    private class Problem()
    {
        private List<long> _values = [];
        private string _operation;
        
        public static Problem From(List<char[]> columns)
        {   
            var problem = new Problem();

            foreach (var columnValues in columns)
            {
                var span = columnValues.AsSpan().Trim();

                if (span.IsEmpty) continue; // Safety check

                var index = span.IndexOfAnyExceptInRange('0', '9');

                if (index >= 0)
                {
                    problem._values.Add(long.Parse(span[..index]));
                    problem._operation = span[index..].Trim().ToString();
                }
                else
                {
                    problem._values.Add(long.Parse(span));
                }
            }
            
            return  problem;
        }

        public long Solve()
        {
            return _operation == "+" ? _values.Sum() : _values.Aggregate(1L, (acc, value) => acc * value);
        }
    }
}