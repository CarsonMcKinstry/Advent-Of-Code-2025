using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2025.Day2;

public class LongRange(long start, long end) : IEnumerable<long>
{
    
    private long _start = start;
    private long _end = end;

    public static LongRange From(long start, long end)
    {
        return new LongRange(start, end);
    }

    public static LongRange From(string range)
    {
        var split = range.Split('-').Select(long.Parse).ToArray();
        return new LongRange(split[0], split[1]);
    }
    
    public static LongRange FromInclusive(string range)
    {
        var split = range.Split('-').Select(long.Parse).ToArray();
        return new LongRange(split[0], split[1] + 1);
    }

    public bool ContainsInclusive (long value)
    {
        return value >= _start && value <= _end;
    }

    public long Length()
    {
        return _end - _start;
    }
    
    public IEnumerator<long> GetEnumerator()
    {
        return new LongRangeEnumerator(_start, _end);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class LongRangeEnumerator(long start, long end) : IEnumerator<long>
    {
        private long _start = start;
        private long _end = end;

        public void Dispose()
        {
            // TODO release managed resources here
        }

        public bool MoveNext()
        {
            Current++;
            return Current <= _end;
        }

        public void Reset()
        {
            Current = _start;
        }

        public long Current { get; private set; } = start - 1;

        object IEnumerator.Current => Current;
    }
}