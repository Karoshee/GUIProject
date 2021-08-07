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

        public void AddOrder()
        {
            var orderForm = new InputForm<Order>("Введите информацию о заказе");
            if (orderForm.Show())
            {
                Order newOrder = orderForm.Value;
                Data.GetData<Order>().Add(newOrder);
                Data.SaveItem(newOrder);
                Dialog.ShowMessage("Создан новый заказ " + newOrder);
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
            var assignedOrder = new AssignedOrder(Data.GetData<Car>()[index], selectedOrder);
            Data.SaveItem(assignedOrder);
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
                var car = Data.GetData<Car>().First();
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
