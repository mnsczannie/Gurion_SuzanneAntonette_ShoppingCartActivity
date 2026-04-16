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

partial class Program
{
    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Product[] products = new Product[]//array of products
            {
            new Product { Id = 1, Name = "Desktop", Price = 25000.00, RemainingStock = 10, OriginalStock = 10 },
            new Product { Id = 2, Name = "Laptop", Price = 30000.00, RemainingStock = 20, OriginalStock = 20 },
            new Product { Id = 3, Name = "Smartphone", Price = 15000.00, RemainingStock = 30, OriginalStock = 30 },
            new Product { Id = 4, Name = "Smartwatch", Price = 1500.00, RemainingStock = 45, OriginalStock = 45 },
            new Product { Id = 5, Name = "Tablet", Price = 12000.00, RemainingStock = 25, OriginalStock = 25 },
            new Product { Id = 6, Name = "Earphones", Price = 800.00, RemainingStock = 50, OriginalStock = 50 },
            new Product { Id = 7, Name = "Headset", Price = 2000.00, RemainingStock = 35, OriginalStock = 35 },
            new Product { Id = 8, Name = "Keyboard", Price = 1500.00, RemainingStock = 35, OriginalStock = 35 },
            new Product { Id = 9, Name = "Mouse", Price = 500.00, RemainingStock = 45, OriginalStock = 45 },
            new Product { Id = 10, Name = "Webcam", Price = 1800.00, RemainingStock = 50, OriginalStock = 50 }
    };
        static string GetYesOrNo(string message)
        {
            string input;
            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine().Trim().ToUpper();

                if (input == "Y" || input == "N")
                    return input;

                Console.WriteLine("Invalid input. Please enter Y or N only.");
            }
        }
        string continueTransaction = "Y";

        while (continueTransaction.ToUpper() == "Y")
        {
            CartItem[] cart = new CartItem[20];
            int cartCount = 0;
            string choice = "Y";
            while (choice.ToUpper() == "Y")
            {
                Console.Clear();
                Console.WriteLine("=== STORE MENU ===");
                foreach (var p in products)
                {
                    Console.WriteLine($"{p.Id}. {p.Name} - ₱{p.Price}");
                }
                Console.WriteLine("\n=== ORDERING ===");
                double tempTotal = 0;
                for (int i = 0; i < cartCount; i++)
                {
                    Console.WriteLine($"- {cart[i].Product.Name} x {cart[i].Quantity} ({cart[i].SubTotal})");
                    tempTotal += cart[i].SubTotal;
                }
                Console.WriteLine($"\nCurrent Total: ₱{tempTotal}");
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
                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
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
                Console.WriteLine("Item added to cart.");
                choice = GetYesOrNo("\nDo you want to add more items? (Y/N): ");
            }
            // checkout
            string confirm = GetYesOrNo("\nAre you sure you want to checkout? (Y/N): ");
            if (confirm.ToUpper() != "Y")
            {
                Console.WriteLine("Checkout cancelled!");
                return;
            }
            else
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
            continueTransaction = GetYesOrNo("\nStart another transaction? (Y/N): ");

            Console.Clear();
        }
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