using System;

namespace OurUI
{
    public class ColorScheme
    {
        public ConsoleColor Backcolor { get; set; }
        public ConsoleColor Fontcolor { get; set; }

        public ColorScheme(ConsoleColor backcolor, ConsoleColor fontcolor)
        {
            Backcolor = backcolor;
            Fontcolor = fontcolor;
        }

        public static ColorScheme Default 
            = new ColorScheme(ConsoleColor.Black, ConsoleColor.White);

        public static ColorScheme FromConsole()
            => new ColorScheme(Console.BackgroundColor, Console.ForegroundColor);

        public static ColorScheme ActiveButtonScheme
            = new ColorScheme(ConsoleColor.White, ConsoleColor.Black);

        public static ColorScheme InputTextScheme
            = new ColorScheme(ConsoleColor.White, ConsoleColor.DarkGray);
    }
}
