using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    public class Dialog : Message
    {
        public List<Button> Buttons { get; set; }

        public int ActiveButtonIndex { get; set; }

        public Dialog(string message, MessageType type = MessageType.Info, params Button[] buttons)
            : base(message, type)
        {
            if (buttons == null || buttons.Length == 0)
                Buttons = new List<Button> { Button.Ok };
            else
                Buttons = buttons.ToList();            
        }

        public Button Show()
        {
            Printer.Clear();

            Printer.PrintTopEdge();

            Printer.PrintEmptyLine();

            Printer.PrintMessage(Lines, Type.GetScheme());

            Printer.PrintEmptyLine();

            Printer.PrintEmptyLine();
            Printer.PrintEmptyLine();
            Printer.PrintEmptyLine();

            PrintButtons();

            Printer.PrintBottomEdge();

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow)
                {
                    ActiveButtonIndex = ActiveButtonIndex == 0 ? Buttons.Count - 1 : ActiveButtonIndex - 1;
                    PrintButtons();
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    ActiveButtonIndex = ActiveButtonIndex == Buttons.Count -1 ? 0 : ActiveButtonIndex + 1;
                    PrintButtons();
                }
            } while (key != ConsoleKey.Enter);
            return Buttons[ActiveButtonIndex];
        }

        public static void ShowMessage(string message, MessageType type = MessageType.Info, Button button = Button.Ok)
        {
            new Dialog(message, type, button)
                .Show();
        }

        public static Button ShowQuestion(string message, MessageType type = MessageType.Info, params Button[] buttons)
        {
            if (buttons is null || buttons.Length == 0)
                buttons = new Button[] { Button.Yes, Button.No };
            return new Dialog(message, type, buttons)
                .Show();
        }

        public void PrintButtons(int width = 0)
        {
            var captions = Buttons.Select(btn => btn.GetDescription()).ToArray();

            if (captions == null || captions.Length == 0)
                return;

            if (width <= 0)
                width += Console.WindowWidth;

            int allButtonsWidth = captions.Sum(c => c.Length) + captions.Length * 4 + captions.Length - 1;
            int xPosintion = width / 2 - allButtonsWidth / 2;

            for (int i = 0; i < captions.Length; i++)
            {
                Printer.PrintButton(captions[i], xPosintion, Lines.Length + 3, i == ActiveButtonIndex); ;
                xPosintion += captions[i].Length + 5;
            }
        }
    }
}
