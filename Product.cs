using System;
using System.Collections.Generic;
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

        /* Menu:
         * -products
         *  add products
         *      product number
         *      price
         *      stock
         *  remove products
         *  display products
         * -orders
         *  display products
         *  Enter product number 
         *      validate: not existing, wrong input, duplicate (if yes, update the existing)
         *  Enter quantity
         *      validate: negative, out of stock, not a number
         *  add to cart
         *      (fixed-size cart) full = 20 items
         *      or
         *      keep going util user is done
         *  show grand total
         * -Checkout
         *  display cart items
         *  display grand total
         *  enter discount code
         *      (if grand total >= 5000) 10% discount
         *  display final total
         */

    }
}
