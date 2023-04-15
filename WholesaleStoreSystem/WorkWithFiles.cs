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
        public static List<Products> getDataAtFiles(string pathTableProducts)
        {
            Products products = new Products(); //экземпляр структуры
            List<string> allProducts = new List<string>(); //хранилище списка всех товаров
            List<Products> productsArray = new List<Products>();
            bool isHeader = true; //обход заголовка таблицы
            foreach (var line in File.ReadLines(pathTableProducts))
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
            foreach (var product in allProducts) //запись в экземпляр структуры
            {
                string[] tempStr = product.Split(';');
                products.Id = int.Parse(tempStr[0]);
                products.Name = tempStr[1];
                products.Price = int.Parse(tempStr[2]);
                products.Count = int.Parse(tempStr[3]);
                productsArray.Add(products);
            }
            return productsArray;
        }
        public static List<Cart> getDataAtFilesCart(string pathTableProducts)
        {
            Cart products = new Cart(); //экземпляр структуры
            List<string> allProducts = new List<string>(); //хранилище списка всех товаров
            List<Cart> productsArray = new List<Cart>();
            bool isHeader = true; //обход заголовка таблицы
            foreach (var line in File.ReadLines(pathTableProducts))
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
            foreach (var product in allProducts) //запись в экземпляр структуры
            {
                string[] tempStr = product.Split(';');
                products.Id = int.Parse(tempStr[0]);
                products.Name = tempStr[1];
                products.Price = int.Parse(tempStr[2]);
                productsArray.Add(products);
            }
            return productsArray;
        }
    }
}
