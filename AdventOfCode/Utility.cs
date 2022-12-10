using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode
{
    internal static class Utility
    {
        public static StreamReader StreamEmbeddedResource(string filename)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.EndsWith(filename))
                {
                    return new StreamReader(assembly.GetManifestResourceStream(resourceName)!);
                }
            }

            throw new ArgumentException($"No embedded resource found with the name {filename}.", nameof(filename));
        }
    }
}
