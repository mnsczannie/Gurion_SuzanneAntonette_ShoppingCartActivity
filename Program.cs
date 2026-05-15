using _1_ShoppingCartActivity;

// ── CartItem ────────────────────────────────────────────────────────────────
class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public double SubTotal { get; private set; }

    public void UpdateSubtotal() => SubTotal = Product.Price * Quantity;

    public override string ToString() =>
        $"{Product.Name} x {Quantity} = ₱{SubTotal}";
}

// ── Order ───────────────────────────────────────────────────────────────────
class Order
{
    public int ReceiptNumber { get; set; }
    public double FinalTotal { get; set; }

    public override string ToString() =>
        $"Receipt #{ReceiptNumber:D4} - ₱{FinalTotal}";
}

// ── ConsoleHelper ────────────────────────────────────────────────────────────
static class ConsoleHelper
{
    public static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public static string GetYesOrNo(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
            if (input == "Y" || input == "N") return input;
            Console.WriteLine("Invalid input. Please enter Y or N only.");
        }
    }

    public static void SearchProduct(Product[] products)
    {
        Console.Write("\nEnter product name to search: ");
        string keyword = Console.ReadLine()?.ToLower() ?? "";
        bool found = false;

        Console.WriteLine("\n=== SEARCH RESULTS ===");
        foreach (var p in products)
        {
            if (p.Name.ToLower().Contains(keyword))
            {
                p.DisplayProduct();
                found = true;
            }
        }

        if (!found) Console.WriteLine("No matching product found.");
        Pause();
    }
}

// ── ShoppingCartApp ──────────────────────────────────────────────────────────
class ShoppingCartApp
{
    private readonly Product[] _products;
    private readonly Order[] _orders = new Order[50];
    private int _orderCount = 0;
    private int _receiptCounter = 1;

    public ShoppingCartApp()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        _products = InitProducts();
    }

    // ── Entry point ──────────────────────────────────────────────────────────
    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("1. Start New Transaction");
            Console.WriteLine("2. View Order History");
            Console.WriteLine("3. View Stock Report");
            Console.WriteLine("4. Search Product");
            Console.WriteLine("5. Exit");
            Console.Write("\nChoose option: ");

            switch (Console.ReadLine())
            {
                case "1": StartTransaction(); break;
                case "2": ViewHistory(); break;
                case "3": ViewStock(); break;
                case "4": ConsoleHelper.SearchProduct(_products); break;
                case "5": exit = true; break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    // ── Transaction flow ─────────────────────────────────────────────────────
    private void StartTransaction()
    {
        CartItem[] cart = new CartItem[50];
        int cartCount = 0;

        // --- Add items phase ---
        bool addingItems = true;
        while (addingItems)
        {
            Console.Clear();
            DisplayMenu();
            DisplayCartSummary(cart, cartCount);

            Console.Write("\nEnter product number: ");
            if (!int.TryParse(Console.ReadLine(), out int productNum) ||
                productNum < 1 || productNum > _products.Length)
            {
                Console.WriteLine("Invalid product number.");
                ConsoleHelper.Pause();
                continue;
            }

            Product selected = _products[productNum - 1];

            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("Product is out of stock.");
                ConsoleHelper.Pause();
                continue;
            }

            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                ConsoleHelper.Pause();
                continue;
            }

            if (!selected.DeductStock(qty))
            {
                Console.WriteLine("Not enough stock available.");
                ConsoleHelper.Pause();
                continue;
            }

            cartCount = AddToCart(cart, cartCount, selected, qty);
            Console.WriteLine("Item added to cart.");

            if (ConsoleHelper.GetYesOrNo("\nDo you want to add more items? (Y/N): ") == "N")
                addingItems = false;
        }

        // --- Cart management phase ---
        ManageCart(cart, ref cartCount);

        // --- Checkout ---
        Checkout(cart, cartCount);

        Console.WriteLine("\nTransaction complete. Returning to main menu...");
        ConsoleHelper.Pause();
    }

    private void ManageCart(CartItem[] cart, ref int cartCount)
    {
        bool done = false;
        while (!done)
        {
            Console.Clear();
            Console.WriteLine("=== CURRENT CART ===");
            double total = 0;

            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty.");
            }
            else
            {
                for (int i = 0; i < cartCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {cart[i]}");
                    total += cart[i].SubTotal;
                }
            }

            Console.WriteLine($"\nCURRENT TOTAL: ₱{total}");
            Console.WriteLine("\n=== CART MENU ===");
            Console.WriteLine("1. Remove Item");
            Console.WriteLine("2. Update Quantity");
            Console.WriteLine("3. Clear Cart");
            Console.WriteLine("4. Checkout");
            Console.Write("\nChoose option: ");

            switch (Console.ReadLine())
            {
                case "1": RemoveItem(cart, ref cartCount); break;
                case "2": UpdateQuantity(cart, cartCount); break;
                case "3": ClearCart(cart, ref cartCount); break;
                case "4": done = true; break;
                default:
                    Console.WriteLine("Invalid choice.");
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private void RemoveItem(CartItem[] cart, ref int cartCount)
    {
        Console.Write("Enter item number to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > cartCount)
        {
            Console.WriteLine("Invalid input.");
        }
        else
        {
            idx--;
            cart[idx].Product.RemainingStock += cart[idx].Quantity;
            for (int i = idx; i < cartCount - 1; i++) cart[i] = cart[i + 1];
            cartCount--;
            Console.WriteLine("Item removed.");
        }
        ConsoleHelper.Pause();
    }

    private void UpdateQuantity(CartItem[] cart, int cartCount)
    {
        Console.Write("Enter item number: ");
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > cartCount)
        {
            Console.WriteLine("Invalid input.");
            ConsoleHelper.Pause();
            return;
        }

        idx--;
        Console.Write("Enter new quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int newQty) || newQty <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            ConsoleHelper.Pause();
            return;
        }

        int difference = newQty - cart[idx].Quantity;
        if (difference > 0 && cart[idx].Product.RemainingStock < difference)
        {
            Console.WriteLine("Not enough stock.");
            ConsoleHelper.Pause();
            return;
        }

        cart[idx].Product.RemainingStock -= difference;   // works for both + and -
        cart[idx].Quantity = newQty;
        cart[idx].UpdateSubtotal();
        Console.WriteLine("Quantity updated.");
        ConsoleHelper.Pause();
    }

    private void ClearCart(CartItem[] cart, ref int cartCount)
    {
        for (int i = 0; i < cartCount; i++)
            cart[i].Product.RemainingStock += cart[i].Quantity;
        cartCount = 0;
        Console.WriteLine("Cart cleared.");
        ConsoleHelper.Pause();
    }

    private void Checkout(CartItem[] cart, int cartCount)
    {
        Console.WriteLine("=== RECEIPT ===");
        Console.WriteLine($"Receipt No: {_receiptCounter:D4}");
        Console.WriteLine($"Date: {DateTime.Now}");

        double grandTotal = 0;
        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine(cart[i]);
            grandTotal += cart[i].SubTotal;
        }

        double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
        double finalTotal = grandTotal - discount;

        Console.WriteLine($"\nTotal: ₱{grandTotal}");
        if (discount > 0) Console.WriteLine($"Discount (10%): ₱{discount}");
        Console.WriteLine($"Final Total: ₱{finalTotal}");

        double payment;
        while (true)
        {
            Console.Write("Enter payment: ");
            if (!double.TryParse(Console.ReadLine(), out payment))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }
            if (payment < finalTotal)
            {
                Console.WriteLine("Insufficient payment.");
                continue;
            }
            break;
        }

        Console.WriteLine($"Change: ₱{payment - finalTotal}");
        Console.WriteLine("\nThank you for shopping with us!");

        _orders[_orderCount++] = new Order
        {
            ReceiptNumber = _receiptCounter++,
            FinalTotal = finalTotal
        };
    }

    // ── History & Stock ──────────────────────────────────────────────────────
    private void ViewHistory()
    {
        Console.WriteLine("\n=== ORDER HISTORY ===");
        for (int i = 0; i < _orderCount; i++)
            Console.WriteLine(_orders[i]);
        ConsoleHelper.Pause();
    }

    private void ViewStock()
    {
        Console.WriteLine("\n=== UPDATED STOCK ===");
        foreach (var p in _products) p.DisplayProduct();

        Console.WriteLine("\nLOW STOCK ALERT:");
        bool anyLow = false;
        foreach (var p in _products)
        {
            if (p.RemainingStock <= 5)
            {
                Console.WriteLine($"{p.Name} has only {p.RemainingStock} left.");
                anyLow = true;
            }
        }
        if (!anyLow) Console.WriteLine("All items are sufficiently stocked.");
        ConsoleHelper.Pause();
    }

    // ── Display helpers ──────────────────────────────────────────────────────
    private void DisplayMenu()
    {
        Console.WriteLine("=== STORE MENU ===");
        foreach (string category in new[] { "Food", "Electronics", "Clothing" })
        {
            Console.WriteLine($"\n===== {category.ToUpper()} =====");
            foreach (var p in _products)
                if (p.Category == category) p.DisplayProduct();
        }
    }

    private void DisplayCartSummary(CartItem[] cart, int cartCount)
    {
        Console.WriteLine("\n=== ORDERING ===");
        double tempTotal = 0;
        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"- {cart[i]}");
            tempTotal += cart[i].SubTotal;
        }
        Console.WriteLine($"\nCurrent Total: ₱{tempTotal}");
    }

    // ── Cart utility ─────────────────────────────────────────────────────────
    private int AddToCart(CartItem[] cart, int cartCount, Product selected, int qty)
    {
        for (int i = 0; i < cartCount; i++)
        {
            if (cart[i].Product.Id == selected.Id)
            {
                cart[i].Quantity += qty;
                cart[i].UpdateSubtotal();
                return cartCount;
            }
        }

        if (cartCount >= cart.Length)
        {
            Console.WriteLine("Cart is full.");
            return cartCount;
        }

        cart[cartCount] = new CartItem { Product = selected, Quantity = qty };
        cart[cartCount].UpdateSubtotal();
        return cartCount + 1;
    }

    // ── Product seed data ────────────────────────────────────────────────────
    private static Product[] InitProducts() => new Product[]
    {
        new Product { Id = 1,  Name = "Burger",         Price = 120,   RemainingStock = 25, OriginalStock = 25, Category = "Food" },
        new Product { Id = 2,  Name = "Pizza",          Price = 350,   RemainingStock = 15, OriginalStock = 15, Category = "Food" },
        new Product { Id = 3,  Name = "Pasta",          Price = 180,   RemainingStock = 20, OriginalStock = 20, Category = "Food" },
        new Product { Id = 4,  Name = "Instant Noodles",Price = 25,    RemainingStock = 50, OriginalStock = 50, Category = "Food" },
        new Product { Id = 5,  Name = "Bread",          Price = 60,    RemainingStock = 30, OriginalStock = 30, Category = "Food" },
        new Product { Id = 6,  Name = "Milk",           Price = 90,    RemainingStock = 20, OriginalStock = 20, Category = "Food" },
        new Product { Id = 7,  Name = "Eggs (Dozen)",   Price = 110,   RemainingStock = 25, OriginalStock = 25, Category = "Food" },
        new Product { Id = 8,  Name = "Rice (1kg)",     Price = 55,    RemainingStock = 40, OriginalStock = 40, Category = "Food" },
        new Product { Id = 9,  Name = "Desktop",        Price = 25000, RemainingStock = 10, OriginalStock = 10, Category = "Electronics" },
        new Product { Id = 10, Name = "Laptop",         Price = 30000, RemainingStock = 8,  OriginalStock = 8,  Category = "Electronics" },
        new Product { Id = 11, Name = "Smartphone",     Price = 15000, RemainingStock = 15, OriginalStock = 15, Category = "Electronics" },
        new Product { Id = 12, Name = "Tablet",         Price = 12000, RemainingStock = 10, OriginalStock = 10, Category = "Electronics" },
        new Product { Id = 13, Name = "Smartwatch",     Price = 2500,  RemainingStock = 20, OriginalStock = 20, Category = "Electronics" },
        new Product { Id = 14, Name = "Earphones",      Price = 800,   RemainingStock = 30, OriginalStock = 30, Category = "Electronics" },
        new Product { Id = 15, Name = "Keyboard",       Price = 1500,  RemainingStock = 20, OriginalStock = 20, Category = "Electronics" },
        new Product { Id = 16, Name = "Mouse",          Price = 500,   RemainingStock = 25, OriginalStock = 25, Category = "Electronics" },
        new Product { Id = 17, Name = "T-shirt",        Price = 300,   RemainingStock = 40, OriginalStock = 40, Category = "Clothing" },
        new Product { Id = 18, Name = "Jeans",          Price = 900,   RemainingStock = 20, OriginalStock = 20, Category = "Clothing" },
        new Product { Id = 19, Name = "Jacket",         Price = 1500,  RemainingStock = 15, OriginalStock = 15, Category = "Clothing" },
        new Product { Id = 20, Name = "Hoodie",         Price = 1200,  RemainingStock = 18, OriginalStock = 18, Category = "Clothing" },
        new Product { Id = 21, Name = "Shorts",         Price = 400,   RemainingStock = 25, OriginalStock = 25, Category = "Clothing" },
        new Product { Id = 22, Name = "Dress",          Price = 1000,  RemainingStock = 12, OriginalStock = 12, Category = "Clothing" },
        new Product { Id = 23, Name = "Socks",          Price = 100,   RemainingStock = 50, OriginalStock = 50, Category = "Clothing" },
        new Product { Id = 24, Name = "Sweater",        Price = 2500,  RemainingStock = 10, OriginalStock = 10, Category = "Clothing" },
    };
}

// ── Program entry ────────────────────────────────────────────────────────────
partial class Program
{
    static void Main() => new ShoppingCartApp().Run();
}