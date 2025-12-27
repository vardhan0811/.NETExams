using System;
public class SaleTransaction
{
    public string? InvoiceNo { get; set; }
    public string? CustomerName { get; set; }
    public string? ItemName { get; set; }
    public int Quantity { get; set; }
    public decimal PurchaseAmount { get; set; }
    public decimal SellingAmount { get; set; }
    public string? ProfitOrLossStatus { get; set; }
    public decimal ProfitOrLossAmount { get; set; }
    public decimal ProfitMarginPercent { get; set; }

    // Method to calculate profit or loss
    public void CalculateProfitOrLoss()
    {
        if (SellingAmount > PurchaseAmount)
        {
            ProfitOrLossStatus = "PROFIT";
            ProfitOrLossAmount = SellingAmount - PurchaseAmount;
        }
        else if (SellingAmount < PurchaseAmount)
        {
            ProfitOrLossStatus = "LOSS";
            ProfitOrLossAmount = PurchaseAmount - SellingAmount;
        }
        else
        {
            ProfitOrLossStatus = "BREAK-EVEN";
            ProfitOrLossAmount = 0;
        }
        if (PurchaseAmount > 0)
            ProfitMarginPercent = (ProfitOrLossAmount / PurchaseAmount) * 100;
        else
            ProfitMarginPercent = 0;
        }
}

public static class SaleTransactionManager
{
    // Static instance to hold the last transaction details
    public static SaleTransaction LastTransaction = new SaleTransaction();

    // Flag to indicate if there is a last transaction
    public static bool HasLastTransaction = false;

    // Method to create a new transaction
    public static void CreateNewTransaction()
    {
        Console.Write("Enter Invoice No: ");
        string invoiceNo = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(invoiceNo))
        {
            Console.Write("Invoice No cannot be empty. Enter Invoice No: ");
            invoiceNo = Console.ReadLine();
        }
        Console.Write("Enter Customer Name: ");
        string customerName = Console.ReadLine();
        Console.Write("Enter Item Name: ");
        string itemName = Console.ReadLine();
        int quantity = 0;
        while (true)
        {
            // Prompt for quantity
            Console.Write("Enter Quantity: ");
            string qtyInput = Console.ReadLine();
            if (int.TryParse(qtyInput, out quantity) && quantity > 0)
                break;
            Console.WriteLine("Quantity must be a positive integer.");
        }
        decimal purchaseAmount = 0;
        while (true)
        {
            // Prompt for purchase amount
            Console.Write("Enter Purchase Amount (total): ");
            string purchaseInput = Console.ReadLine();
            if (decimal.TryParse(purchaseInput, out purchaseAmount) && purchaseAmount > 0)
                break;
            Console.WriteLine("Purchase Amount must be a positive number.");
        }
        decimal sellingAmount = 0;
        while (true)
        {
            Console.Write("Enter Selling Amount (total): ");
            string sellingInput = Console.ReadLine();
            if (decimal.TryParse(sellingInput, out sellingAmount) && sellingAmount >= 0)
                break;
            Console.WriteLine("Selling Amount must be zero or positive.");
        }

        // Create a new SaleTransaction object
        SaleTransaction transaction = new SaleTransaction
        {
            InvoiceNo = invoiceNo.Trim(),
            CustomerName = customerName?.Trim(),
            ItemName = itemName?.Trim(),
            Quantity = quantity,
            PurchaseAmount = purchaseAmount,
            SellingAmount = sellingAmount
        };

        // Calculate profit or loss
        transaction.CalculateProfitOrLoss();
        LastTransaction = transaction;
        HasLastTransaction = true;
        Console.WriteLine("\nTransaction saved successfully.");
        Console.WriteLine($"Status: {transaction.ProfitOrLossStatus}");
        Console.WriteLine($"Profit/Loss Amount: {transaction.ProfitOrLossAmount:F2}");
        Console.WriteLine($"Profit Margin (%): {transaction.ProfitMarginPercent:F2}");
        Console.WriteLine("------------------------------------------------------\n");
    }

    public static void ViewLastTransaction()
    {
        // Check if there is a last transaction
        if (!HasLastTransaction || LastTransaction == null)
        {
            Console.WriteLine("No transaction available. Please create a new transaction first.\n");
            return;
        }

        // Get the last transaction details
        var t = LastTransaction;
        Console.WriteLine("\n-------------- Last Transaction --------------");
        Console.WriteLine($"InvoiceNo: {t.InvoiceNo}");
        Console.WriteLine($"Customer: {t.CustomerName}");
        Console.WriteLine($"Item: {t.ItemName}");
        Console.WriteLine($"Quantity: {t.Quantity}");
        Console.WriteLine($"Purchase Amount: {t.PurchaseAmount:F2}");
        Console.WriteLine($"Selling Amount: {t.SellingAmount:F2}");
        Console.WriteLine($"Status: {t.ProfitOrLossStatus}");
        Console.WriteLine($"Profit/Loss Amount: {t.ProfitOrLossAmount:F2}");
        Console.WriteLine($"Profit Margin (%): {t.ProfitMarginPercent:F2}");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("------------------------------------------------------\n");
        }

    public static void RecalculateAndPrint()
    {
        // Check if there is a last transaction
        if (!HasLastTransaction || LastTransaction == null)
        {
            Console.WriteLine("No transaction available. Please create a new transaction first.\n");
            return;
        }

        // Recalculate profit or loss
        LastTransaction.CalculateProfitOrLoss();
        Console.WriteLine("\n-------------- Recalculated Transaction --------------");
        Console.WriteLine($"InvoiceNo: {LastTransaction.InvoiceNo}");
        Console.WriteLine($"Customer: {LastTransaction.CustomerName}");
        Console.WriteLine($"Item: {LastTransaction.ItemName}");
        Console.WriteLine($"Quantity: {LastTransaction.Quantity}");
        Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount:F2}");
        Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount:F2}");
        Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
        Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
        Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("------------------------------------------------------\n");
        }
}

public class Week1Exam2
{
    public static void Run(string[] args)
    {
        while (true)
        {
            // Display menu options
            Console.WriteLine("================== QuickMart Traders ==================");
            Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
            Console.WriteLine("2. View Last Transaction");
            Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            // Handle user input
            switch (input)
            {
                case "1":
                    SaleTransactionManager.CreateNewTransaction();
                    break;
                case "2":
                    SaleTransactionManager.ViewLastTransaction();
                    break;
                case "3":
                    SaleTransactionManager.RecalculateAndPrint();
                    break;
                case "4":
                    Console.WriteLine("Thank you. Application closed normally.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please enter a valid menu number (1-4).\n");
                    break;
            }
        }
    }
}
