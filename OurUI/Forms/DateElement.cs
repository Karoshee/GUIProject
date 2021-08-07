using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurUI.Forms
{
    public class DateElement : TextElement
    {
        public static IFormatProvider Provider = new CultureInfo(CultureInfo.CurrentCulture.Name)
        {
            DateTimeFormat = new DateTimeFormatInfo() { ShortDatePattern = "dd.MM.yyyy" }
        };

        public DateTime Value { get; set; }

        private Action<DateTime> Commit { get; }

        public DateElement(string hint, Action<DateTime> commit) : base(hint)
        {
            Commit = commit;
        }

        public override bool FilterChar(char ch)
        {
            return char.IsDigit(ch) || ch == '.';
        }

        public override bool CanCommit()
        {
            if (DateTime.TryParse(Text, Provider, DateTimeStyles.AssumeLocal, out DateTime date))
            {
                Value = date;
                Commit?.Invoke(Value);
                return true;
            }
            return false;
        }
    }
}
