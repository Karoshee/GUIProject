using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurUI
{
    public class Menu : Message
    {
        private int _selectedIndex;

        public string[] Elements { get; }

        public int SelectedIndex
        {
            get => _selectedIndex; 
            set
            {
                if (value < 0)
                    _selectedIndex = Elements.Length - 1;
                else if (value >= Elements.Length)
                    _selectedIndex = 0;
                else
                    _selectedIndex = value;
            }
        }

        public Menu(string message, params string[] elements)
            : base(message, MessageType.Info)
        {
            Elements = elements;
        }

        public int Show()
        {
            Printer.Clear();

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
                    UpdateItem(SelectedIndex, false);
                    SelectedIndex--;
                    UpdateItem(SelectedIndex, true);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    UpdateItem(SelectedIndex, false);
                    SelectedIndex++;
                    UpdateItem(SelectedIndex, true);
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

        public void UpdateItem(int index, bool isActive)
        {
            int y = Lines.Length + index * 2 + 4;

            ColorScheme scheme = isActive ? ColorScheme.ActiveButtonScheme : ColorScheme.Default;

            scheme.Apply(2, y, () => Printer.PrintText($"{index + 1} - {Elements[index]}"));
        }
    }
}
