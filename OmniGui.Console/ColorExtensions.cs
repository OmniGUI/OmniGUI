using System;
using System.Collections.Generic;
using System.Linq;

namespace OmniGui.Console
{
    public static class ColorExtensions
    {
        private static IDictionary<Color, ConsoleColor> GetDictionary()
        {
            return new Dictionary<Color, ConsoleColor>()
            {
                {Color.Parse("#FF000000"), ConsoleColor.Black},
                {Color.Parse("#FF00008B"), ConsoleColor.DarkBlue},
                {Color.Parse("#FF006400"), ConsoleColor.DarkGreen},
                {Color.Parse("#FF8B0000"), ConsoleColor.DarkRed},
                {Color.Parse("#FF008B8B"), ConsoleColor.DarkCyan},
                {Color.Parse("#FF808080"), ConsoleColor.Gray},
                {Color.Parse("#FF8B008B"), ConsoleColor.DarkMagenta},
                {Color.Parse("#FFd7c32a"), ConsoleColor.DarkYellow},
                {Color.Parse("#FF0000FF"), ConsoleColor.Blue},
                {Color.Parse("#FFA9A9A9"), ConsoleColor.DarkGray},
                {Color.Parse("#FF008000"), ConsoleColor.Green},
                {Color.Parse("#FF00FFFF"), ConsoleColor.Cyan},
                {Color.Parse("#FFFF0000"), ConsoleColor.Red},
                {Color.Parse("#FFFF00FF"), ConsoleColor.Magenta},
                {Color.Parse("#FFFFFF00"), ConsoleColor.Yellow},
                {Color.Parse("#FFFFFFFF"), ConsoleColor.White}
            };
        }
        
        public static ConsoleColor ClosestConsoleColor(Color color)
        {
            var colorsByDistances =
                from entry in GetDictionary()
                let distance = entry.Key.GetDistance(color)
                orderby distance
                select new {entry.Value, distance};
      
            return colorsByDistances
                .First()
                .Value;
        }
    }
}