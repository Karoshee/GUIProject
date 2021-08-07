using GUIProject.Cars;
using GUIProject.Orders;
using OurUI;
using OurUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Logic
{
    public class PriceCounter
    {
        public decimal ProcentageProfit { get; } = 50;

        public decimal Count(Order order, Car car)
        {
            var totalDistance = 
                car.CurrentPosition.GetDistance(order.From) + 
                order.From.GetDistance(order.To);

            var fuelPrice = FuelPrice(totalDistance * car.BaseConsumption);

            var driverPrice = DriverPrice(totalDistance);

            var total = fuelPrice + driverPrice;

            return total + total * ProcentageProfit / 100;
        }

        public decimal FuelPrice(decimal fuelCount)
        {
            return fuelCount * 45.1M;
        }

        public decimal DriverPrice(decimal distance)
        {
            return distance * 100;
        }
    }
}
