using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface ISerializableProcessor
    {

    }

    class Program
    {
        class Product
        {
            public string Name { get; set; }
            public DateTime ExpiryDate { get; set; }
            public decimal Price { get; set; }
            public IList<string> Sizes { get; set; }
        }

        static void Main(string[] args)
        {
            Product product = new Product();

            product.Name = "Apple";
            product.ExpiryDate = new DateTime(2008, 12, 28);
            product.Price = 3.99M;
            product.Sizes = new string[] { "Small", "Medium", "Large" };

            string output = JsonConvert.SerializeObject(product);

            Console.WriteLine(output);

            Product deserializedProduct = JsonConvert.DeserializeObject<Product>(output);

        }

        static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        static T DeserializeObject<T>(string jsonObj)
        {
            return JsonConvert.DeserializeObject<T>(jsonObj);
        }
    }
}
