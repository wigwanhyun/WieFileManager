
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileSearch
{
    class Program
    {
        static void Main()
        {


            var sw = Stopwatch.StartNew();
            string archiveDirectory = @"D:\";
            string searchText = "M00009 삭제할 행을 선택하세요.";
            var files = from retrievedFile in Directory.EnumerateFiles(archiveDirectory, "*", System.IO.SearchOption.AllDirectories).CatchExceptions() select retrievedFile;
            var regex = new Regex("((?:.*?)(?:\\.(png|jpg|exe|zip|7z|mp4|dll|pdb|xlsx|pdf|ppt|docx|ico|iso|svn-base|meta|msi|psd)))");
            foreach (var f in files)
            {
                sw.Restart();
                //zip, png, jpg, exe 등 파일은 제외
                
                if (regex.IsMatch(f))
                {
                    continue;
                }
                else
                {
                    try
                    {
                        var a = File.ReadLines(f, Encoding.UTF8).CatchExceptions().Any(x => x.Contains(searchText));
                        if (a == true)
                        {
                            Console.WriteLine("success : " + f);
                        }                        
                        if(sw.Elapsed > TimeSpan.FromSeconds(1.5))
                        {
                            Console.WriteLine("anyContais() took " + sw.Elapsed + " fileNm : " + f);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                
            }
            
        }

    }
}
