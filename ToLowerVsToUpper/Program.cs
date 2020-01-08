using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;

namespace ToLowerVsToUpper
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, RPlotExporter]
    public class Bench
    {
        public const string defaultString = "VXTDuob5YhummuDq1PPXOHE4PbrRjYfBjcHdFs8UcKSAHOCGievbUItWhU3ovCmRALgdZUG1CB0sQ4iMj8Z1ZfkML2owvfkOKxBCoFUAN4VLd4I8ietmlsS5PtdQEn6zEgy1uCVZXiXuubd0xM5ONVZBqDu6nOVq1GQloEjeRN8jXrj0MVUexB9aIECs7caKGddpuut3";

        [Benchmark]
        public bool ToLower()
        {
            return defaultString.ToLower() == defaultString.ToLower();
        }

        [Benchmark]
        public bool ToLowerInvariant()
        {
            return defaultString.ToLowerInvariant() == defaultString.ToLowerInvariant();
        }

        [Benchmark]
        public bool ToUpper()
        {
            return defaultString.ToUpper() == defaultString.ToUpper();
        }

        [Benchmark]
        public bool ToUpperInvariant()
        {
            return defaultString.ToUpperInvariant() == defaultString.ToUpperInvariant();
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
