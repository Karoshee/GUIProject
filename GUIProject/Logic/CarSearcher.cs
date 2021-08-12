using GUIProject.Cars;
using GUIProject.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Logic
{
    public class CarSearcher
    {
        public TimeCounter Counter { get; }

        public CarSearcher(TimeCounter counter = null)
        {
            Counter = counter ?? new TimeCounter();
        }

        public Car FindCar(IEnumerable<Car> cars, Order order)
        {
            return cars
                .Aggregate((c1, c2) => _Distance(c1, order) <= _Distance(c2, order) ? c1 : c2);
        }

        private decimal _Distance(Car car, Order order)
            => car.CurrentPosition.GetDistance(order.From);


        public Car FindCarFromOrders(IEnumerable<AssignedOrder> orders, IEnumerable<Car> cars)
        {
            if (orders is null || orders.Any() == false)
                return cars.FirstOrDefault();

            var groups = orders
                .GroupBy(o => o.Car)
                .ToArray();

            var freeCar = cars.FirstOrDefault(c => groups.All(g => g.Key.Id != c.Id));
            if (freeCar is not null)
                return freeCar;

            return groups
                .Aggregate((g1, g2) => Counter.GetChainTime(g1.Key, g1) < Counter.GetChainTime(g2.Key, g2) ? g1 : g2).Key;
        }
    }
}
