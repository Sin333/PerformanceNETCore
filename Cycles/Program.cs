using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cycles
{
    [CoreJob]
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, RPlotExporter]
    public class Bench
    {
        private List<int> _list;
        private int[] _array;

        [Params(100000, 10000000)] public int N;

        [GlobalSetup]
        public void Setup()
        {
            const int MIN = 1;
            const int MAX = 10;
            Random random = new Random();
            _list = Enumerable.Repeat(0, N).Select(i => random.Next(MIN, MAX)).ToList();
            _array = _list.ToArray();
        }

        [Benchmark]
        public int ForList()
        {
            int total = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                total += _list[i];
            }

            return total;
        }
        
        [Benchmark]
        public int ForListFromEnd()
        {
            int total = 0;
            for (int i = _list.Count-1; i > 0; i--)
            {
                total += _list[i];
            }

            return total;
        }

        [Benchmark]
        public int ForeachList()
        {
            int total = 0;
            foreach (int i in _list)
            {
                total += i;
            }

            return total;
        }

        [Benchmark]
        public int ForeachArray()
        {
            int total = 0;
            foreach (int i in _array)
            {
                total += i;
            }

            return total;
        }

        [Benchmark]
        public int ForArray()
        {
            int total = 0;
            for (int i = 0; i < _array.Length; i++)
            {
                total += _array[i];
            }

            return total;
        }
        
        [Benchmark]
        public int ForArrayFromEnd()
        {
            int total = 0;
            for (int i = _array.Length-1; i > 0; i--)
            {
                total += _array[i];
            }

            return total;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Bench>();
            Console.ReadKey();
        }
    }
}
