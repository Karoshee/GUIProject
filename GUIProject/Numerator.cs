using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace GUIProject
{
    public class Numerator
    {
        private Dictionary<string, int> _Numbers { get; set; }

        private string FilePath { get; }

        public Paths Paths { get; }

        public Numerator(Paths paths)
        {
            _Numbers = new Dictionary<string, int>();
            Paths = paths;
            FilePath = Path.Combine(Paths.BaseDirectory, "Numbers.json");
        }

        public int GetNumber(Type type)
        {
            string name = type.FullName;
            if (_Numbers.ContainsKey(name))
            {
                var number = _Numbers[name];
                _Numbers[name]++;
                return number;
            }
            else
            {
                _Numbers.Add(name, 2);
                return 1;
            }
        }

        private static JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                Encoder = JavaScriptEncoder.Default
            };
        }

        public void SaveNumbers()
        {
            string jsonString =  JsonSerializer.Serialize(_Numbers, _Numbers.GetType(), GetOptions());
            File.WriteAllText(FilePath, jsonString);
        }

        public void LoadNumbers()
        {
            if (File.Exists(FilePath))
            {
                string jsonString = File.ReadAllText(FilePath);
                _Numbers = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonString, GetOptions());
            }
        }
    }
}
