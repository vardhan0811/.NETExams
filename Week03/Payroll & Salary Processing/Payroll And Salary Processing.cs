
using System; 

namespace PayrollAndSalaryProcessing 
{ 
    /// <summary>
    /// Represents a payslip for an employee, including salary details.
    /// </summary>
    public class PaySlip 
    { 
        public int Id; // Employee ID
        public string Name; // Employee name
        public string Type; // Employee type (FullTime/Contract)
        public double Gross; // Gross salary
        public double Deductions; // Deductions from salary
        public double Net; // Net salary after deductions

        /// <summary>
        /// Initializes a new instance of the PaySlip class.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="name">Employee name</param>
        /// <param name="type">Employee type</param>
        /// <param name="gross">Gross salary</param>
        /// <param name="deductions">Deductions from salary</param>
        /// <param name="net">Net salary after deductions</param>
        public PaySlip(int id, string name, string type, double gross, double deductions, double net) // PaySlip constructor
        { 
            Id = id; // Set employee ID
            Name = name; // Set employee name
            Type = type; // Set employee type
            Gross = gross; // Set gross salary
            Deductions = deductions; // Set deductions
            Net = net; // Set net salary
        } 
    } 


    /// <summary>
    /// Abstract base class for employees.
    /// </summary>
    public abstract class Employee 
    { 
        public int Id { get; } // Employee ID property
        public string Name { get; } // Employee name property
        public string Type { get; } // Employee type property

        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="name">Employee name</param>
        /// <param name="type">Employee type</param>
        protected Employee(int id, string name, string type) // Employee constructor
        { 
            Id = id; // Set employee ID
            Name = name; // Set employee name
            Type = type; // Set employee type
        } 

        /// <summary>
        /// Calculates the salary for the employee.
        /// </summary>
        /// <returns>A PaySlip object with salary details.</returns>
        public abstract PaySlip CalculateSalary();
    } 


    /// <summary>
    /// Represents a full-time employee.
    /// </summary>
    public class FullTimeEmployee : Employee 
    { 
        private double _monthlySalary; // Monthly salary field

        /// <summary>
        /// Initializes a new instance of the FullTimeEmployee class.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="name">Employee name</param>
        /// <param name="salary">Monthly salary</param>
        public FullTimeEmployee(int id, string name, double salary) : base(id, name, "FullTime") // Constructor
        { 
            if (salary < 0) // Check for negative salary
                throw new ArgumentException("Salary cannot be negative"); // Throw exception if negative
            _monthlySalary = salary; // Set monthly salary
        }

        /// <summary>
        /// Calculates the salary for a full-time employee.
        /// </summary>
        /// <returns>A PaySlip object with salary details.</returns>
        public override PaySlip CalculateSalary() 
        { 
            double deduction = _monthlySalary * 0.10; // Calculate deduction (10%)
            double net = _monthlySalary - deduction; // Calculate net salary
            return new PaySlip(Id, Name, Type, _monthlySalary, deduction, net); // Return PaySlip object
        }
    } 


    /// <summary>
    /// Represents a contract employee.
    /// </summary>
    public class ContractEmployee : Employee 
    {
        private double _dailyRate; // Daily rate field
        private int _workingDays; // Working days field

        /// <summary>
        /// Initializes a new instance of the ContractEmployee class.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="name">Employee name</param>
        /// <param name="rate">Daily rate</param>
        /// <param name="days">Number of working days</param>
        public ContractEmployee(int id, string name, double rate, int days) : base(id, name, "Contract") 
        {
            if (rate < 0 || days < 0 || days > 31) // Validate rate and days
                throw new ArgumentException("Invalid rate or days"); // Throw exception if invalid
            _dailyRate = rate; // Set daily rate
            _workingDays = days; // Set working days
        }

        /// <summary>
        /// Calculates the salary for a contract employee.
        /// </summary>
        /// <returns>A PaySlip object with salary details.</returns>
        public override PaySlip CalculateSalary() 
        {
            double gross = _dailyRate * _workingDays; // Calculate gross salary
            double deduction = gross * 0.05; // Calculate deduction (5%)
            double net = gross - deduction; // Calculate net salary
            return new PaySlip(Id, Name, Type, gross, deduction, net); // Return PaySlip object
        }
    }


    /// <summary>
    /// Processes payroll for a list of employees.
    /// </summary>
    public class PayrollProcessor
    {
        public Action<PaySlip>? SalaryProcessed; // Event for salary processed notification

        /// <summary>
        /// Processes payroll for all employees in the dictionary.
        /// </summary>
        /// <param name="employees">Dictionary of employees</param>
        /// <returns>List of PaySlip objects for all employees.</returns>
        public List<PaySlip> ProcessPayroll(Dictionary<int, Employee> employees)
        {
            List<PaySlip> slips = new List<PaySlip>(); // List to hold payslips
            foreach (var emp in employees.Values) 
            {
                PaySlip slip = emp.CalculateSalary(); // Calculate salary
                slips.Add(slip); // Add payslip to list
                SalaryProcessed?.Invoke(slip); // Invoke notification if set
            }
            return slips; // Return list of payslips
        }
    }

    /// <summary>
    /// Provides notification methods for payroll processing.
    /// </summary>
    public static class Notifications 
    {
        /// <summary>
        /// Notifies HR when a salary is processed.
        /// </summary>
        /// <param name="slip">The processed PaySlip object.</param>
        public static void HRNotification(PaySlip slip) 
        { 
            Console.WriteLine($"[HR] Processed {slip.Name} ({slip.Type})"); // Print HR notification
        } 

        /// <summary>
        /// Notifies Finance when a salary is processed.
        /// </summary>
        /// <param name="slip">The processed PaySlip object.</param>
        public static void FinanceNotification(PaySlip slip) 
        { 
            Console.WriteLine($"[Finance] Net Salary: {slip.Net}"); // Print Finance notification
        } 
    }
}