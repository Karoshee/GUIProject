using GUIProject.Cars;
using GUIProject.Common;
using GUIProject.Forms;
using System;
using System.Collections.Generic;

namespace GUIProject.Orders
{
    public class Order : IHaveId, IHaveNumber
    {
        private Order OldOrder { get; set; }

        public Guid Id { get; set; }

        [InputIgnore]
        public int Number { get; set; }

        public Position From { get; private set; }

        public Position To { get; private set; }

        public string ContactPhone { get; set; }

        public OrderState State { get; private set; }

        public Order()
        {
            Id = Guid.NewGuid();
        }

        public Order(Position from, Position to) : base()
        {
            From = from;
            To = to;
        }

        private Order(Order oldOrder)
        {
            Id = oldOrder.Id;
            From = oldOrder.From;
            To = oldOrder.To;
            State = oldOrder.State;
        }

        public void SetState(OrderState state)
        {
            State = state;
        }

        public void ChangeDestination(Position newDestination)
        {
            SaveToHistory();
            To = newDestination;
        }

        private void SaveToHistory()
        {
            if (OldOrder is null)
            {
                OldOrder = new Order(this);
            }
            else
            {
                var fix = new Order(this);
                fix.OldOrder = OldOrder;
                OldOrder = fix;
            }
        }

        public IEnumerable<Order> GetHistory()
        {
            Order current = this.OldOrder;
            while (current is not null)
            {
                yield return current;
                current = current.OldOrder;
            }
        }

        public override string ToString()
        {
            return $"Заказ № {Number} ({State})";
        }
    }
}
