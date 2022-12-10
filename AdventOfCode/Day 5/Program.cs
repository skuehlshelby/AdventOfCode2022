using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day_5
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Stack<char>[] crates = new Stack<char>[]
            {
                new Stack<char>("BSVZGPW"),
                new Stack<char>("JVBCZF"),
                new Stack<char>("VLMHNZDC"),
                new Stack<char>("LDMZPFJB"),
                new Stack<char>("VFCGJBQH"),
                new Stack<char>("GFQTSLB"),
                new Stack<char>("LGCZV"),
                new Stack<char>("NLG"),
                new Stack<char>("JFHC"),
            };

            using (StreamReader reader = Utility.StreamEmbeddedResource("CratesInput.txt"))
            {
                Regex regex = new Regex(@"(?:move )(\d+)(?: from )(\d+)(?: to )(\d+)");

                while (!reader.EndOfStream)
                {
                    Match match = regex.Match(reader.ReadLine());
                    int numberToMove = int.Parse(match.Groups[1].Value);
                    int fromStack = int.Parse(match.Groups[2].Value) - 1;
                    int toStack = int.Parse(match.Groups[3].Value) - 1;

                    Stack<char> group = new Stack<char>();

                    for (int i = 0; i < numberToMove; i++)
                    {
                        group.Push(crates[fromStack].Pop());
                    }

                    while (group.Any())
                    {
                        crates[toStack].Push(group.Pop());
                    }
                }
            }

            Console.WriteLine(new string(crates.Select(c => c.Peek()).ToArray()));
        }
    }
}