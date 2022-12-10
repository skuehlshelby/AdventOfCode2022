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

            Console.WriteLine(rucksacks.Chunk(3).Sum(r => Rucksack.GetGroupBadgePriority(r[0], r[1], r[2])));
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

            public char GetCommonElement()
            {
                return CompartmentOne.Intersect(CompartmentTwo).Single();
            }

            public int GetPriorityOfCommonElement()
            {
                return GetPriority(GetCommonElement());
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

            public static char GetElfBadgeForGroup(Rucksack one, Rucksack two, Rucksack three)
            {
                return (one.CompartmentOne + one.CompartmentTwo).Intersect(two.CompartmentOne + two.CompartmentTwo)
                                                                .Intersect(three.CompartmentOne + three.CompartmentTwo)
                                                                .Single();
            }

            public static int GetGroupBadgePriority(Rucksack one, Rucksack two, Rucksack three)
            {
                return GetPriority(GetElfBadgeForGroup(one, two, three));
            }
        }
    }
}
