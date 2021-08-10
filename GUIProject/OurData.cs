using GUIProject.Cars;
using GUIProject.Common;
using GUIProject.Orders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace GUIProject
{
    public class OurData
    {
        private Dictionary<Type, IList> _Data { get; }

        public Paths Paths { get; }

        public Numerator Numerator { get; }

        public OurData(Paths paths, Numerator numerator)
        {
            _Data = new Dictionary<Type, IList>();
            Paths = paths;
            Numerator = numerator;
        }

        public IList<T> GetData<T>()
        {
            Type t = typeof(T);
            return (IList<T>)_Data[t];
        }

        public void LoadData()
        {
            _Data.Add(typeof(Car), _Load<Car>());
            _Data.Add(typeof(Order), _Load<Order>());

            _Data.Add(typeof(AssignedOrder), _Load<AssignedOrderFile>().Select(item => new AssignedOrder() 
            {
                Id = item.Id,
                Car = GetData<Car>().First(c => c.Id == item.CarId),
                Order = GetData<Order>().First(o => o.Id == item.OrderId),
            }).ToList());
        }

        private List<T> _Load<T>() where T : IHaveId
        {
            string directory = Paths.GetDirectory<T>();
            string[] files = Directory.GetFiles(directory);
            return files
                .Select(filename => File.ReadAllText(filename, Encoding.Default))
                .Select(text => JsonSerializer.Deserialize<T>(text, GetOptions()))
                .ToList();
        }

        public void SaveItem<T>(T item) where T: IHaveId
        {
            string filename = Path.Combine(Paths.GetDirectory<T>(), item.Id + ".json");
            if (item is IHaveNumber numbered)
            {
                numbered.Number = Numerator.GetNumber(item.GetType());
            }
            File.WriteAllText(filename, JsonSerializer.Serialize(item, GetOptions()), Encoding.Default);
        }

        public void SaveItem(AssignedOrder order)
        {
            string filename = Path.Combine(Paths.GetDirectory<AssignedOrder>(), order.Id + ".json");
            order.Number = Numerator.GetNumber(order.GetType());
            File.WriteAllText(filename, JsonSerializer.Serialize(order.GetDataFroFile(), GetOptions()), Encoding.Default);
        }

        public AssignedOrder GetActiveOrder(Car car)
        {
            return GetData<AssignedOrder>()
                .Where(o => o.Car.Id == car.Id && o.Order.State == OrderState.Active)
                .FirstOrDefault();
        }

        private static JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                Encoder = JavaScriptEncoder.Default
            };
        }
    }
}
