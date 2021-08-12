using GUIProject.Cars;
using GUIProject.Common;
using GUIProject.Orders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
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

        public IFileSystem FileSystem { get; }

        public OurData(Paths paths, Numerator numerator, IFileSystem fileSystem)
        {
            _Data = new Dictionary<Type, IList>();
            Paths = paths;
            Numerator = numerator;
            FileSystem = fileSystem;
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

            _Data.Add(typeof(AssignedOrder), _Load<AssignedOrderFile>()
                .Select(item => new AssignedOrder() 
                {
                    Id = item.Id,
                    Car = GetData<Car>().First(c => c.Id == item.CarId),
                    Order = GetData<Order>().First(o => o.Id == item.OrderId),
                })
                .ToList());
        }

        private List<T> _Load<T>() where T : IHaveId
        {
            string directory = Paths.GetDirectory<T>();
            string[] files = FileSystem.Directory.GetFiles(directory);
            return files
                .Select(filename => _Load<T>(filename))
                .ToList();
        }

        public void SaveItem<T>(T item) where T: IHaveId
        {
            string filename = FileSystem.Path.Combine(Paths.GetDirectory<T>(), item.Id + ".json");
            if (item is IHaveNumber numbered)
            {
                numbered.Number = Numerator.GetNumber(item.GetType());
            }
            _Save(item, filename);
        }

        public void SaveItem(AssignedOrder order)
        {
            string filename = FileSystem.Path.Combine(Paths.GetDirectory<AssignedOrder>(), order.Id + ".json");
            order.Number = Numerator.GetNumber(order.GetType());
            _Save(order.GetDataFromFile(), filename);
        }

        public AssignedOrder GetActiveOrder(Car car)
        {
            return GetData<AssignedOrder>()
                .Where(o => o.Car.Id == car.Id && o.Order.State == OrderState.Active)
                .FirstOrDefault();
        }

        private static JsonSerializerOptions _GetOptions()
        {
            return new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                Encoder = JavaScriptEncoder.Default,
                WriteIndented = true
            };
        }

        private void _Save<T>(T item, string filename)
        {
            FileSystem.File.WriteAllText(filename, JsonSerializer.Serialize(item, _GetOptions()), Encoding.Default);
        }

        private T _Load<T>(string filename)
        {
            return JsonSerializer.Deserialize<T>(FileSystem.File.ReadAllText(filename, Encoding.Default), _GetOptions());
        }
    }
}
