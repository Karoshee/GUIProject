using GUIProject.Cars;
using GUIProject.Forms;
using GUIProject.Navigation;
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
        static void Main(string[] args)        
        {
            var paths = new Paths();
            
            var numerator = new Numerator(paths);
            numerator.LoadNumbers();

            var data = new OurData(paths, numerator);
            data.LoadData();

            var carMenu = new CarMenu(data);
            new MainMenu(data, carMenu).Show();
        }

        static void OldMain(string[] args)
        {
            //CheckDirectories();

            //Cars = LoadCars();
            //Orders = LoadOrders();
            //AssignedOrders = LoadAssignedOrders();

            //var mainMenu = new Menu("Выберите пункт меню из списка", MainMenu);
            //var subMenu = new Menu("Парк машин", SubMenu);
            //int currentMenu = mainMenu.Show();
            //while (true)
            //{
            //    switch (currentMenu)
            //    {
            //        case 0:
            //            int currentSubMenu = subMenu.Show();
            //            while (currentSubMenu < 2)
            //            {
            //                switch (currentSubMenu)
            //                {
            //                    case 0:
            //                        var carMenu = new Menu("Все машины, которые у нас есть", Cars.Select(c => c.ToString()).Concat(new[] { "Возврат" }).ToArray());
            //                        int selectedCar = carMenu.Show();
            //                        while(selectedCar < Cars.Count)
            //                        {
            //                            selectedCar = carMenu.Show();
            //                        }
            //                        break;
            //                    case 1:
            //                        var carForm = new InputForm<Car>("Введите информацию о новой машине");
            //                        if(carForm.Show())
            //                        {
            //                            Cars.Add(carForm.Value);
            //                        }
            //                        break;
            //                }
            //                currentSubMenu = subMenu.Show();
            //            }
            //            break;
            //        case 1:
            //            var orderForm = new InputForm<Order>("Введите информацию о заказе");
            //            if (orderForm.Show())
            //            {
            //                var newOrder = orderForm.Value;
            //                Orders.Add(newOrder);
            //                Dialog.ShowMessage("Создан новый заказ " + newOrder);
            //            }
            //            break;
            //        case 2:
            //            var orderMenu = new Menu("Выберите заказ", Orders.Select(o => o.ToString()).Concat(new[] { "Назад" }).ToArray());                        
            //            var index = orderMenu.Show();
            //            if (index == Orders.Count)
            //                break;
            //            var selectedOrder = Orders[index];

            //            var carSubMenu = new Menu("Все машины, которые у нас есть", Cars.Select(c => c.ToString()).Concat(new[] { "Назад" }).ToArray());
            //            index = carSubMenu.Show();
            //            if (index == Cars.Count)
            //                break;
            //            var assignedOrder = new AssignedOrder(Cars[index], selectedOrder);
            //            Dialog.ShowMessage(assignedOrder.ToDisplayString());
            //            break;
            //        case 3:
            //            if (Dialog.ShowQuestion("Вы действительно хотите выйти из программы?") == Button.Yes)
            //                return;
            //            break;
            //    }
            //    currentMenu = mainMenu.Show();
            //}
        }

       
    }
}
