using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.IO;

namespace TryCatch
{
    [SimpleJob(RuntimeMoniker.Net70)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [MemoryDiagnoser()]
    [MarkdownExporter, CsvExporter]
    public class Bench
    {
        private List<string> _files;

        private HashSet<string> _hash;

        [GlobalSetup]
        public void Setup()
        {
            _files = new List<string>()
            {
                "C:\\1.png",
                "C:\\User\\TestUser\\Desktop\\TestDir\\TestFile.png",
                "D:\\TestDir\\SomeFileName.exe",
                "D:\\TestDir\\SomeFileName2.fnt",
                "C:\\User\\TestUser\\Desktop\\TestDir\\TestFile.mp3",
                "E:\\TestFolder\\RandomFileName1321243.wav",
                "C:\\ProgramFiles\\TestDir\\r32r23ff23.wav"
            };

            _hash = new HashSet<string>()
            {
//                ".mp3",
                ".txt",
//                ".wav",
                //".mp4",
                //".avi",
                //".apx",
                //".dll",
                //".sys",
                //".cfg",
                //".obj"
            };
        }

        [Benchmark]
        public bool ContainsHash()
        {
            bool result = false;
            foreach (var file in _files)
            {
                var extension = Path.GetExtension(file);
                if (_hash.Contains(extension))
                    result = true;
            }

            return result;
        }

        [Benchmark]
        public bool ContainsHashTryCatch()
        {
            bool result = false;
            try
            {
                foreach (var file in _files)
                {
                    var extension = Path.GetExtension(file);
                    if (_hash.Contains(extension))
                        result = true;
                }

                if (!result)
                    throw new Exception("false");
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Bench>();
            Console.ReadKey();
        }
    }
}