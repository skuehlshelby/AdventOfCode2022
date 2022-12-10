using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day_3
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            
            ICollection<Rucksack> rucksacks = new List<Rucksack>();

            using (StreamReader reader = Utility.StreamEmbeddedResource("Rucksacks.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    int midpoint = line.Length / 2;
                    rucksacks.Add(new Rucksack(line[..midpoint], line[midpoint..]));
                }
            }

            Console.WriteLine($"The sum of all common item priorities is: {rucksacks.Sum(r => r.GetPriorityOfItemCommonToBothCompartments())}");
            Console.WriteLine($"The sum of all elf badge priorities is: {rucksacks.Chunk(3).Sum(r => Rucksack.FindElfBadgePriority(r[0], r[1], r[2]))}");
        }

        private sealed class Rucksack
        {
            public Rucksack(string compartmentOne, string compartmentTwo)
            {
                CompartmentOne = compartmentOne ?? throw new ArgumentNullException(nameof(compartmentOne));
                CompartmentTwo = compartmentTwo ?? throw new ArgumentNullException(nameof(compartmentTwo));
                if (compartmentOne.Length != compartmentTwo.Length)
                {
                    throw new ArgumentException($"Compartments are not the same length. '{compartmentOne.Length}' vs. '{compartmentTwo.Length}'.");
                }
            }

            public string CompartmentOne { get; }

            public string CompartmentTwo { get; }

            public char GetItemCommonToBothCompartments()
            {
                return CompartmentOne.Intersect(CompartmentTwo).Single();
            }

            public int GetPriorityOfItemCommonToBothCompartments()
            {
                return GetPriority(GetItemCommonToBothCompartments());
            }

            public override string ToString()
            {
                return $"{CompartmentOne} | {CompartmentTwo}";
            }

            private static int GetPriority(char element)
            {
                int offset = char.IsUpper(element) ? (-65 + 27) : -96;

                return ((int)element) + offset;
            }

            public static char FindElfBadge(Rucksack one, Rucksack two, Rucksack three)
            {
                return (one.CompartmentOne + one.CompartmentTwo).Intersect(two.CompartmentOne + two.CompartmentTwo)
                                                                .Intersect(three.CompartmentOne + three.CompartmentTwo)
                                                                .Single();
            }

            public static int FindElfBadgePriority(Rucksack one, Rucksack two, Rucksack three)
            {
                return GetPriority(FindElfBadge(one, two, three));
            }
        }
    }
}
