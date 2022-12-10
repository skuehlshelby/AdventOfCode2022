using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int rock = 1;
            const int paper = 2;
            const int scissors = 3;

            const char ROCK = 'A';
            const char PAPER = 'B';
            const char SCISSORS = 'C';

            const char LOSE = 'X';
            const char DRAW = 'Y';
            const char WIN = 'Z';

            IDictionary<string, int> possibleplays = new Dictionary<string, int>()
            {
                { $"{ROCK} {LOSE}", Lose(scissors)},
                { $"{ROCK} {DRAW}", Draw(rock)},
                { $"{ROCK} {WIN}", Win(paper)},
                { $"{PAPER} {LOSE}", Lose(rock)},
                { $"{PAPER} {DRAW}", Draw(paper)},
                { $"{PAPER} {WIN}", Win(scissors)},
                { $"{SCISSORS} {LOSE}", Lose(paper)},
                { $"{SCISSORS} {DRAW}", Draw(scissors)},
                { $"{SCISSORS} {WIN}", Win(rock)},
            };

            int score = 0;

            using (StreamReader reader = Utility.StreamEmbeddedResource("Strategy.txt"))
            {
                while (!reader.EndOfStream)
                {
                    score += possibleplays[reader.ReadLine()!];
                }
            }

            Console.WriteLine(score);
        }

        static int Lose(int play) => play + 0;
        static int Draw(int play) => play +  3;
        static int Win(int play) => play + 6;
    }
}