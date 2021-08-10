using GUIProject.Common;
using OurUI;
using OurUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Cars
{
    public record Car : IHaveId
    {
        [Hint("Введите бренд")]
        [Description("Бренд")]
        public string Brand { get; set; }

        [Hint("Введите потребление топлива")]
        [Description("Потребление топлива")]
        public decimal BaseConsumption { get; set; }

        [Hint("Введите размер бензобака")]
        [Description("Бензобак")]
        public int FuelTank { get; set; }

        [Hint("Введите регистрационный номер")]
        [Description("Номер")]
        public string PlateNumber { get; set; }

        [Hint("Введите дату выпуска")]
        [Description("Дата выпуска")]
        public DateTime ReleaseDate { get; set; }

        [Description("Местонахождение")]
        public Position CurrentPosition { get; set; }

        [Hint("Введите среднюю скорость")]
        [Description("Средняя скорость")]
        public decimal Speed { get; set; }


        public Guid Id { get; set; } = Guid.NewGuid();

        public override string ToString()
        {
            return $"{Brand} - {ReleaseDate:dd.MM.yyyy} {CurrentPosition}";
        }

    }
}
