using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurUI.Forms
{
    public class HintAttribute : Attribute
    {
        public string Hint { get; set; }

        public HintAttribute(string hint)
        {
            Hint = hint;
        }
    }
}
