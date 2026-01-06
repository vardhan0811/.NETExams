
using System; // Import the System namespace for basic C# functionality

/// <summary>
/// Represents a sale transaction with all relevant details and calculations.
/// </summary>
public class SaleTransaction // Defines the SaleTransaction class
{ // Start of SaleTransaction class
    /// <summary>
    /// Gets or sets the invoice number for the transaction.
    /// </summary>
    public string? InvoiceNo { get; set; } // Invoice number property
    /// <summary>
    /// Gets or sets the customer name for the transaction.
    /// </summary>
    public string? CustomerName { get; set; } // Customer name property
    /// <summary>
    /// Gets or sets the item name for the transaction.
    /// </summary>
    public string? ItemName { get; set; } // Item name property
    /// <summary>
    /// Gets or sets the quantity of items in the transaction.
    /// </summary>
    public int Quantity { get; set; } // Quantity property
    /// <summary>
    /// Gets or sets the total purchase amount for the transaction.
    /// </summary>
    public decimal PurchaseAmount { get; set; } // Purchase amount property
    /// <summary>
    /// Gets or sets the total selling amount for the transaction.
    /// </summary>
    public decimal SellingAmount { get; set; } // Selling amount property
    /// <summary>
    /// Gets or sets the profit or loss status for the transaction.
    /// </summary>
    public string? ProfitOrLossStatus { get; set; } // Profit or loss status property
    /// <summary>
    /// Gets or sets the profit or loss amount for the transaction.
    /// </summary>
    public decimal ProfitOrLossAmount { get; set; } // Profit or loss amount property
    /// <summary>
    /// Gets or sets the profit margin percentage for the transaction.
    /// </summary>
    public decimal ProfitMarginPercent { get; set; } // Profit margin percent property

    /// <summary>
    /// Calculates the profit or loss for the transaction.
    /// </summary>
    public void CalculateProfitOrLoss() // Method to calculate profit or loss
    { // Start of CalculateProfitOrLoss
        if (SellingAmount > PurchaseAmount) // If selling amount is greater than purchase amount
        { // Start of if
            ProfitOrLossStatus = "PROFIT"; // Set status to PROFIT
            ProfitOrLossAmount = SellingAmount - PurchaseAmount; // Calculate profit amount
        } // End of if
        else if (SellingAmount < PurchaseAmount) // If selling amount is less than purchase amount
        { // Start of else if
            ProfitOrLossStatus = "LOSS"; // Set status to LOSS
            ProfitOrLossAmount = PurchaseAmount - SellingAmount; // Calculate loss amount
        } // End of else if
        else // If selling amount equals purchase amount
        { // Start of else
            ProfitOrLossStatus = "BREAK-EVEN"; // Set status to BREAK-EVEN
            ProfitOrLossAmount = 0; // Set profit/loss amount to 0
        } // End of else
        if (PurchaseAmount > 0) // If purchase amount is greater than 0
            ProfitMarginPercent = (ProfitOrLossAmount / PurchaseAmount) * 100; // Calculate profit margin percent
        else // If purchase amount is 0 or less
            ProfitMarginPercent = 0; // Set profit margin percent to 0
    } // End of CalculateProfitOrLoss
} // End of SaleTransaction class

/// <summary>
/// Manages sale transactions, including creation, viewing, and recalculation.
/// </summary>
public static class SaleTransactionManager // Defines the SaleTransactionManager class
{ // Start of SaleTransactionManager class
    /// <summary>
    /// Holds the last transaction details.
    /// </summary>
    public static SaleTransaction LastTransaction = new SaleTransaction(); // Last transaction property
    /// <summary>
    /// Indicates if there is a last transaction.
    /// </summary>
    public static bool HasLastTransaction = false; // Flag for last transaction existence

    /// <summary>
    /// Creates a new sale transaction by collecting user input.
    /// </summary>
    public static void CreateNewTransaction() // Method to create a new transaction
    { // Start of CreateNewTransaction
        Console.Write("Enter Invoice No: "); // Prompt for invoice number
        string invoiceNo = Console.ReadLine(); // Read invoice number
        while (string.IsNullOrWhiteSpace(invoiceNo)) // While invoice number is empty
        { // Start of while
            Console.Write("Invoice No cannot be empty. Enter Invoice No: "); // Prompt again
            invoiceNo = Console.ReadLine(); // Read invoice number again
        } // End of while
        Console.Write("Enter Customer Name: "); // Prompt for customer name
        string customerName = Console.ReadLine(); // Read customer name
        Console.Write("Enter Item Name: "); // Prompt for item name
        string itemName = Console.ReadLine(); // Read item name
        int quantity = 0; // Variable for quantity
        while (true) // Start of quantity loop
        { // Start of loop
            Console.Write("Enter Quantity: "); // Prompt for quantity
            string qtyInput = Console.ReadLine(); // Read quantity input
            if (int.TryParse(qtyInput, out quantity) && quantity > 0) // Parse and check
                break; // Exit loop if valid
            Console.WriteLine("Quantity must be a positive integer."); // Print error
        } // End of quantity loop
        decimal purchaseAmount = 0; // Variable for purchase amount
        while (true) // Start of purchase amount loop
        { // Start of loop
            Console.Write("Enter Purchase Amount (total): "); // Prompt for purchase amount
            string purchaseInput = Console.ReadLine(); // Read purchase amount input
            if (decimal.TryParse(purchaseInput, out purchaseAmount) && purchaseAmount > 0) // Parse and check
                break; // Exit loop if valid
            Console.WriteLine("Purchase Amount must be a positive number."); // Print error
        } // End of purchase amount loop
        decimal sellingAmount = 0; // Variable for selling amount
        while (true) // Start of selling amount loop
        { // Start of loop
            Console.Write("Enter Selling Amount (total): "); // Prompt for selling amount
            string sellingInput = Console.ReadLine(); // Read selling amount input
            if (decimal.TryParse(sellingInput, out sellingAmount) && sellingAmount >= 0) // Parse and check
                break; // Exit loop if valid
            Console.WriteLine("Selling Amount must be zero or positive."); // Print error
        } // End of selling amount loop
        SaleTransaction transaction = new SaleTransaction // Create new SaleTransaction object
        { // Start of object initializer
            InvoiceNo = invoiceNo.Trim(), // Set InvoiceNo
            CustomerName = customerName?.Trim(), // Set CustomerName
            ItemName = itemName?.Trim(), // Set ItemName
            Quantity = quantity, // Set Quantity
            PurchaseAmount = purchaseAmount, // Set PurchaseAmount
            SellingAmount = sellingAmount // Set SellingAmount
        }; // End of object initializer
        transaction.CalculateProfitOrLoss(); // Calculate profit or loss
        LastTransaction = transaction; // Set LastTransaction
        HasLastTransaction = true; // Set HasLastTransaction to true
        Console.WriteLine("\nTransaction saved successfully."); // Print success message
        Console.WriteLine($"Status: {transaction.ProfitOrLossStatus}"); // Print status
        Console.WriteLine($"Profit/Loss Amount: {transaction.ProfitOrLossAmount:F2}"); // Print profit/loss amount
        Console.WriteLine($"Profit Margin (%): {transaction.ProfitMarginPercent:F2}"); // Print profit margin
        Console.WriteLine("------------------------------------------------------\n"); // Print separator
    } // End of CreateNewTransaction

    /// <summary>
    /// Displays the last transaction details.
    /// </summary>
    public static void ViewLastTransaction() // Method to view last transaction
    { // Start of ViewLastTransaction
        if (!HasLastTransaction || LastTransaction == null) // Check if last transaction exists
        { // Start of if
            Console.WriteLine("No transaction available. Please create a new transaction first.\n"); // Print error
            return; // Exit method
        } // End of if
        var t = LastTransaction; // Get last transaction
        Console.WriteLine("\n-------------- Last Transaction --------------"); // Print header
        Console.WriteLine($"InvoiceNo: {t.InvoiceNo}"); // Print InvoiceNo
        Console.WriteLine($"Customer: {t.CustomerName}"); // Print CustomerName
        Console.WriteLine($"Item: {t.ItemName}"); // Print ItemName
        Console.WriteLine($"Quantity: {t.Quantity}"); // Print Quantity
        Console.WriteLine($"Purchase Amount: {t.PurchaseAmount:F2}"); // Print PurchaseAmount
        Console.WriteLine($"Selling Amount: {t.SellingAmount:F2}"); // Print SellingAmount
        Console.WriteLine($"Status: {t.ProfitOrLossStatus}"); // Print status
        Console.WriteLine($"Profit/Loss Amount: {t.ProfitOrLossAmount:F2}"); // Print profit/loss amount
        Console.WriteLine($"Profit Margin (%): {t.ProfitMarginPercent:F2}"); // Print profit margin
        Console.WriteLine("--------------------------------------------"); // Print separator
        Console.WriteLine("------------------------------------------------------\n"); // Print separator
    } // End of ViewLastTransaction

    /// <summary>
    /// Recalculates and displays the profit or loss for the last transaction.
    /// </summary>
    public static void RecalculateAndPrint() // Method to recalculate and print transaction
    { // Start of RecalculateAndPrint
        if (!HasLastTransaction || LastTransaction == null) // Check if last transaction exists
        { // Start of if
            Console.WriteLine("No transaction available. Please create a new transaction first.\n"); // Print error
            return; // Exit method
        } // End of if
        LastTransaction.CalculateProfitOrLoss(); // Recalculate profit or loss
        Console.WriteLine("\n-------------- Recalculated Transaction --------------"); // Print header
        Console.WriteLine($"InvoiceNo: {LastTransaction.InvoiceNo}"); // Print InvoiceNo
        Console.WriteLine($"Customer: {LastTransaction.CustomerName}"); // Print CustomerName
        Console.WriteLine($"Item: {LastTransaction.ItemName}"); // Print ItemName
        Console.WriteLine($"Quantity: {LastTransaction.Quantity}"); // Print Quantity
        Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount:F2}"); // Print PurchaseAmount
        Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount:F2}"); // Print SellingAmount
        Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}"); // Print status
        Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}"); // Print profit/loss amount
        Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}"); // Print profit margin
        Console.WriteLine("--------------------------------------------"); // Print separator
        Console.WriteLine("------------------------------------------------------\n"); // Print separator
    } // End of RecalculateAndPrint
} // End of SaleTransactionManager class

/// <summary>
/// Main class for the Week 1 Exam 2 application logic.
/// </summary>
public class Week1Exam2 // Defines the Week1Exam2 class
{ // Start of Week1Exam2 class
    /// <summary>
    /// Main loop to run the sales transaction application.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the program.</param>
    public static void Run(string[] args) // Run method starts the application
    { // Start of Run method
        while (true) // Infinite loop for menu
        { // Start of loop
            Console.WriteLine("================== QuickMart Traders =================="); // Print header
            Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)"); // Print menu option 1
            Console.WriteLine("2. View Last Transaction"); // Print menu option 2
            Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)"); // Print menu option 3
            Console.WriteLine("4. Exit"); // Print menu option 4
            Console.Write("Enter your option: "); // Prompt for option
            string input = Console.ReadLine(); // Read user input
            Console.WriteLine(); // Print empty line
            switch (input) // Switch on user input
            { // Start of switch
                case "1": // If option is 1
                    SaleTransactionManager.CreateNewTransaction(); // Call CreateNewTransaction
                    break; // Exit case
                case "2": // If option is 2
                    SaleTransactionManager.ViewLastTransaction(); // Call ViewLastTransaction
                    break; // Exit case
                case "3": // If option is 3
                    SaleTransactionManager.RecalculateAndPrint(); // Call RecalculateAndPrint
                    break; // Exit case
                case "4": // If option is 4
                    Console.WriteLine("Thank you. Application closed normally."); // Print exit message
                    return; // Exit method
                default: // For any other input
                    Console.WriteLine("Invalid option. Please enter a valid menu number (1-4).\n"); // Print invalid message
                    break; // Exit case
            } // End of switch
        } // End of loop
    } // End of Run method
} // End of Week1Exam2 class
