using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholesaleStoreSystem
{
    [Serializable]
    internal class Products
    {
        int id;
        string name;
        double price;
        int count;
        //public Products(int id,string name, double price, int count)
        //{
        //    this.Id = id;
        //    this.name = name;
        //    this.price = price;
        //    this.count = count;
        //}
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public int Count { get => count; set => count = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return "Наименование: "+name+" "+", цена: "+count.ToString()+", id товара: "+id.ToString();
        }
    }
}
