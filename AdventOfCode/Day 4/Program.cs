using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampSectionId = System.UInt16;

namespace AdventOfCode.Day_4
{
    internal static class Program
    {
        public static void Main(string[] args) 
        {
            ICollection<Tuple<CleaningAssignment, CleaningAssignment>> cleaningAssignments = new List<Tuple<CleaningAssignment, CleaningAssignment>>();

            using (StreamReader reader = Utility.StreamEmbeddedResource("CleaningAssignmentPairs.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split(new char[] { '-', ',' }, 4);
                    CleaningAssignment assignmentOne = new CleaningAssignment(CampSectionId.Parse(data[0]), CampSectionId.Parse(data[1]));
                    CleaningAssignment assignmentTwo = new CleaningAssignment(CampSectionId.Parse(data[2]), CampSectionId.Parse(data[3]));
                    cleaningAssignments.Add(Tuple.Create(assignmentOne, assignmentTwo));
                }
            }

            Console.WriteLine($"There are {cleaningAssignments.Count(a => CleaningAssignment.OneCompletelyContainsOther(a.Item1, a.Item2))} assignments where one completely contains the other.");
            Console.WriteLine($"There are {cleaningAssignments.Count(a => a.Item1.OverlapsWith(a.Item2))} assignments which overlap.");
        }

        

        private sealed class CleaningAssignment
        {
            public CleaningAssignment(ushort start, ushort end)
            {
                Start = start;
                End = end;
            }

            public CampSectionId Start { get; }

            public CampSectionId End { get; }

            public bool CompletelyContains(CleaningAssignment other)
            {
                return Start <= other.Start && other.End <= End;
            }

            public bool OverlapsWith(CleaningAssignment other)
            {
                return (other.Start <= Start && Start <= other.End) || CompletelyContains(other) || (other.Start <= End && End <= other.End);
            }

            public override string ToString()
            {
                return $"{Start}-{End}";
            }

            public static bool OneCompletelyContainsOther(CleaningAssignment left, CleaningAssignment right)
            {
                return left.CompletelyContains(right) || right.CompletelyContains(left);
            }
        }
    }
}
