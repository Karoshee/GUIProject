using GUIProject.Cars;
using GUIProject.Common;
using OurUI;
using OurUI.Forms;
using System;
using System.Collections.Generic;

namespace GUIProject.Orders
{
    public record Order : IHaveId, IHaveNumber
    {
        private Position _to;
        private Position _from;

        private Order OldOrder { get; set; }

        public Guid Id { get; set; }

        [InputIgnore]
        public int Number { get; set; }

        [Hint("Точка отправления")]
        public Position From
        {
            get => _from; 
            set
            {
                if (From is not null)
                    SaveToHistory();
                _from = value;
            }
        }

        [Hint("Точка прибытия")]
        public Position To
        {
            get => _to;
            set
            {
                if (To is not null)
                    SaveToHistory();
                _to = value;
            }
        }

        [InputIgnore]
        public string ContactPhone { get; set; } = "90998743434";

        public OrderState State { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
        }

        public Order(Position from, Position to) : base()
        {
            From = from;
            To = to;
        }

        protected Order(Order oldOrder)
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
