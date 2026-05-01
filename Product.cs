using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_ShoppingCartActivity
{
    internal class Product
    { //fields and properties
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }//for the new product list
        public int RemainingStock { get; set; }
        public int OriginalStock { get; set; }
        public void DisplayProduct()
        {
            int consumed = OriginalStock - RemainingStock;
            Console.WriteLine($"{Id}: {Name} ({Category}) - ₱{Price}");
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public bool DeductStock(int quantity)
        {
            if (quantity <= 0)
                return false;
            if (quantity > RemainingStock)
                return false;

            RemainingStock -= quantity;
            return true;
        }
    }
}
