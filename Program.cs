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
class Order
{
    public int ReceiptNumber;
    public double FinalTotal;
}

partial class Program
{
    static void SearchProduct(Product[] products)
    {
        Console.Write("\nEnter product name to search: ");
        string keyword = Console.ReadLine().ToLower();

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

        if (!found)
        {
            Console.WriteLine("No matching product found.");
        }

        Pause();
    }
    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    static string GetYesOrNo(string message)//validation for yes or no input
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
    static void Main()
    {
        Order[] orders = new Order[50];
        int orderCount = 0;
        int receiptCounter = 1;

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
        };

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
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    while (true)
                    {
                        CartItem[] cart = new CartItem[50];
                        int cartCount = 0;
                        bool addingItems = true;
                        while (addingItems)
                        {
                            Console.Clear();
                            Console.WriteLine("=== STORE MENU ===");// products
                            string[] categories = { "Food", "Electronics", "Clothing" };
                            foreach (string category in categories)
                            {
                                Console.WriteLine($"\n===== {category.ToUpper()} =====");

                                foreach (var p in products)
                                {
                                    if (p.Category == category)
                                    {
                                        p.DisplayProduct();
                                    }
                                }
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
                            string input = GetYesOrNo("\nDo you want to add more items? (Y/N): ");

                            if (input == "N")
                            {
                                addingItems = false;
                            }
                        }
                        bool isCheckingOut = false;
                        while (!isCheckingOut)
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
                                    Console.WriteLine($"{i + 1}. {cart[i].Product.Name} x {cart[i].Quantity} = ₱{cart[i].SubTotal}");
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
                            string cartChoice = Console.ReadLine();
                            switch (cartChoice)
                            {
                                case "1":
                                    Console.Write("Enter item number to remove: ");
                                    if (int.TryParse(Console.ReadLine(), out int removeIndex) &&
                                        removeIndex > 0 && removeIndex <= cartCount)
                                    {
                                        removeIndex--;

                                        // restore stock
                                        cart[removeIndex].Product.RemainingStock += cart[removeIndex].Quantity;

                                        // shift items
                                        for (int i = removeIndex; i < cartCount - 1; i++)
                                        {
                                            cart[i] = cart[i + 1];
                                        }

                                        cartCount--;
                                        Console.WriteLine("Item removed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input.");
                                    }
                                    Pause();
                                    break;

                                case "2":
                                    Console.Write("Enter item number: ");
                                    if (int.TryParse(Console.ReadLine(), out int updateIndex) &&
                                        updateIndex > 0 && updateIndex <= cartCount)
                                    {
                                        updateIndex--;

                                        Console.Write("Enter new quantity: ");
                                        if (int.TryParse(Console.ReadLine(), out int newQty) && newQty > 0)
                                        {
                                            int difference = newQty - cart[updateIndex].Quantity;

                                            if (difference > 0)
                                            {
                                                if (cart[updateIndex].Product.RemainingStock >= difference)
                                                {
                                                    cart[updateIndex].Product.RemainingStock -= difference;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Not enough stock.");
                                                    Pause();
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                cart[updateIndex].Product.RemainingStock += Math.Abs(difference);
                                            }

                                            cart[updateIndex].Quantity = newQty;
                                            cart[updateIndex].UpdateSubtotal();
                                            Console.WriteLine("Quantity updated.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid quantity.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input.");
                                    }
                                    Pause();
                                    break;

                                case "3":
                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        cart[i].Product.RemainingStock += cart[i].Quantity;
                                    }

                                    cartCount = 0;
                                    Console.WriteLine("Cart cleared.");
                                    Pause();
                                    break;

                                case "4":
                                    isCheckingOut = true;
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice.");
                                    Pause();
                                    break;
                            }
                        }
                        Console.WriteLine("=== RECEIPT ===");
                        Console.WriteLine($"Receipt No: {receiptCounter:D4}");
                        Console.WriteLine($"Date: {DateTime.Now}");
                        receiptCounter++;
                        double grandTotal = 0;
                        double discount = 0;
                        double finalTotal = 0;
                        for (int i = 0; i < cartCount; i++)
                        {
                            Console.WriteLine($"{cart[i].Product.Name} x {cart[i].Quantity} = ₱{cart[i].SubTotal}");
                            grandTotal += cart[i].SubTotal;
                        }
                        Console.WriteLine($"\nTotal: ₱{grandTotal}");
                        if (grandTotal >= 5000)
                        {
                            discount = grandTotal * 0.10;
                            Console.WriteLine($"Discount (10%): ₱{discount}");
                        }
                        finalTotal = grandTotal - discount;
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

                        double change = payment - finalTotal;
                        Console.WriteLine($"Change: ₱{change}");
                        Console.WriteLine("\nThank you for shopping with us!");
                        orders[orderCount++] = new Order
                        {
                            ReceiptNumber = receiptCounter - 1,
                            FinalTotal = finalTotal // will be updated after calculating total
                        };
                    }
                    Console.WriteLine("\nTransaction complete. Returning to main menu...");
                    Pause();
                    break;


                case "2":
                    Console.WriteLine("\n=== ORDER HISTORY ===");
                    for (int i = 0; i < orderCount; i++)
                    {
                        Console.WriteLine($"Receipt #{orders[i].ReceiptNumber:D4} - ₱{orders[i].FinalTotal}");
                    }
                    Pause();
                    break;

                case "3":
                    Console.WriteLine("\n=== UPDATED STOCK ===");
                    foreach (var p in products)
                        p.DisplayProduct();
                    Console.WriteLine("\nLOW STOCK ALERT:");
                    foreach (var p in products)
                    {
                        if (p.RemainingStock <= 5)
                        {
                            Console.WriteLine($"{p.Name} has only {p.RemainingStock} left.");
                        }
                    }
                    Pause();
                    break;

                case "4":
                    SearchProduct(products);
                    break;

                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    Pause();
                    break;


            }
        }
    }
}

