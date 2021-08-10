using GUIProject.Common;
using System;

namespace GUIProject.Orders
{
    public record AssignedOrderFile : IHaveId, IHaveNumber
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CarId { get; set; }

        public Guid OrderId { get; set; }

        public int Number { get; set; }
    }
}
