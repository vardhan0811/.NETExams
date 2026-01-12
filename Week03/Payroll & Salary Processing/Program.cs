using System;
using System.Collections.Generic;

namespace PayrollAndSalaryProcessing
{ 
    class Program 
    { // Start of PayrollAndSalaryProcessing class
        /// <summary>
        /// Runs the payroll and salary processing logic, allowing user to choose data entry mode.
        /// </summary>
        public static void Main() 
        { 

            Dictionary<int, Employee> employees = new Dictionary<int, Employee>(); // Create a dictionary to hold employees
            // Always add preexisting employees first
            employees[1] = new FullTimeEmployee(1, "Elijah", 50000); 
            employees[2] = new FullTimeEmployee(2, "Klaus", 60000); 
            employees[3] = new ContractEmployee(3, "Kol", 1000, 20);  
            employees[4] = new ContractEmployee(4, "Finn", 1200, 22); 
            employees[5] = new FullTimeEmployee(5, "Esther", 70000); 
            employees[6] = new ContractEmployee(6, "Rebekah", 900, 25); 

            Console.WriteLine("Choose data entry mode:");
            Console.WriteLine("1. Use pre_existing employees"); 
            Console.WriteLine("2. Enter new employees"); 
            Console.Write("Enter choice (1 or 2): "); 
            string? mode = Console.ReadLine(); 

            if (mode == "2")
            {
                Console.Write("Enter number of employees: "); 
                int empCount = int.Parse(Console.ReadLine() ?? "0"); 
                for (int i = 0; i < empCount; i++) // Loop for each employee
                {
                    Console.WriteLine($"Employee #{i+1}:"); 
                    int id;
                    while (true)
                    {
                        Console.Write("ID: ");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        if (employees.ContainsKey(id))
                        {
                            Console.WriteLine("Employee ID already exists. Please enter a unique ID.");
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.Write("Name: ");
                    string name = Console.ReadLine() ?? ""; 
                    Console.Write("Type (F for FullTime, C for Contract): "); 
                    string type = (Console.ReadLine() ?? "F").ToUpper(); 
                    if (type == "F") 
                    { 
                        Console.Write("Salary: "); 
                        double salary = double.Parse(Console.ReadLine() ?? "0"); // Read and parse salary
                        employees[id] = new FullTimeEmployee(id, name, salary); // Add FullTimeEmployee
                    } 
                    else 
                    { 
                        Console.Write("Rate: "); 
                        double rate = double.Parse(Console.ReadLine() ?? "0"); // Read and parse rate
                        Console.Write("Hours: ");
                        int hours = int.Parse(Console.ReadLine() ?? "0"); // Read and parse hours
                        employees[id] = new ContractEmployee(id, name, rate, hours); // Add ContractEmployee
                    }
                } 
                Console.WriteLine($"{empCount} employees added successfully.\n"); // Print confirmation
            }

            Console.WriteLine("\n--- Employees Summary ---"); // Print employees summary header
            PayrollProcessor processor = new PayrollProcessor(); // Create PayrollProcessor instance
            processor.SalaryProcessed += Notifications.HRNotification; // Subscribe HR notification
            processor.SalaryProcessed += Notifications.FinanceNotification; // Subscribe Finance notification

            List<PaySlip> slips = processor.ProcessPayroll(employees); // Process payroll and get payslips

            Console.WriteLine("\n--- Payroll Summary ---"); // Print payroll summary header
            Console.WriteLine($"Total Employees: {slips.Count}"); // Print total employees
            Console.WriteLine($"Total Payout: {slips.Sum(s => s.Net)}"); // Print total payout
            Console.WriteLine($"Highest Salary: {slips.Max(s => s.Net)}"); // Print highest salary
        } 
    } 
} 