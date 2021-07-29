using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Cars
{
    public record Position(decimal X, decimal Y)
    {
        public decimal GetDistance(Position from)
        {
            var powedDistance = (X - from.X) * (X - from.X) + (Y - from.Y) * (Y - from.Y);
            return (decimal)Math.Sqrt((double)powedDistance);
        }
    }
}
