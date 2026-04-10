using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_ShoppingCartActivity
{
    internal class Product
    {

        /* fields/properties
         *  ID
         *  Name
         *  Price
         *  Remaining Stock
         */
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int RemainingStock { get; set; }

        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name} - ${Price}");
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public bool HasEnoughStock(int quantity)
        {
            return RemainingStock >= quantity;
        }

        public bool DeductStock(int quantity)
        {
            if (quantity > RemainingStock)
                return false;

            RemainingStock -= quantity;
            return true;
        }



        
    }
}
