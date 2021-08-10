using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurUI.Forms
{
    public class TextElement
    {
        protected StringBuilder _inputText = new StringBuilder();

        public int Y { get; set; }

        public string Hint { get; set; }

        public string Text => _inputText.ToString();

        private Action<string> Commit { get; }
        public int MinPosition { get; protected set; }
        public int MaxLength { get; protected set; }

        protected TextElement(string hint)
        {
            Hint = hint;
        }

        public TextElement(string hint, Action<string> commit) : this(hint)
        {
            Commit = commit;
        }

        public virtual bool Input()
        {
            if (MinPosition == 0)
                MinPosition = Console.CursorLeft;

            if (MaxLength == 0)
                MaxLength = Console.WindowWidth - Console.CursorLeft - 2;

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                    return false;
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (Console.CursorLeft > MinPosition && _inputText.Length > 0)
                    {
                        _inputText.Remove(_inputText.Length - 1, 1);
                        Console.CursorLeft--;
                        Console.Write(' ');
                        Console.CursorLeft--;
                    }
                }
                else if (MaxLength > _inputText.Length && FilterChar(keyInfo.KeyChar))
                {
                    _inputText.Append(keyInfo.KeyChar);
                    ColorScheme.InputTextScheme.Apply(() => Console.Write(keyInfo.KeyChar));
                }
            } while (keyInfo.Key != ConsoleKey.Enter || !CanCommit());
            Commit?.Invoke(Text);
            return true;
        }

        public virtual bool FilterChar(char ch) => ch != '\r';

        public virtual bool CanCommit() => true;
        
    }
}
