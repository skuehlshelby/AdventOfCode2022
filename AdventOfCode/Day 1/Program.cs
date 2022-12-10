#define PUZZLE_SECTION_TWO

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day_1
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            int elf = 1;
            IDictionary<int, int> calories = new Dictionary<int, int>();

            using (StreamReader reader = Utility.StreamEmbeddedResource("Calories.txt"))
            {
                while (!reader.EndOfStream)
                {
                    if (int.TryParse(reader.ReadLine(), out int result))
                    {
                        if (!calories.ContainsKey(elf))
                        {
                            calories.Add(elf, result);
                        }
                        else
                        {
                            calories[elf] += result;
                        }
                    }
                    else
                    {
                        elf++;
                    }
                }
            }

#if PUZZLE_SECTION_ONE
            Console.WriteLine(calories.Values.Max());
#else
            Console.WriteLine(calories.Values.OrderBy(x => x).TakeLast(3).Sum());
#endif
        }
    }
}
