/* Store Menu:
         * -products[not sure if i would still make this]
         *  add products
         *      product number
         *      price
         *      stock
         *  remove products
         *  display products
         * -orders
         *  display products (check)
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
using System.Runtime.InteropServices;

class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public double SubTotal { get; set; }

    public void UpdateSubtotal()
    {
        SubTotal = Product.Price * Quantity;
    }
}

class Program
{
    static void Main()
    {
        Product[] products = new Product[]
            {
            new Product { Id = 101, Name = "Desktop", Price = 25000.00, RemainingStock = 10 },
            new Product { Id = 102, Name = "Laptop", Price = 30000.00, RemainingStock = 20 },
            new Product { Id = 103, Name = "Smartphone", Price = 15000.00, RemainingStock = 30 },
            new Product { Id = 104, Name = "Smartwatch", Price = 1500.00, RemainingStock = 45 },
            new Product { Id = 105, Name = "Tablet", Price = 12000.00, RemainingStock = 25 },
            new Product { Id = 201, Name = "Earphones", Price = 800.00, RemainingStock = 50 },
            new Product { Id = 202, Name = "Headset", Price = 2000.00, RemainingStock = 35 },
            new Product { Id = 203, Name = "Keyboard", Price = 1500.00, RemainingStock = 35 },
            new Product { Id = 204, Name = "Mouse", Price = 500.00, RemainingStock = 45 },
            new Product { Id = 205, Name = "Webcam", Price = 1800.00, RemainingStock = 50 }
    };
       static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        CartItem[] cart = new CartItem[20];
        int cartCount = 0;
        string choice = "Y";
        while (choice.ToUpper() == "Y")
        {
            Console.Clear();
            Console.WriteLine("=== STORE MENU ===");
            foreach (var p in products)
                p.DisplayProduct();
            Console.Write("\nEnter product number: ");
            if (!int.TryParse(Console.ReadLine(), out int productNum) ||
                productNum < 1 || productNum > products.Length)
            {
                Console.WriteLine("Invalid product number.");
                Pause();
                continue;
            }
            Product selected = products[productNum - 1];
            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("Product is out of stock.");
                Pause();
                continue;
            }
            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty < 0)
            {
                Console.WriteLine("Invalid quantity.");
                Pause();
                continue;
            }
            if (!selected.DeductStock(qty))
            {
                Console.WriteLine("Not enough stock available.");
                Pause();
                continue;
            }

            bool found = false;
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Product.Id == selected.Id)
                {
                    cart[i].Quantity += qty;
                    cart[i].UpdateSubtotal();
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                if (cartCount >= cart.Length)
                {
                    Console.WriteLine("Cart is full. Cannot add more items.");
                    Pause();
                    continue;
                }
                cart[cartCount] = new CartItem
                {
                    Product = selected,
                    Quantity = qty
                };
                cart[cartCount].UpdateSubtotal();
                cartCount++;
            }
            selected.DeductStock(qty);
            Console.WriteLine("Item added to cart.");
            Console.Write("\nDo you want to add more items? (Y/N): ");
            choice = Console.ReadLine();
        }
        // checkout
        Console.Write("\nAre you sure you want to checkout? (Y/N): ");
        string confirm = Console.ReadLine();
        if (confirm.ToUpper() != "Y")
        {
            Console.WriteLine("Checkout cancelled!");
            return;
        }
        Console.Clear();
        Console.WriteLine("=== RECEIPT ===");
        double grandTotal = 0;
        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{cart[i].Product.Name} x {cart[i].Quantity} = ₱{cart[i].SubTotal}");
            grandTotal += cart[i].SubTotal;
        }
        Console.WriteLine($"\nGrand Total: ₱{grandTotal}");
        double discount = 0;
        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            Console.WriteLine($"Discount (10%): ₱{discount}");
        }
        double finalTotal = grandTotal - discount;
        Console.WriteLine($"Final Total: ₱{finalTotal}");
        Console.WriteLine("\nThank you for shopping with us!");
        Console.WriteLine("\n=== UPDATED STOCK ===");
        foreach (var p in products)
            p.DisplayProduct();
    }
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