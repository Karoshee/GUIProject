using GUIProject.Cars;
using OurUI.Forms;
using GUIProject.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OurUI;
using GUIProject.Logic;

namespace GUIProject.Navigation
{
    public class MainMenu : NavigationMenu
    {
        private const string _MAIN_MAIN_MENU = "Выберите пункт меню из списка";

        public OurData Data { get; }

        public CarMenu CarMenu { get; }

        public MainMenu(OurData data, CarMenu carMenu) : base(_MAIN_MAIN_MENU)
        {
            Data = data;
            CarMenu = carMenu;
            AddItems(
                ("Парк машин", ShowCarPark),
                ("Заказы в работе", ShowAssignedOrders),
                ("Добавление заказа", AddOrder),
                ("Назначение заказа", SetAssignedOrder),
                ("Рассчитать стоимость", CountPrice),
                ("Выход из программы", ExitQuestion));            
        }

        public void ShowCarPark()
        {
            CarMenu.Show();
            Show();
        }

        public void ShowAssignedOrders()
        {
            var newMenu = new NavigationMenu<AssignedOrder>("Заказы в работе");
            newMenu.BindItems(Data.GetData<AssignedOrder>(), selectAction: c =>
            {
                newMenu.Show();
            });
            newMenu.AddItems(("Возврат", Show));
            newMenu.Show();
            Show();
        }

        public void AddOrder()
        {
            var orderForm = new InputForm<Order>("Введите информацию о заказе");
            if (orderForm.Show())
            {
                Order newOrder = orderForm.Value;
                Data.GetData<Order>().Add(newOrder);
                Car selectedCar = 
                    new CarSearcher()
                        .FindCarFromOrders(Data.GetData<AssignedOrder>(), Data.GetData<Car>());
                var newAssignedOrder = new AssignedOrder
                {
                    Car = selectedCar,
                    Order = newOrder
                };
                if (Data.GetActiveOrder(selectedCar) is null)
                    newOrder.SetState(OrderState.Active);
                else
                    newOrder.SetState(OrderState.Queued);
                Data.SaveItem(newOrder);
                Data.SaveItem(newAssignedOrder);
                var averangeTime = new TimeCounter().GetChainTime(selectedCar, Data.GetData<AssignedOrder>());
                Dialog.ShowMessage($"Создан новый заказ {newOrder}, назначен {selectedCar}, будет выполен примерно через {averangeTime.TotalHours} часов");
                Show();
            }
        }

        public void SetAssignedOrder()
        {
            var orderMenu = new Menu("Выберите заказ", Data.GetData<Order>().Select(o => o.ToString()).Concat(new[] { "Назад" }).ToArray());
            var index = orderMenu.Show();
            if (index == Data.GetData<Order>().Count)
            {
                Show();
                return;
            }
            var selectedOrder = Data.GetData<Order>()[index];

            var carSubMenu = new Menu("Все машины, которые у нас есть", Data.GetData<Car>().Select(c => c.ToString()).Concat(new[] { "Назад" }).ToArray());
            index = carSubMenu.Show();
            if (index == Data.GetData<Car>().Count)
            {
                Show();
                return;
            }

            var selectedCar = Data.GetData<Car>()[index];
            var assignedOrder = new AssignedOrder()
            {
                Car = selectedCar,
                Order = selectedOrder
            };
            if (Data.GetActiveOrder(selectedCar) is null)
                selectedOrder.SetState(OrderState.Active);
            else
                selectedOrder.SetState(OrderState.Queued);
            Data.SaveItem(selectedOrder);
            Data.SaveItem(assignedOrder);
            Data.GetData<AssignedOrder>().Add(assignedOrder);
            Dialog.ShowMessage(assignedOrder.ToDisplayString());
            Show();
        }

        public void CountPrice()
        {
            var orderForm = new InputForm<Order>("Введите информацию о маршруте");
            if (orderForm.Show())
            {
                Order newOrder = orderForm.Value;
                newOrder.SetState(OrderState.Counted);
                Data.GetData<Order>().Add(newOrder);
                Data.SaveItem(newOrder);
                Car car = 
                    new CarSearcher()
                        .FindCar(Data.GetData<Car>(), newOrder);
                PriceCounter counter = new();
                var price = counter.Count(newOrder, car);
                Dialog.ShowMessage($"Заказ обойдётся в {price} р.");
                Show();
            }
        }

        public void ExitQuestion()
        {
            if (Dialog.ShowQuestion("Вы действительно хотите выйти из программы?") == Button.No)
                Show();
        }
    }
}
