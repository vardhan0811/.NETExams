using System;
using System.Collections.Generic;
using System.Linq;

namespace PettyCashLedgerSystem
{
    /// <summary>
    /// Interface for reporting transaction summaries
    /// </summary>
    public interface IReportable
    {
    	/// <summary>
    	/// Returns a summary string for the transaction
    	/// </summary>
    	string GetSummary();
    }

    /// <summary>
    /// Abstract base class for all transactions
    /// </summary>
    public abstract class Transaction : IReportable
    {
    	// Unique transaction identifier
    	public int Id { get; protected set; }
    	// Date of the transaction
    	public DateTime Date { get; protected set; }
    	// Amount of money involved
    	public decimal Amount { get; protected set; }
    	// Description of the transaction  
    	public string Details { get; protected set; }

    	/// <summary>
    	/// Constructor for Transaction
    	/// </summary>
    	protected Transaction(int id, DateTime date, decimal amount, string details)
    	{
    		Id = id;
    		Date = date;
    		Amount = amount;
    		Details = details;
    	}

    	// Abstract summary method
    	public abstract string GetSummary();
    }

    /// <summary>
    /// Represents an expense transaction
    /// </summary>
    public class ExpenseTransaction : Transaction
    {
    	// Category of the expense
    	public string Type { get; private set; }

    	/// <summary>
    	/// Constructor for ExpenseTransaction
    	/// </summary>
    	public ExpenseTransaction(int id, DateTime date, decimal amount, string details, string type) : base(id, date, amount, details)
    	{
    		Type = type;
    	}

    	/// <summary>
    	/// Returns summary for expense
    	/// </summary>
    	public override string GetSummary()
        {
            return $"ID: {Id} | Expense | {Type} | {Details} | {Date:yyyy-MM-dd} | ${Amount:F2}";
        }
    }

    /// <summary>
    /// Represents an income transaction
    /// </summary>
    public class IncomeTransaction : Transaction
    {
    	// Source of the income
    	public string Origin { get; private set; }

    	/// <summary>
    	/// Constructor for IncomeTransaction
    	/// </summary>
    	public IncomeTransaction(int id, DateTime date, decimal amount, string details, string origin) : base(id, date, amount, details)
    	{
    		Origin = origin;
    	}

    	/// <summary>
    	/// Returns summary for income
    	/// </summary>
    	public override string GetSummary()
        {
            return $"ID: {Id} | Income | {Origin} | {Details} | {Date:yyyy-MM-dd} | ${Amount:F2}";
        }
    }

    /// <summary>
    /// Generic ledger for storing transactions
    /// </summary>
    public class Ledger<T> where T : Transaction
    {
        // Stores all transaction records
    	private List<T> _records = new List<T>();

    	/// <summary>
    	/// Adds a transaction to the ledger
    	/// </summary>
    	public void AddEntry(T entry)
    	{
    		_records.Add(entry);
    	}

    	/// <summary>
    	/// Gets transactions for a specific date
    	/// </summary>
    	public List<T> GetTransactionsByDate(DateTime date)
    	{
    		return _records.Where(x => x.Date.Date == date.Date).ToList();
    	}

    	/// <summary>
    	/// Calculates total amount in the ledger
    	/// </summary>
    	public decimal CalculateTotal()
    	{
    		return _records.Sum(x => x.Amount);
    	}

    	/// <summary>
    	/// Gets all transactions
    	/// </summary>
    	public List<T> GetAll()
    	{
    		return new List<T>(_records);
    	}
    }

// Main program class
public class Week02
{
    // Entry point of the application
    public static void Run(string[] args)
    {
        // Create ledgers for income and expenses
        var incomeBook = new Ledger<IncomeTransaction>();
        var expenseBook = new Ledger<ExpenseTransaction>();
        bool running = true;
        // Main menu loop
        while (running)
        {
            Console.WriteLine("\nPetty Cash Ledger System");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. Show Summary");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option (1-4): ");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddIncome(incomeBook);
                    break;
                case "2":
                    AddExpense(expenseBook);
                    break;
                case "3":
                    ShowSummary(incomeBook, expenseBook);
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1-4.");
                    break;
            }
        }
    }

    // Handles adding an income transaction
    private static void AddIncome(Ledger<IncomeTransaction> incomeBook)
    {
        Console.WriteLine("\nEnter Income Transaction Details:");
        // Prompt for each detail
        Console.Write("ID: ");
        string? incomeIdInput = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(incomeIdInput)) // Validate ID input
        {
            Console.WriteLine("Invalid input for ID.");
            return;
        }
        int incomeId = int.Parse(incomeIdInput);
        
        Console.Write("Amount: "); // Prompt for amount
        string? incomeAmountInput = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(incomeAmountInput) || !decimal.TryParse(incomeAmountInput, out decimal incomeAmount)) // Validate Amount input
        {
            Console.WriteLine("Invalid input for Amount.");
            return;
        }

        Console.Write("Description: "); // Prompt for description
        string incomeDesc = Console.ReadLine() ?? string.Empty;
        Console.Write("Source (e.g., Salary, Gift, etc.): ");
        string incomeSource = Console.ReadLine() ?? string.Empty;
        
        var incomeEntry = new IncomeTransaction(incomeId, DateTime.Today, incomeAmount, incomeDesc, incomeSource); // Create and add the income transaction
        incomeBook.AddEntry(incomeEntry);
        Console.WriteLine("Income transaction added successfully.");
    }

    // Handles adding an expense transaction
    private static void AddExpense(Ledger<ExpenseTransaction> expenseBook)
    {
        Console.WriteLine("\nEnter Expense Transaction Details:");
        Console.Write("ID: ");
        string? expenseIdInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(expenseIdInput))
        {
            Console.WriteLine("Invalid input for ID.");
            return;
        }
        int expenseId = int.Parse(expenseIdInput);
        Console.Write("Amount: ");
        string? expenseAmountInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(expenseAmountInput) || !decimal.TryParse(expenseAmountInput, out decimal expenseAmount))
        {
            Console.WriteLine("Invalid input for Amount.");
            return;
        }
        Console.Write("Description: ");
        string expenseDesc = Console.ReadLine() ?? string.Empty;
        Console.Write("Category (e.g., Food, Travel, etc.): ");
        string expenseCategory = Console.ReadLine() ?? string.Empty;
        // Create and add the expense transaction
        var expenseEntry = new ExpenseTransaction(expenseId, DateTime.Today, expenseAmount, expenseDesc, expenseCategory);
        expenseBook.AddEntry(expenseEntry);
        Console.WriteLine("Expense transaction added successfully.");
    }

    // Displays a summary of all transactions and balances
    private static void ShowSummary(Ledger<IncomeTransaction> incomeBook, Ledger<ExpenseTransaction> expenseBook)
    {
        // Calculate totals
        decimal totalIncome = incomeBook.CalculateTotal();
        decimal totalExpense = expenseBook.CalculateTotal();
        decimal netAmount = totalIncome - totalExpense;
        Console.WriteLine($"\nTotal received: ${totalIncome}");
        Console.WriteLine($"Total spent: ${totalExpense}");
        Console.WriteLine($"Net balance: ${netAmount}");
        // Combine all transactions for summary
        var allTransactions = new List<Transaction>();
        allTransactions.AddRange(incomeBook.GetAll());
        allTransactions.AddRange(expenseBook.GetAll());
        Console.WriteLine("\nTransaction summaries:");
        // Display each transaction summary
        foreach (var trans in allTransactions)
        {
            Console.WriteLine(trans.GetSummary());
        }
    }
}
}
