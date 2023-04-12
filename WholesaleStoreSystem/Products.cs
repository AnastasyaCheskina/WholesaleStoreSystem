using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholesaleStoreSystem
{
    struct Products
    {
        int id;
        string name;
        double price;
        int count;
        public Products(int id, string name, double price, int count)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.count = count;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public int Count { get => count; set => count = value; }

        public override string ToString()
        {
            return "Наименование: " + name + ", цена: " + count.ToString() + ", id товара: " + id.ToString();
        }
    }

}
