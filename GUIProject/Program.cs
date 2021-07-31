using GUIProject.Cars;
using GUIProject.Forms;
using GUIProject.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace GUIProject
{


    class Program
    {
        public static string BaseDirectory = 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GUIProject");

        public static string CarDirectory =
            Path.Combine(BaseDirectory, "Cars");

        public static string OrderDirectory =
            Path.Combine(BaseDirectory, "Orders");

        public static List<Car> Cars;

        public static List<Order> Orders;

        public static List<AssignedOrder> AssignedOrders;

        public static string[] MainMenu =
        {
            "Парк машин",
            "Добавление заказа",
            "Назначение заказа",
            "Выход из программы"
        };

        public static string[] SubMenu =
        {
            "Просмотр машин",
            "Ввод новой машины",
            "В главное меню"
        };

        static void Main(string[] args)
        {
            CheckDirectories();

            Cars = LoadCars();
            Orders = LoadOrders();
            AssignedOrders = LoadAssignedOrders();

            var mainMenu = new Menu("Выберите пункт меню из списка", MainMenu);
            var subMenu = new Menu("Парк машин", SubMenu);            
            int currentMenu = mainMenu.Show();
            while (true)
            {
                switch (currentMenu)
                {
                    case 0:
                        int currentSubMenu = subMenu.Show();
                        while (currentSubMenu < 2)
                        {
                            switch (currentSubMenu)
                            {
                                case 0:
                                    var carMenu = new Menu("Все машины, которые у нас есть", Cars.Select(c => c.ToString()).Concat(new[] { "Возврат" }).ToArray());
                                    int selectedCar = carMenu.Show();
                                    while(selectedCar < Cars.Count)
                                    {
                                        selectedCar = carMenu.Show();
                                    }
                                    break;
                                case 1:
                                    var carForm = new InputForm<Car>("Введите информацию о новой машине");
                                    if(carForm.Show())
                                    {
                                        Cars.Add(carForm.Value);
                                    }
                                    break;
                            }
                            currentSubMenu = subMenu.Show();
                        }
                        break;
                    case 1:
                        var orderForm = new InputForm<Order>("Введите информацию о заказе");
                        if (orderForm.Show())
                        {
                            var newOrder = orderForm.Value;
                            Orders.Add(newOrder);
                            Dialog.ShowMessage("Создан новый заказ " + newOrder);
                        }
                        break;
                    case 2:
                        var orderMenu = new Menu("Выберите заказ", Orders.Select(o => o.ToString()).Concat(new[] { "Назад" }).ToArray());                        
                        var index = orderMenu.Show();
                        if (index == Orders.Count)
                            break;
                        var selectedOrder = Orders[index];

                        var carSubMenu = new Menu("Все машины, которые у нас есть", Cars.Select(c => c.ToString()).Concat(new[] { "Назад" }).ToArray());
                        index = carSubMenu.Show();
                        if (index == Cars.Count)
                            break;
                        var assignedOrder = new AssignedOrder(Cars[index], selectedOrder);
                        Dialog.ShowMessage(assignedOrder.ToDisplayString());
                        break;
                    case 3:
                        if (Dialog.ShowQuestion("Вы действительно хотите выйти из программы?") == Button.Yes)
                            return;
                        break;
                }
                currentMenu = mainMenu.Show();
            }
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

        private static void CheckDirectories()
        {
            if (!Directory.Exists(BaseDirectory))
                Directory.CreateDirectory(BaseDirectory);

            if (!Directory.Exists(CarDirectory))
                Directory.CreateDirectory(CarDirectory);

            if (!Directory.Exists(OrderDirectory))
                Directory.CreateDirectory(OrderDirectory);
        }
    }
}
