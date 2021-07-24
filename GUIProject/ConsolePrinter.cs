using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    public class ConsolePrinter
    {
        public void PrintButton(string caption, int xPosition, int yPosition, bool isActive)
        {
            int oldX = Console.CursorLeft;
            int oldY = Console.CursorTop;

            ColorScheme scheme = isActive ? ColorScheme.ActiveButtonScheme : ColorScheme.FromConsole();

            scheme.Apply(() =>
            {
                Console.CursorLeft = xPosition;
                Console.CursorTop = yPosition;
                Console.Write("╔");
                PrintMany('═', caption.Length + 2);
                Console.Write("╗");
                Console.CursorLeft = xPosition;
                Console.CursorTop++;
                Console.Write("║ {0} ║", caption);
                Console.CursorLeft = xPosition;
                Console.CursorTop++;
                Console.Write("╚");
                PrintMany('═', caption.Length + 2);
                Console.Write("╝");
            });

            Console.CursorLeft = oldX;
            Console.CursorTop = oldY;
        }

        public void PrintMessage(string[] lines, ColorScheme scheme)
        {
            var oldScheme = ColorScheme.FromConsole();

            foreach (var line in lines)
            {
                Console.Write("║");
                Console.CursorLeft = 2;
                scheme.Apply(() => Console.Write(line));
                Console.CursorLeft = Console.WindowWidth - 1;
                Console.Write("║");
            }
        }

        public void PrintTopEdge()
        {
            Console.Write("╔");
            PrintMany('═');
            Console.Write("╗");
        }

        public void PrintBottomEdge()
        {
            Console.Write("╚");
            PrintMany('═');
            Console.Write("╝");
        }

        public void PrintEmptyLine()
        {
            Console.Write("║");
            Console.CursorLeft = Console.WindowWidth - 1;
            Console.WriteLine("║");
        }

        public void PrintMiddleLine()
        {
            Console.Write("╟");
            PrintMany('─');
            Console.Write("╢");
        }


        public void PrintMany(char ch, int repeats = -2)
        {
            if (repeats < 0)
                repeats += Console.WindowWidth;

            for (int i = 0; i < repeats; i++)
            {
                Console.Write(ch);
            }
        }
    }
}
