using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Orders
{
    public enum OrderState : byte
    {
        New = 0,
        Counted,
        Queued,
        Active,
        Closed,
        Cancelled,
    }
}
