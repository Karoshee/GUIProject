using GUIProject.Cars;
using GUIProject.Forms;
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

        public List<Car> Cars { get; }

        public CarMenu(List<Car> cars) : base(_CAR_PARK)
        {
            Cars = cars;
            AddItems(
                ("Просмотр машин", ViewCars),
                ("Ввод новой машины", InputNewCar),
                ("В главное меню", Empty));
        }

        public void ViewCars()
        {
            var newMenu = new NavigationMenu<Car>("Все машины, которые у нас есть");
            newMenu.BindItems(Cars, selectAction: c => newMenu.Show());
            newMenu.AddItems(("Возврат", Empty));
            newMenu.Show();
        }

        public void InputNewCar()
        {
            var carForm = new InputForm<Car>(Properties.Resources.NewCarText);
            if (carForm.Show())
            {
                Cars.Add(carForm.Value);
            }
            this.Show();
        }
    }
}
