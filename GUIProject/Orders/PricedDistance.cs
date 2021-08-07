using OurUI;
using OurUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Orders
{
    public class PricedDistance
    {
        [Hint("Точка отправления")]
        public Position From { get; private set; }

        [Hint("Точка прибытия")]
        public Position To { get; private set; }
    }
}
