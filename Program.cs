/* Store Menu:
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

using _1_ShoppingCartActivity;

static void Main()
{
    Product[] products = new Product[]
        {
        new Product { Id = 101, Name = "Desktop", Price = 999.99, RemainingStock = 10 },
        new Product { Id = 102, Name = "Laptop", Price = 499.99, RemainingStock = 20 },
        new Product { Id = 103, Name = "Smartphone", Price = 199.99, RemainingStock = 15 },
        new Product { Id = 104, Name = "Smartwatch", Price = 299.99, RemainingStock = 5 },
        new Product { Id = 105, Name = "Tablet", Price = 399.99, RemainingStock = 8 }
    };
}
/*Need:
 validation messages:
    - added/removed product
    - invalid product number
    - invalid quantity
    - added to cart
            - out of stock
    - "are you sure you want to checkout? (y/n)"
    - "thank you for shopping with us!"
*/