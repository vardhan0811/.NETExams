
using System; 

namespace OnlineOrderProcessingAndStatusNotifications 
{ 
    public enum OrderStatus // OrderStatus enum
    { 
        Created, // Order has been created
        Paid, // Order has been paid
        Packed, // Order has been packed
        Shipped, // Order has been shipped
        Delivered, // Order has been delivered
        Cancelled // Order has been cancelled
    } 

    /// <summary>
    /// Represents a product in the system.
    /// </summary>
    public class Product 
    { 
        public int ProductId { get; } // Product ID property
        public string Name { get; } // Product name property
        public double Price { get; } // Product price property
        public string Category { get; } // Product category property

        /// <summary>
        /// Initializes a new instance of the Product class.
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="name">Product name</param>
        /// <param name="price">Product price</param>
        /// <param name="category">Product category</param>
        public Product(int id, string name, double price, string category) // Product constructor
        { 
            ProductId = id; // Set product ID
            Name = name; // Set product name
            Price = price; // Set product price
            Category = category; // Set product category
        } 
    } // End of Product class

    /// <summary>
    /// Represents a customer in the system.
    /// </summary>
    public class Customer // Defines the Customer class
    { // Start of Customer class
        public int CustomerId { get; } // Customer ID property
        public string Name { get; } // Customer name property
        public string Email { get; } // Customer email property

        /// <summary>
        /// Initializes a new instance of the Customer class.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="name">Customer name</param>
        /// <param name="email">Customer email</param>
        public Customer(int id, string name, string email) // Customer constructor
        { // Start of constructor
            CustomerId = id; // Set customer ID
            Name = name; // Set customer name
            Email = email; // Set customer email
        } // End of constructor
    } // End of Customer class

    /// <summary>
    /// Represents an item in an order.
    /// </summary>
    public class OrderItem // Defines the OrderItem class
    { // Start of OrderItem class
        public Product Product { get; } // Product property
        public int Quantity { get; } // Quantity property

        /// <summary>
        /// Initializes a new instance of the OrderItem class.
        /// </summary>
        /// <param name="product">Product object</param>
        /// <param name="quantity">Quantity of the product</param>
        public OrderItem(Product product, int quantity) // OrderItem constructor
        { // Start of constructor
            Product = product; // Set product
            Quantity = quantity; // Set quantity
        } // End of constructor

        /// <summary>
        /// Calculates the total price for this order item.
        /// </summary>
        /// <returns>Total price for the item.</returns>
        public double GetItemTotal() // Method to get item total
        { // Start of GetItemTotal
            return Product.Price * Quantity; // Calculate and return total
        } // End of GetItemTotal
    } // End of OrderItem class

    /// <summary>
    /// Represents a log entry for an order status change.
    /// </summary>
    public class OrderStatusLog // Defines the OrderStatusLog class
    { // Start of OrderStatusLog class
        public OrderStatus OldStatus { get; } // Old status property
        public OrderStatus NewStatus { get; } // New status property
        public DateTime ChangedAt { get; } // Date and time of status change

        /// <summary>
        /// Initializes a new instance of the OrderStatusLog class.
        /// </summary>
        /// <param name="oldStatus">Previous order status</param>
        /// <param name="newStatus">New order status</param>
        public OrderStatusLog(OrderStatus oldStatus, OrderStatus newStatus) // OrderStatusLog constructor
        { // Start of constructor
            OldStatus = oldStatus; // Set old status
            NewStatus = newStatus; // Set new status
            ChangedAt = DateTime.Now; // Set change time to now
        } // End of constructor
    } // End of OrderStatusLog class

    /// <summary>
    /// Represents an order in the system.
    /// </summary>
    public class Order // Defines the Order class
    { // Start of Order class
        public int OrderId { get; } // Order ID property
        public Customer Customer { get; } // Customer property
        public OrderStatus CurrentStatus { get; private set; } // Current status property

        private readonly List<OrderItem> _items; // List of order items
        private readonly List<OrderStatusLog> _statusHistory; // List of status history logs

        public IReadOnlyList<OrderItem> Items => _items; // Read-only list of order items
        public IReadOnlyList<OrderStatusLog> StatusHistory => _statusHistory; // Read-only list of status history

        /// <summary>
        /// Initializes a new instance of the Order class.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <param name="customer">Customer object</param>
        public Order(int orderId, Customer customer) // Order constructor
        { // Start of constructor
            OrderId = orderId; // Set order ID
            Customer = customer; // Set customer
            CurrentStatus = OrderStatus.Created; // Set initial status

            _items = new List<OrderItem>(); // Initialize items list
            _statusHistory = new List<OrderStatusLog>(); // Initialize status history list
        } // End of constructor

        /// <summary>
        /// Adds an item to the order.
        /// </summary>
        /// <param name="item">OrderItem to add</param>
        public void AddItem(OrderItem item) // Method to add item
        { // Start of AddItem
            _items.Add(item); // Add item to list
        } // End of AddItem

        /// <summary>
        /// Calculates the total price for the order.
        /// </summary>
        /// <returns>Total price for the order.</returns>
        public double CalculateTotal() // Method to calculate total
        { // Start of CalculateTotal
            double total = 0; // Initialize total
            foreach (var item in _items) // Loop through items
            { // Start of foreach
                total += item.GetItemTotal(); // Add item total
            } // End of foreach
            return total; // Return total
        } // End of CalculateTotal

        /// <summary>
        /// Updates the status of the order and logs the change.
        /// </summary>
        /// <param name="newStatus">The new status to set.</param>
        internal void UpdateStatus(OrderStatus newStatus) // Method to update status
        { // Start of UpdateStatus
            _statusHistory.Add(new OrderStatusLog(CurrentStatus, newStatus)); // Add status log
            CurrentStatus = newStatus; // Update current status
        } // End of UpdateStatus
    } // End of Order class

    /// <summary>
    /// Provides services for managing orders, including status changes and notifications.
    /// </summary>
    public class OrderService // Defines the OrderService class
    { // Start of OrderService class
        /// <summary>
        /// Delegate for status change notification.
        /// </summary>
        public Action<Order, OrderStatus, OrderStatus>? OnStatusChanged; // Delegate for status change notification

        /// <summary>
        /// Changes the status of an order, validates transitions, and notifies subscribers.
        /// </summary>
        /// <param name="order">Order to update</param>
        /// <param name="newStatus">New status to set</param>
        public void ChangeOrderStatus(Order order, OrderStatus newStatus) // Method to change order status
        { // Start of ChangeOrderStatus
            // Validation rules
            if (order.CurrentStatus == OrderStatus.Cancelled) // If order is cancelled
            { // Start of if
                Console.WriteLine("❌ Cancelled order cannot change status."); // Print error
                return; // Exit method
            } // End of if

            if (newStatus == OrderStatus.Paid && order.CurrentStatus != OrderStatus.Created || // Only allow Paid after Created
                newStatus == OrderStatus.Packed && order.CurrentStatus != OrderStatus.Paid || // Only allow Packed after Paid
                newStatus == OrderStatus.Shipped && order.CurrentStatus != OrderStatus.Packed || // Only allow Shipped after Packed
                newStatus == OrderStatus.Delivered && order.CurrentStatus != OrderStatus.Shipped) // Only allow Delivered after Shipped
            { // Start of if
                Console.WriteLine($"❌ Invalid transition: {order.CurrentStatus} → {newStatus}"); // Print error
                return; // Exit method
            } // End of if

            OrderStatus oldStatus = order.CurrentStatus; // Store old status
            order.UpdateStatus(newStatus); // Update order status

            Console.WriteLine($"✔ Order {order.OrderId} status changed: {oldStatus} → {newStatus}"); // Print status change

            // Notify subscribers
            OnStatusChanged?.Invoke(order, oldStatus, newStatus); // Invoke notification delegate
        } // End of ChangeOrderStatus
    } // End of OrderService class

    /// <summary>
    /// Provides notification methods for order status changes.
    /// </summary>
    public static class Notifications // Defines the Notifications class
    { // Start of Notifications class
        /// <summary>
        /// Notifies the customer when the order status changes.
        /// </summary>
        /// <param name="order">Order object</param>
        /// <param name="oldStatus">Previous status</param>
        /// <param name="newStatus">New status</param>
        public static void CustomerNotification(Order order, OrderStatus oldStatus, OrderStatus newStatus) // Customer notification method
        { // Start of CustomerNotification
            Console.WriteLine($"[Customer] Order {order.OrderId} is now {newStatus}"); // Print customer notification
        } // End of CustomerNotification
    
        /// <summary>
        /// Notifies the logistics team when the order is shipped.
        /// </summary>
        /// <param name="order">Order object</param>
        /// <param name="oldStatus">Previous status</param>
        /// <param name="newStatus">New status</param>
        public static void LogisticsNotification(Order order, OrderStatus oldStatus, OrderStatus newStatus) // Logistics notification method
        { // Start of LogisticsNotification
            if (newStatus == OrderStatus.Shipped) // If order is shipped
            { // Start of if
                Console.WriteLine($"[Logistics] Dispatch order {order.OrderId}"); // Print logistics notification
            } // End of if
        } // End of LogisticsNotification
    } // End of Notifications class
} // End of namespace