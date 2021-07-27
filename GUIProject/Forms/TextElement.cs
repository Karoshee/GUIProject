using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Forms
{
    public class TextElement
    {
        protected StringBuilder _inputText = new StringBuilder();

        public int Y { get; set; }

        public string Hint { get; set; }

        public string Text => _inputText.ToString();

        private Action<string> Commit { get; }

        protected TextElement(string hint)
        {
            Hint = hint;
        }

        public TextElement(string hint, Action<string> commit) : this(hint)
        {
            Commit = commit;
        }

        public void Input()
        {
            var minPosition = Console.CursorLeft;
            int maxLength = Console.WindowWidth - Console.CursorLeft - 2;

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (Console.CursorLeft > minPosition)
                    {
                        _inputText.Remove(_inputText.Length - 1, 1);
                        Console.CursorLeft--;
                        Console.Write(' ');
                        Console.CursorLeft--;
                    }
                }
                else if (maxLength > _inputText.Length && FilterChar(keyInfo.KeyChar))
                {
                    _inputText.Append(keyInfo.KeyChar);
                    ColorScheme.InputTextScheme.Apply(() => Console.Write(keyInfo.KeyChar));
                }
            } while (keyInfo.Key != ConsoleKey.Enter || !CanCommit());
        }

        public virtual bool FilterChar(char ch) => true;

        public virtual bool CanCommit() => true;
        
    }
}
