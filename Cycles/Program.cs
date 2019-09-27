using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace Cycles
{
    [CoreJob]
    public class Bench
    {
        [Benchmark]
        public bool ListForEach()
        {
            bool result = default;
            var list = new List<object>();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(new Random(DateTime.Now.Millisecond));
            }

            var listend = new List<object>();
            list.ForEach(item =>
            {
                if (item.GetType() == typeof(object))
                    result = true;

                listend.Add(item);

                result = false;
            });

            return result;
        }

        [Benchmark]
        public bool FOREACH()
        {
            bool result = default;
            var list = new List<object>();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(new Random(DateTime.Now.Millisecond));
            }

            var listend = new List<object>();
            foreach (var item in list)
            {
                if (item.GetType() == typeof(object))
                    result = true;

                listend.Add(item);

                result = false;
            }

            return result;
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
