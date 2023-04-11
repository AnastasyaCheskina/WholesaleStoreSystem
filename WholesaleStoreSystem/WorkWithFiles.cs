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
            List<Products> productsArray = new List<Products>();
            bool isHeader = true; //обход заголовка таблицы
            foreach (var line in File.ReadLines("Products.csv"))
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }
                string[] fields = line.Split(','); //считываем строки по разделителю
                for (int i = 0; i < fields.Length; i++)
                {
                    string value = fields[i];
                    allProducts.Add(value); //добавляем в лист
                }
            }
            foreach (var product in allProducts)
            {
                string[] tempStr = product.Split(';');
                products.Id = int.Parse(tempStr[0]);
                products.Name = tempStr[1];
                products.Price = int.Parse(tempStr[2]);
                products.Count = int.Parse(tempStr[3]);
                productsArray.Add(products);
            }
            foreach (var product in productsArray)
            {
                Console.WriteLine(product);
            }
        }
    }
}
