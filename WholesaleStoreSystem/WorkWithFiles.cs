using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WholesaleStoreSystem
{
    internal class WorkWithFiles
    {
        private string pathTableProducts = "Products.csv";
        public static void getDataAtFiles()
        {
            Products products = new Products();
            BinaryFormatter bf = new BinaryFormatter();
            List<string> allProducts = new List<string>(); //хранилище списка всех товаров
            bool isHeader = true; //обход заголовка таблицы
            foreach (var line in File.ReadLines("Products.csv"))
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }
                string[] fields = line.Split(','); //считываем строки по разделителю
                allProducts.Add(fields[0]); //добавляем в лист
            }
            foreach (var product in allProducts)
            {
                //products.Id = Convert.ToInt32(fields[0]);
                //products.Name = fields[1];
                //products.Price = Convert.ToInt32(fields[2]);
                //products.Count = Convert.ToInt32(fields[3]);
                //Console.WriteLine(products);
            }
        }
    }
}
