using GUIProject.Cars;
using GUIProject.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    public class OurData
    {
        public List<Car> Cars { get; private set; }

        public List<Order> Orders { get; private set; }

        public List<AssignedOrder> AssignedOrders { get; private set; }

        public OurData()
        {
            Cars = LoadCars();
            Orders = LoadOrders();
            AssignedOrders = LoadAssignedOrders();            
        }

        private static List<AssignedOrder> LoadAssignedOrders()
        {
            return new List<AssignedOrder>();
        }

        private static List<Order> LoadOrders()
        {
            return new List<Order>();
        }

        private static List<Car> LoadCars()
        {
            return new List<Car>
            {
                new Car
                {
                    Brand = "Car1",
                    BaseConsumption = 0.1M,
                    CurrentPosition = new Position(12, 19),
                    FuelTank = 45,
                    PlateNumber = "x192xx",
                    ReleaseDate = new DateTime(2000, 1, 1)
                },
                new Car
                {
                    Brand = "Car2",
                    BaseConsumption = 0.1M,
                    CurrentPosition = new Position(20, 19),
                    FuelTank = 45,
                    PlateNumber = "x102xx",
                    ReleaseDate = new DateTime(2000, 1, 1)
                },
                new Car
                {
                    Brand = "Car3",
                    BaseConsumption = 0.1M,
                    CurrentPosition = new Position(12, 18),
                    FuelTank = 45,
                    PlateNumber = "x112xx",
                    ReleaseDate = new DateTime(2000, 1, 1)
                },
                new Car
                {
                    Brand = "Car4",
                    BaseConsumption = 0.1M,
                    CurrentPosition = new Position(1, 19),
                    FuelTank = 45,
                    PlateNumber = "x123xx",
                    ReleaseDate = new DateTime(2000, 1, 1)
                },
                new Car
                {
                    Brand = "Car5",
                    BaseConsumption = 0.1M,
                    CurrentPosition = new Position(10, 11),
                    FuelTank = 45,
                    PlateNumber = "x122xy",
                    ReleaseDate = new DateTime(2000, 1, 1)
                },
            };
        }
    }
}
