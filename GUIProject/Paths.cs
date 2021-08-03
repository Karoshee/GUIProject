using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    public class Paths
    {
        public string BaseDirectory { get; } =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GUIProject");

        public string CarDirectory { get; }
            

        public string OrderDirectory { get; }

        public Paths()
        {
            CarDirectory = Path.Combine(BaseDirectory, "Cars");
            OrderDirectory = Path.Combine(BaseDirectory, "Orders");
            CheckDirectories();
        }

        private void CheckDirectories()
        {
            if (!Directory.Exists(BaseDirectory))
                Directory.CreateDirectory(BaseDirectory);

            if (!Directory.Exists(CarDirectory))
                Directory.CreateDirectory(CarDirectory);

            if (!Directory.Exists(OrderDirectory))
                Directory.CreateDirectory(OrderDirectory);
        }
    }
}
