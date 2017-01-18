using System;

namespace OmniGui
{
    public static class ColorExtensions
    {
        public static double GetDistance(this Color a, Color b)
        {
            var diff = a.Substract(b);
            var sumOfPoweredDiffs = Math.Pow(diff.Item1, 2) + Math.Pow(diff.Item2, 2)+ Math.Pow(diff.Item3, 2)+ Math.Pow(diff.Item4, 2);
            return Math.Sqrt(sumOfPoweredDiffs);
        }

        private static Tuple<int, int, int, int> Substract(this Color a, Color b)
        {
            return new Tuple<int, int, int, int>(a.Alpha - b.Alpha, a.Red - b.Red, a.Green - b.Green, a.Blue - b.Blue);
        }
    }
}