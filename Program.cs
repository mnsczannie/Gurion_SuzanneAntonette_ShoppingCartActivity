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
        //product list
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Product[] products = new Product[]//array of products
            {
                //food
                new Product { Id = 1, Name = "Burger", Price = 120, RemainingStock = 25, OriginalStock = 25, Category = "Food" },
                new Product { Id = 2, Name = "Pizza", Price = 350, RemainingStock = 15, OriginalStock = 15, Category = "Food" },
                new Product { Id = 3, Name = "Pasta", Price = 180, RemainingStock = 20, OriginalStock = 20, Category = "Food" },
                new Product { Id = 4, Name = "Instant Noodles", Price = 25, RemainingStock = 50, OriginalStock = 50, Category = "Food" },
                new Product { Id = 5, Name = "Bread", Price = 60, RemainingStock = 30, OriginalStock = 30, Category = "Food" },
                new Product { Id = 6, Name = "Milk", Price = 90, RemainingStock = 20, OriginalStock = 20, Category = "Food" },
                new Product { Id = 7, Name = "Eggs (Dozen)", Price = 110, RemainingStock = 25, OriginalStock = 25, Category = "Food" },
                new Product { Id = 8, Name = "Rice (1kg)", Price = 55, RemainingStock = 40, OriginalStock = 40, Category = "Food" },
                
                //electronics
                new Product { Id = 9, Name = "Desktop", Price = 25000, RemainingStock = 10, OriginalStock = 10, Category = "Electronics" },
                new Product { Id = 10, Name = "Laptop", Price = 30000, RemainingStock = 8, OriginalStock = 8, Category = "Electronics" },
                new Product { Id = 11, Name = "Smartphone", Price = 15000, RemainingStock = 15, OriginalStock = 15, Category = "Electronics" },
                new Product { Id = 12, Name = "Tablet", Price = 12000, RemainingStock = 10, OriginalStock = 10, Category = "Electronics" },
                new Product { Id = 13, Name = "Smartwatch", Price = 2500, RemainingStock = 20, OriginalStock = 20, Category = "Electronics" },
                new Product { Id = 14, Name = "Earphones", Price = 800, RemainingStock = 30, OriginalStock = 30, Category = "Electronics" },
                new Product { Id = 15, Name = "Keyboard", Price = 1500, RemainingStock = 20, OriginalStock = 20, Category = "Electronics" },
                new Product { Id = 16, Name = "Mouse", Price = 500, RemainingStock = 25, OriginalStock = 25, Category = "Electronics" },
    
                //clothing
                new Product { Id = 17, Name = "T-shirt", Price = 300, RemainingStock = 40, OriginalStock = 40, Category = "Clothing" },
                new Product { Id = 18, Name = "Jeans", Price = 900, RemainingStock = 20, OriginalStock = 20, Category = "Clothing" },
                new Product { Id = 19, Name = "Jacket", Price = 1500, RemainingStock = 15, OriginalStock = 15, Category = "Clothing" },
                new Product { Id = 20, Name = "Hoodie", Price = 1200, RemainingStock = 18, OriginalStock = 18, Category = "Clothing" },
                new Product { Id = 21, Name = "Shorts", Price = 400, RemainingStock = 25, OriginalStock = 25, Category = "Clothing" },
                new Product { Id = 22, Name = "Dress", Price = 1000, RemainingStock = 12, OriginalStock = 12, Category = "Clothing" },
                new Product { Id = 23, Name = "Socks", Price = 100, RemainingStock = 50, OriginalStock = 50, Category = "Clothing" },
                new Product { Id = 24, Name = "Sweater", Price = 2500, RemainingStock = 10, OriginalStock = 10, Category = "Clothing" }
            }
    };

        static string GetYesOrNo(string message)
        {
            string input;
            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine().Trim().ToUpper();

                if (input = "Y" || input == "N")
                    return input;

                Console.WriteLine("Invalid input. Please enter Y or N only.");
            }
        }
        string continueTransaction = "Y";

        while (continueTransaction.ToUpper() == "Y")
        {
            CartItem[] cart = new CartItem[5];
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
            else//receipt
                Console.Clear();

            Console.WriteLine("\n=== CART MENU ===");
            Console.WriteLine("1. View Cart");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. Update Quantity");
            Console.WriteLine("4. Clear Cart");
            Console.WriteLine("5. Checkout");

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