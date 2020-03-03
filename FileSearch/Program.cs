
using sun.text.normalizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSearch
{
    class Program
    {
        static void Main()
        {
            string[] needles = CreateNeedles(1500).ToArray();
            string archiveDirectory = @"D:\";
            //anyViaContains

            var files =  from retrievedFile  in Directory.EnumerateFiles(archiveDirectory, "*.txt", System.IO.SearchOption.AllDirectories).CatchExceptions() select retrievedFile;

            var sw = Stopwatch.StartNew();
            foreach (var f in files)
            {
                anyViaAhoCorasick(File.ReadLines(f).ToArray(), "max_line_length = 120");
            }
            Console.WriteLine("anyViaContains() took " + sw.Elapsed);

            sw.Restart();
            foreach (var f in files)
            {
                
                anyViaAhoCorasick(string.Join(",", File.ReadLines(f)).Split(","), "max_line_length = 120");
            }
            Console.WriteLine("anyViaContains() took " + sw.Elapsed);

            sw.Restart();
            foreach (var f in files)
            {
                var a = File.ReadLines(f).Where(x => x.Contains("max_line_length = 120"));
                Console.WriteLine(string.Join(",", a));
            }
            Console.WriteLine("anyViaAhoCorasick() took " + sw.Elapsed);
        }

        static bool anyViaContains(string[] needles, string haystack)
        {
            return needles.Any(haystack.Contains);
        }

        static bool anyViaAhoCorasick(string[] needles, string haystack)
        {
            var trie = new Trie();
            trie.Add(needles);
            trie.Build();
            return trie.Find(haystack).Any();
        }

        static IEnumerable<string> CreateNeedles(int n)
        {
            for (int i = 0; i < n; ++i)
                yield return n + "." + n + "." + n;
        }

        static string createHaystack(int n)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < n; ++i)
                sb.AppendLine("Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text");

            return sb.ToString();
        }

    }
}
