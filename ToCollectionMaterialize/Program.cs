using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ToCollectionMaterialize
{
    [SimpleJob(RuntimeMoniker.Net70)]
    [MemoryDiagnoser(false)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [MarkdownExporter, CsvExporter]
    public class Bench
    {
        [Params(100000)] public int N;
        IEnumerable<int> collection = null;

        int[] array = Array.Empty<int>();
        ImmutableArray<int> imarray = ImmutableArray.Create<int>();
        List<int> list = new List<int>();
        HashSet<int> hashSet = new HashSet<int>();
        Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
        public Bench()
        {
            const int MIN = 1;
            const int MAX = 10;
            Random random = new Random();
            collection = Enumerable.Repeat(0, N).Select(i => random.Next(MIN, MAX));
        }

        #region Collection To
        [Benchmark]
        public void ToArray()
        {
            array = collection.ToArray();
        }
        
        [Benchmark]
        public void ToImmutableArray()
        {
            imarray = collection.ToImmutableArray();
        }

        [Benchmark]
        public void ToList()
        {
            list = collection.ToList();
        }

        [Benchmark]
        public void ToHashSet()
        {
            hashSet = collection.ToHashSet();
        }

        [Benchmark]
        public void ToDictionary()
        {
            dictionary = collection.ToDictionary(x => x, x => false);
        }
        #endregion
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