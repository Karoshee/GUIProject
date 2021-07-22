using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GUIProject
{


    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("╔═══════════════════════════════════╗");
            //Console.WriteLine("║                                   ║");
            //Console.WriteLine("║                                   ║");
            //Console.WriteLine("║        Тут что-то написано.       ║");
            //Console.WriteLine("║                                   ║");
            //Console.WriteLine("║                                   ║");
            //Console.WriteLine("║            ╔════════╗             ║");
            //Console.WriteLine("║            ║   Ok   ║             ║");
            //Console.WriteLine("║            ╚════════╝             ║");
            //Console.WriteLine("╚═══════════════════════════════════╝");
            ShowMessage("Тут что-то написано. Тут что-то написано.");
        }

        public static void ShowMessage(string message, MessageType type = MessageType.Error)
        {
            PrintTopEdge();

            PrintEmptyLine();

            PrintMessage(message, type.GetScheme());

            PrintEmptyLine();

            PrintEmptyLine();
            PrintEmptyLine();
            PrintEmptyLine();

            AddButtons(new[] { "Да", "Нет", "Отмена" }, 2);

            PrintBottomEdge();

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
        }

        public static void AddButtons(string[] captions, int activeIndex = 0, int width = 0)
        {
            if (captions == null || captions.Length == 0)
                return;

            if (width <= 0)
                width += Console.WindowWidth;

            int allButtonsWidth = captions.Sum(c => c.Length) + captions.Length * 4 + captions.Length - 1;
            int xPosintion = width / 2 - allButtonsWidth / 2;

            for (int i = 0; i < captions.Length; i++)
            {
                PrintButton(captions[i], xPosintion, Console.CursorTop - 3, i == activeIndex);
                xPosintion += captions[i].Length + 5;
            }
        }

        private static void PrintButton(string caption, int xPosition, int yPosition, bool isActive)
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

        private static void PrintMessage(string message, ColorScheme scheme)
        {
            var oldScheme = ColorScheme.FromConsole();

            foreach (var line in SplitText(message))
            {
                Console.Write("║");
                Console.CursorLeft = 2;
                scheme.Apply(() => Console.Write(line));
                Console.CursorLeft = Console.WindowWidth - 1;
                Console.Write("║");
            }
        }

        private static void PrintTopEdge()
        {
            Console.Write("╔");
            PrintMany('═');
            Console.Write("╗");
        }

        private static void PrintBottomEdge()
        {
            Console.Write("╚");
            PrintMany('═');
            Console.Write("╝");
        }

        private static void PrintEmptyLine()
        {
            Console.Write("║");
            Console.CursorLeft = Console.WindowWidth - 1;
            Console.WriteLine("║");
        }

        public static void PrintMany(char ch, int repeats = - 2)
        {
            if (repeats < 0)
                repeats += Console.WindowWidth;

            for (int i = 0; i < repeats; i++)
            {
                Console.Write(ch);
            }
        }

        public static List<string> SplitText(string text, int maxLength = -4)
        {
            if (maxLength <= 0)
                maxLength += Console.WindowWidth;

            if (text is null)
                text = "";

            if (text.Length <= maxLength)
                return new List<string> { text };

            List<string> result = new List<string>(text.Length / maxLength);

            int startIndex = 0;
            int index = text.LastIndexOf(' ', maxLength);
            while (index > -1)
            {
                result.Add(text.Substring(startIndex, index - startIndex));
                startIndex = index + 1;
                if (maxLength + index >= text.Length)
                    break;
                index = text.LastIndexOf(' ', index + maxLength);
            }
            
            result.Add(text.Substring(startIndex));

            return result;
        }
    }
}
