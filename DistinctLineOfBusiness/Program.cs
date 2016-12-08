using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DistinctLineOfBusiness
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = args.First();
            Console.WriteLine("I am going to extract distinct value for column 36 (base 0) from this csv file: " + fileName);

            var lines = File.ReadLines(fileName);
            var businessSet = new HashSet<string>();
            int i = 0;
            foreach (var line in lines)
            {
                var elements = line.Split('|');
                if (elements.Count() > 37)
                {
                    var business = elements[36];
                    if (!businessSet.Contains(business)) businessSet.Add(business);
                }
                i++;

                if (i % 1000000 == 0)
                    Console.WriteLine($"\t {i} lines done.");
            }

            var targetFileName = @"C:\temp\Biz.txt";
            using (var x = File.CreateText(targetFileName))
            {
                foreach (var item in businessSet.OrderBy(foo => foo))
                {
                    x.WriteLine(item);
                }
            }
            Console.WriteLine("Distinct lines of business has been written into this file: " + targetFileName);
        }
    }
}
