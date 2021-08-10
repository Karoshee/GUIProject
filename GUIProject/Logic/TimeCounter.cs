using GUIProject.Cars;
using GUIProject.Orders;
using OurUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Logic
{
    public class TimeCounter
    {
        public TimeSpan GetChainTime(Car car, IEnumerable<AssignedOrder> orders)
        {
            IEnumerable<AssignedOrder> ourOrders = 
                orders
                    .Where(o => o.Car.Id == car.Id);

            decimal totalDistance = 0M;
            Position currentPosition = car.CurrentPosition;
            foreach (var item in ourOrders)
            {
                totalDistance += 
                    currentPosition.GetDistance(item.Order.From) + 
                    item.Order.From.GetDistance(item.Order.To);
            }

            return TimeSpan.FromSeconds((double)(totalDistance / car.Speed));
        }
    }
}
