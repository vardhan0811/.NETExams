using System; 
using System.Collections.Generic; 

namespace OnlineOrderProcessingAndStatusNotifications 
{ 
    public class OnlineOrderProcessingAndStatusNotifications 
    { 
        static Dictionary<int, Product> products = new Dictionary<int, Product>(); // Dictionary to hold products
        static List<Customer> customers = new List<Customer>(); // List to hold customers
        static Dictionary<int, Order> orders = new Dictionary<int, Order>(); // Dictionary to hold orders

        /// <summary>
        /// Runs the interactive order processing and notification logic.
        /// </summary>
        public static void Run() 
        { 
            products[1] = new Product(1, "Laptop", 60000, "Electronics"); // Add Laptop product
            products[2] = new Product(2, "Phone", 30000, "Electronics"); // Add Phone product
            products[3] = new Product(3, "Book", 500, "Education"); // Add Book product
            products[4] = new Product(4, "Headphones", 2000, "Accessories"); // Add Headphones product
            products[5] = new Product(5, "Keyboard", 1500, "Accessories"); // Add Keyboard product

            customers.Add(new Customer(1, "Elijah", "elijah@mail.com")); // Add customer Elijah
            customers.Add(new Customer(2, "Klaus", "klaus@mail.com")); // Add customer Klaus
            customers.Add(new Customer(3, "Kol", "kol@mail.com")); // Add customer Kol

            Order o1 = new Order(101, customers[0]); // Create order o1 for Elijah
            o1.AddItem(new OrderItem(products[1], 1)); // Add Laptop to o1
            o1.AddItem(new OrderItem(products[3], 2)); // Add Book to o1

            Order o2 = new Order(102, customers[1]); // Create order o2 for Klaus
            o2.AddItem(new OrderItem(products[2], 3)); // Add Phone to o2

            Order o3 = new Order(103, customers[2]); // Create order o3 for Kol
            o3.AddItem(new OrderItem(products[4], 1)); // Add Headphones to o3
            o3.AddItem(new OrderItem(products[5], 1)); // Add Keyboard to o3

            orders[o1.OrderId] = o1; // Add o1 to orders
            orders[o2.OrderId] = o2; // Add o2 to orders
            orders[o3.OrderId] = o3; // Add o3 to orders

            Console.WriteLine("Choose data entry mode:"); // Prompt for data entry mode
            Console.WriteLine("1. Use pre_existing values"); // Option 1: use pre-existing values
            Console.WriteLine("2. Enter new values"); // Option 2: enter new values
            Console.Write("Enter choice (1 or 2): "); // Prompt for user choice
            string? mode = Console.ReadLine(); // Read user input for mode

            if (mode == "2") // If user chooses to enter new values
            { 
                Console.WriteLine("Enter number of products:"); 
                int prodCount = int.Parse(Console.ReadLine() ?? "0"); 
                for (int i = 0; i < prodCount; i++) // Loop for each product
                { 
                    Console.WriteLine($"Product #{i+1}:"); // Print product number
                    int id; 
                    while (true) // Loop until unique ID is entered
                    { 
                        Console.Write("ID: "); 
                        id = int.Parse(Console.ReadLine() ?? "0");
                        if (products.ContainsKey(id)) // If ID already exists
                        { 
                            Console.WriteLine("Product ID already exists. Please enter a unique ID."); // Print error
                        }
                        else 
                        { 
                            break; 
                        } 
                    } 
                    Console.Write("Name: ");
                    string name = Console.ReadLine() ?? ""; 
                    Console.Write("Price: "); 
                    decimal price = decimal.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Category: "); 
                    string category = Console.ReadLine() ?? ""; 
                    products[id] = new Product(id, name, (double)price, category); // Add product to dictionary
                } 

                Console.WriteLine("Enter number of customers:"); 
                int customerCount = int.Parse(Console.ReadLine() ?? "0"); 
                for (int i = 0; i < customerCount; i++) // Loop for each customer
                {
                    Console.WriteLine($"Customer #{i+1}:"); // Print customer number
                    int id; 
                    while (true) 
                    { 
                        Console.Write("ID: "); 
                        id = int.Parse(Console.ReadLine() ?? "0"); 
                        if (customers.Exists(c => c.CustomerId == id)) // If ID already exists
                        { 
                            Console.WriteLine("Customer ID already exists. Please enter a unique ID."); // Print error
                        }
                        else 
                        { 
                            break; 
                        } 
                    } 
                    Console.Write("Name: ");
                    string name = Console.ReadLine() ?? ""; 
                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? ""; 
                    customers.Add(new Customer(id, name, email)); // Add customer to list
                } 

                Console.WriteLine("Enter number of orders:"); 
                int orderCount = int.Parse(Console.ReadLine() ?? "0");
                for (int i = 0; i < orderCount; i++) 
                { 
                    Console.WriteLine($"Order #{i+1}:"); // Print order number
                    int orderId; 
                    while (true) // Loop until unique ID is entered
                    {
                        Console.Write("Order ID: "); 
                        orderId = int.Parse(Console.ReadLine() ?? "0"); 
                        if (orders.ContainsKey(orderId)) // If ID already exists
                        { 
                            Console.WriteLine("Order ID already exists. Please enter a unique ID."); // Print error
                        } 
                        else 
                        { 
                            break; 
                        } 
                    } 
                    Console.WriteLine("Available customers:"); // Print available customers
                    foreach (var cust in customers) 
                    { 
                        Console.WriteLine($"  ID: {cust.CustomerId}, Name: {cust.Name}"); 
                    } 
                    Console.Write("Customer ID: "); 
                    int customerId = int.Parse(Console.ReadLine() ?? "0");
                    var customer = customers.Find(c => c.CustomerId == customerId); 
                    if (customer == null) // If customer not found
                    { 
                        Console.WriteLine("Customer not found. Skipping order."); // Print error
                        continue; // Skip this order
                    } 
                    var order = new Order(orderId, customer); // Create new order
                    Console.Write("Number of items: "); 
                    int itemCount = int.Parse(Console.ReadLine() ?? "0"); 
                    for (int j = 0; j < itemCount; j++) 
                    { 
                        Console.WriteLine($"  Item #{j+1}:"); 
                        int prodId;
                        while (true) 
                        { 
                            Console.Write("  Product ID: "); 
                            prodId = int.Parse(Console.ReadLine() ?? "0"); 
                            if (!products.ContainsKey(prodId)) 
                            { 
                                Console.WriteLine("  Product not found. Please enter a valid Product ID."); // Print error
                            } 
                            else 
                            { 
                                break; 
                            } 
                        } 
                        Console.Write("  Quantity: "); 
                        int qty = int.Parse(Console.ReadLine() ?? "0"); 
                        order.AddItem(new OrderItem(products[prodId], qty)); // Add item to order
                    } 
                    orders[order.OrderId] = order; // Add order to dictionary
                }
                Console.WriteLine("Orders created successfully.\n"); 
            } 

            // Service + delegate subscription
            OrderService service = new OrderService(); // Create OrderService instance
            service.OnStatusChanged += Notifications.CustomerNotification; // Subscribe customer notification
            service.OnStatusChanged += Notifications.LogisticsNotification; // Subscribe logistics notification

            // Status workflow
            foreach (var order in orders.Values) 
            { 
                service.ChangeOrderStatus(order, OrderStatus.Paid); // Change status to Paid
                service.ChangeOrderStatus(order, OrderStatus.Packed); // Change status to Packed
                service.ChangeOrderStatus(order, OrderStatus.Shipped); // Change status to Shipped
                service.ChangeOrderStatus(order, OrderStatus.Delivered); // Change status to Delivered
            } 

            Console.WriteLine("\n--- ORDER SUMMARY ---"); 
            foreach (var order in orders.Values) 
            { 
                Console.WriteLine($"OrderId: {order.OrderId}, Customer: {order.Customer.Name}"); // Print order and customer
                Console.WriteLine($"Total: {order.CalculateTotal()}, Status: {order.CurrentStatus}"); // Print total and status

                Console.WriteLine("Status History:"); 
                foreach (var log in order.StatusHistory) 
                { 
                    Console.WriteLine($"  {log.OldStatus} → {log.NewStatus} at {log.ChangedAt}"); // Print status change log
                } 
                Console.WriteLine(); 
            }
        } 
    }
} 