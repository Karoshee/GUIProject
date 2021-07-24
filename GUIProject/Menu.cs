using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    public class Menu : Message
    {
        public string[] Elements { get; }

        public int SelectedIndex { get; set; }

        public Menu(string message, params string[] elements) 
            : base(message, MessageType.Info)
        {
            Elements = elements;
        }

        public int Show()
        {
            Printer.PrintTopEdge();

            Printer.PrintEmptyLine();

            Printer.PrintMessage(Lines, Type.GetScheme());

            Printer.PrintEmptyLine();

            for (int i = 0; i < Elements.Length; i++)
            {
                Printer.PrintMiddleLine();
                Printer.PrintMessage(new[] { $"{i + 1} - {Elements[i]}" }, i == SelectedIndex ? ColorScheme.ActiveButtonScheme : ColorScheme.FromConsole());
            }

            Printer.PrintBottomEdge();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {

                }
                else if (key.Key == ConsoleKey.DownArrow)
                {

                }
                else if (char.IsDigit(key.KeyChar))
                {
                    int index = Convert.ToInt32(key.KeyChar.ToString());
                    if (index > 0 && index <= Elements.Length)
                        return index - 1;
                }
            } while (key.Key != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
