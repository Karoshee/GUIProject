using GUIProject.Cars;
using GUIProject.Common;
using System;
using System.ComponentModel;

namespace GUIProject.Orders
{
    public record AssignedOrder : IHaveId, IHaveNumber
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Car Car { get; set; }

        public Order Order { get; set; }

        [Description("Номер выполенния")]
        public int Number { get; set; }

        public string ToDisplayString()
        {
            return $"{Order} назначен {Car}";
        }

        public override string ToString()
        {
            return $"Заказ {Order.Number} выполняет машина {Car}";
        }

        public AssignedOrderFile GetDataFromFile()
        {
            return new()
            {
                Id = Id,
                OrderId = Order.Id,
                CarId = Car.Id,
                Number = Number
            };
        }
    }
}
