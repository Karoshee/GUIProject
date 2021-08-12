using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OurUI.Forms
{
    public class PositionElement : TextElement
    {
        private Action<Position> Commit { get; }

        public bool CanComplete { get; private set; }

        public decimal XPosition { get; private set; }

        public PositionElement(string hint, Action<Position> commit) : base(hint)
        {
            Commit = commit;
            MaxLength = 10;
        }

        public override bool FilterChar(char ch)
        {
            return char.IsDigit(ch) || ch == ',';
        }

        public override bool CanCommit()
        {
            if (decimal.TryParse(Text, out decimal digit))
            {
                if (CanComplete)
                {
                    Commit?.Invoke(new Position(XPosition, digit));
                    return true;
                }
                else
                {
                    XPosition = digit;
                    Console.CursorLeft = MinPosition + MaxLength + 1;
                    _inputText.Clear();
                    MinPosition = Console.CursorLeft;
                    CanComplete = true;
                }
            }
            return false;
        }

        public override bool Input()
        {
            CanComplete = false;
            int firstX = Console.CursorLeft;
            Console.Write("(");
            Console.CursorLeft += MaxLength;
            Console.Write(";");
            Console.CursorLeft += MaxLength;
            Console.Write(")");
            Console.CursorLeft = firstX + 1;
            return base.Input();
        }

        // fjdsfjksdjfkljsdf: (12321321312;1213123213)
    }
}
