using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionMaterializeTo
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, RPlotExporter]
    public class Bench
    {
        [Params(100000)] public int N;
        IEnumerable<int> collection = null;

        int[] array = Array.Empty<int>();
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
        public void ArrayTo()
        {
            array = collection.ToArray();
        }

        [Benchmark]
        public void ListTo()
        {
            list = collection.ToList();
        }

        [Benchmark]
        public void HashSetTo()
        {
            hashSet = collection.ToHashSet();
        }

        [Benchmark]
        public void DictionaryTo()
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