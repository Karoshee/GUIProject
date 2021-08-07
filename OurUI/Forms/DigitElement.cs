using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurUI.Forms
{
    public class DigitElement : TextElement
    {
        public int Min { get; set; } = 0;

        public int Max { get; set; } = 100000;

        public bool IsDecimal { get; }

        private Action<decimal> Commit { get; }

        public DigitElement(string hint, Action<decimal> commit) : base(hint)
        {
            IsDecimal = true;
            Commit = commit;
        }

        public DigitElement(string hint, Action<int> commit) : base(hint)
        {
            IsDecimal = false;
            Commit = d => commit((int)d);
        }

        public override bool FilterChar(char ch)
        {
            if (char.IsDigit(ch) || (IsDecimal && ch == ','))
            {
                return true;
            }
            return false;
        }

        public override bool CanCommit()
        {
            if (IsDecimal)
            {
                if (decimal.TryParse(Text, out decimal digit) && digit >= Min && digit <= Max)
                {
                    Commit?.Invoke(digit);
                    return true;
                }
            }
            else if (int.TryParse(Text, out int i) && i >= Min && i <= Max)
            {
                Commit.Invoke(i);
                return true;
            }
            return false;
        }
    }
}
