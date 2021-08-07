using GUIProject.Cars;
using OurUI;
using OurUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Navigation
{
    public class CarMenu : NavigationMenu
    {
        private const string _CAR_PARK = "Парк машин";

        private Random rnd = new Random();

        public OurData Data { get; }

        public View<Car> View { get; }

        public CarMenu(OurData data) : base(_CAR_PARK)
        {
            Data = data;
            View = new View<Car>("Информация о машине");
            AddItems(
                ("Просмотр машин", ViewCars),
                ("Ввод новой машины", InputNewCar),
                ("Перемещение машины", MoveCar),
                ("В главное меню", Empty));            
        }

        public void ViewCars()
        {
            var newMenu = new NavigationMenu<Car>("Все машины, которые у нас есть");
            newMenu.BindItems(Data.GetData<Car>(), selectAction: c =>
            {
                View.Show(c);
                newMenu.Show();
            });
            newMenu.AddItems(("Возврат", Empty));
            newMenu.Show();
        }

        public void InputNewCar()
        {
            var carForm = new InputForm<Car>(Properties.Resources.NewCarText);
            if (carForm.Show())
            {
                Data.GetData<Car>().Add(carForm.Value);
                Data.SaveItem(carForm.Value);
            }
            this.Show();
        }

        public void MoveCar()
        {
            var carPositionMenu = new NavigationMenu<Car>("Выберите машину, которую хотите переместить");
            carPositionMenu.BindItems(Data.GetData<Car>(), selectAction: c =>
            {
                c.CurrentPosition = new(rnd.Next(0, 100000), rnd.Next(1, 99999));
                Data.SaveItem(c);
                View.Show(c);
                this.Show();
            });
            carPositionMenu.AddItems(("Отмена", Empty));
            carPositionMenu.Show();
        }
    }
}
