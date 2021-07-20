using System;

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
            ShowMessage("Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. Тут что-то написано. ");
        }

        public static void ShowMessage(string message)
        {
            PrintTopEdge();

            PrintEmptyLine();

            PrintMessage(message);

            PrintEmptyLine();

            PrintEmptyLine();
            PrintEmptyLine();
            PrintEmptyLine();

            const string buttonCaption = "Очень большая кнопка";

            PrintButton(buttonCaption, Console.WindowWidth/2 - buttonCaption.Length/2, 4);

            PrintBottomEdge();

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
        }

        private static void PrintButton(string caption, int xPosition, int yPosition)
        {
            int oldX = Console.CursorLeft;
            int oldY = Console.CursorTop;

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

            Console.CursorLeft = oldX;
            Console.CursorTop = oldY;
        }

        private static void PrintMessage(string message)
        {
            Console.Write("║ ");

            Console.Write(message.PadRight(Console.WindowWidth - 4, ' '));

            Console.Write(" ║");
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
            PrintMany(' ');
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
    }
}
