using GUIProject.Cars;
using GUIProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Orders
{
    public record AssignedOrder(Car Car, Order Order) : IHaveId
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ToDisplayString()
        {
            return $"{Order} назначен {Car}";
        }
    }   
}
