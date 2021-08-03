using GUIProject.Forms;
using GUIProject.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var newOrder = orderForm.Value;
                Data.Orders.Add(newOrder);
                Dialog.ShowMessage("Создан новый заказ " + newOrder);
                Show();
            }
        }

        public void SetAssignedOrder()
        {
            var orderMenu = new Menu("Выберите заказ", Data.Orders.Select(o => o.ToString()).Concat(new[] { "Назад" }).ToArray());
            var index = orderMenu.Show();
            if (index == Data.Orders.Count)
            {
                Show();
                return;
            }
            var selectedOrder = Data.Orders[index];

            var carSubMenu = new Menu("Все машины, которые у нас есть", Data.Cars.Select(c => c.ToString()).Concat(new[] { "Назад" }).ToArray());
            index = carSubMenu.Show();
            if (index == Data.Cars.Count)
            {
                Show();
                return;
            }
            var assignedOrder = new AssignedOrder(Data.Cars[index], selectedOrder);
            Dialog.ShowMessage(assignedOrder.ToDisplayString());
            Show();
        }

        public void ExitQuestion()
        {
            if (Dialog.ShowQuestion("Вы действительно хотите выйти из программы?") == Button.No)
                Show();
        }
    }
}
