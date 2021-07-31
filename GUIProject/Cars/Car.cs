using GUIProject.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Cars
{
    public class Car
    {
        [Hint("Введите бренд")]
        public string Brand { get; set; }

        [Hint("Введите потребление топлива")]
        public decimal BaseConsumption { get; set; }

        [Hint("Введите размер бензобака")]
        public int FuelTank { get; set; }

        [Hint("Введите регистрационный номер")]
        public string PlateNumber { get; set; }

        [Hint("Введите дату выпуска")]
        public DateTime ReleaseDate { get; set; }

        public Position CurrentPosition { get; set; }

        public override string ToString()
        {
            return $"{Brand} - {ReleaseDate:dd.MM.yyyy} {CurrentPosition}";
        }
    }
}
