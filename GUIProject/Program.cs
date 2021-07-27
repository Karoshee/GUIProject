using GUIProject.Cars;
using GUIProject.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GUIProject
{


    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("╔═══════════════════════════════════╗");
            //Console.WriteLine("║                                   ║");
            //Console.WriteLine("║                                   ║");
            //Console.WriteLine("║        Тут что-то написано.       ║");
            //Console.WriteLine("║                                   ║");
            //Console.WriteLine("║                                   ║");
            //Console.WriteLine("║            ╔════════╗             ║");
            //Console.WriteLine("║            ║   Ok   ║             ║");
            //Console.WriteLine("║            ╚════════╝             ║");
            //Console.WriteLine("╚═══════════════════════════════════╝");

            //var result = Dialog.ShowQuestion("Тут что-то написано. Тут что-то написано.", MessageType.Info, Button.Yes, Button.No, Button.Cancel);

            //if (result == Button.Yes)
            //{
            //    Console.WriteLine("Вы выбрали Да");
            //}

            //var result = new Menu("Тут что-то написано. Тут что-то написано.", "Элемент номер раз", "Элемент номер два", "Элемент номер три").Show();

            //if (result == 0)
            //{
            //    Console.WriteLine("Выбран первый элемент");
            //}

            InputForm<Car> form = new("Введите информацию о машине");
            form.Show();
        }
    }
}
