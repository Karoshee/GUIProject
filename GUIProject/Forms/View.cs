using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Forms
{
    public class View<T> : Dialog
    {
        public PropertyInfo[] Properties { get; }

        public int MaxLenght { get; }

        public View(string message) : base(message, MessageType.Info)
        {
            Properties = typeof(T)
                .GetProperties(
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.GetProperty)
                .Where(p => p.GetCustomAttribute<DescriptionAttribute>() != null)
                .ToArray();

            MaxLenght = Properties
                .Select(pr => pr.GetCustomAttribute<DescriptionAttribute>().Description.Length)
                .Max();
        }
        public void Show(T item)
        {
            Printer.Clear();

            Printer.PrintTopEdge();

            Printer.PrintEmptyLine();

            Printer.PrintMessage(Lines, Type.GetScheme());

            Printer.PrintEmptyLine();

            string[] text = Properties
                .Select(pr =>
                    pr.GetCustomAttribute<DescriptionAttribute>().Description.PadLeft(MaxLenght, ' ') +
                    ": " +
                    pr.GetValue(item)?.ToString())
                .ToArray();

            Printer.PrintMessage(text, Type.GetScheme());

            Printer.PrintEmptyLine();

            Printer.PrintEmptyLine();
            Printer.PrintEmptyLine();
            Printer.PrintEmptyLine();

            PrintButtons(y: Lines.Length + text.Length + 4);

            Printer.PrintBottomEdge();

            while (Console.ReadKey().Key != ConsoleKey.Enter);
        }
    }
}
