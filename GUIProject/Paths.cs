using GUIProject.Cars;
using GUIProject.Orders;
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

        private Dictionary<Type, string> _Directories { get; }

        public Paths()
        {
            _Directories = new Dictionary<Type, string>
            {
                { typeof(Car), Path.Combine(BaseDirectory, "Cars") },
                { typeof(Order), Path.Combine(BaseDirectory, "Orders") },
                { typeof(AssignedOrder), Path.Combine(BaseDirectory, "AssignedOrders") },
                { typeof(AssignedOrderFile), Path.Combine(BaseDirectory, "AssignedOrders") },
            };
            _CheckDirectories();
        }

        public string GetDirectory<T>()
        {
            Type t = typeof(T);
            return _Directories[t];
        }

        private void _CheckDirectories()
        {
            if (!Directory.Exists(BaseDirectory))
                Directory.CreateDirectory(BaseDirectory);

            foreach (string filename in _Directories.Values)
            {
                if (!Directory.Exists(filename))
                    Directory.CreateDirectory(filename);
            }
        }
    }
}
