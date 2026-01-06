using System;

    /// <summary>
    /// Represents a patient's bill with all relevant details and calculations.
    /// </summary>
    public class PatientBill // Defines the PatientBill class
    { // Start of PatientBill class
        /// <summary>
        /// Gets or sets the unique bill identifier.
        /// </summary>
        public string? BillId { get; set; } // Bill ID property
        /// <summary>
        /// Gets or sets the patient's name.
        /// </summary>
        public string? PatientName { get; set; } // Patient name property
        /// <summary>
        /// Gets or sets whether the patient has insurance.
        /// </summary>
        public bool HasInsurance { get; set; } // Insurance status property
        /// <summary>
        /// Gets or sets the consultation fee.
        /// </summary>
        public decimal ConsultationFee { get; set; } // Consultation fee property
        /// <summary>
        /// Gets or sets the lab charges.
        /// </summary>
        public decimal LabCharges { get; set; } // Lab charges property
        /// <summary>
        /// Gets or sets the medicine charges.
        /// </summary>
        public decimal MedicineCharges { get; set; } // Medicine charges property
        /// <summary>
        /// Gets the gross amount (total charges before discount).
        /// </summary>
        public decimal GrossAmount // Gross amount property
        { // Start of GrossAmount property
            get { return ConsultationFee + LabCharges + MedicineCharges; } // Calculate gross amount
        } // End of GrossAmount property
        /// <summary>
        /// Gets the discount amount based on insurance status.
        /// </summary>
        public decimal DiscountAmount // Discount amount property
        { // Start of DiscountAmount property
            get { return HasInsurance ? GrossAmount * 0.10m : 0m; } // Calculate discount
        } // End of DiscountAmount property
        /// <summary>
        /// Gets the final payable amount after applying discounts.
        /// </summary>
        public decimal FinalPayable // Final payable property
        { // Start of FinalPayable property
            get { return GrossAmount - DiscountAmount; } // Calculate final payable
        } // End of FinalPayable property
    } // End of PatientBill class

    /// <summary>
    /// Main class for the Question1 application logic.
    /// </summary>
    public class Question1 // Defines the Question1 class
    { // Start of Question1 class
        /// <summary>
        /// Represents the last generated patient bill.
        /// </summary>
        static PatientBill? LastBill = null; // Holds the last bill
        /// <summary>
        /// Indicates whether there is a last bill available.
        /// </summary>
        static bool HasLastBill = false; // Flag for last bill existence

        /// <summary>
        /// Main loop to run the billing application.
        /// </summary>
        public static void Run() // Run method starts the application
        { // Start of Run method
            while (true) // Infinite loop for menu
            { // Start of loop
                Console.WriteLine("================== MediSure Clinic Billing =================="); // Print header
                Console.WriteLine("1. Create New Bill (Enter Patient Details)"); // Print menu option 1
                Console.WriteLine("2. View Last Bill"); // Print menu option 2
                Console.WriteLine("3. Clear Last Bill"); // Print menu option 3
                Console.WriteLine("4. Exit"); // Print menu option 4
                Console.Write("Enter your option: "); // Prompt for option
                string option = Console.ReadLine() ?? ""; // Read user input, ensure not null
                switch (option) // Switch on user input
                { // Start of switch
                    case "1": // If option is 1
                        CreateNewBill(); // Call CreateNewBill
                        break; // Exit case
                    case "2": // If option is 2
                        ViewLastBill(); // Call ViewLastBill
                        break; // Exit case
                    case "3": // If option is 3
                        ClearLastBill(); // Call ClearLastBill
                        break; // Exit case
                    case "4": // If option is 4
                        Console.WriteLine("\nThank you. Application closed normally."); // Print exit message
                        return; // Exit method
                    default: // For any other input
                        Console.WriteLine("Invalid option. Please try again.\n"); // Print invalid message
                        break; // Exit case
                } // End of switch
            } // End of loop
        } // End of Run method

        /// <summary>
        /// Creates a new patient bill by collecting user input.
        /// </summary>
        public static void CreateNewBill() // Method to create a new bill
        { // Start of CreateNewBill
            string billId; // Variable for bill ID
            do // Start of do-while loop
            { // Start of do block
                Console.Write("\nEnter Bill Id: "); // Prompt for bill ID
                billId = Console.ReadLine() ?? ""; // Read bill ID, ensure not null
                if (string.IsNullOrWhiteSpace(billId)) // Check if empty
                    Console.WriteLine("Bill Id cannot be empty."); // Print error
            } while (string.IsNullOrWhiteSpace(billId)); // Repeat if empty
            Console.Write("Enter Patient Name: "); // Prompt for patient name
            string patientName = Console.ReadLine() ?? ""; // Read patient name, ensure not null
            bool hasInsurance = false; // Variable for insurance
            while (true) // Start of insurance loop
            { // Start of loop
                Console.Write("Is the patient insured? (Y/N): "); // Prompt for insurance
                string ins = Console.ReadLine() ?? ""; // Read insurance input, ensure not null
                if (ins.Equals("Y", StringComparison.OrdinalIgnoreCase)) // If yes
                { // Start of if
                    hasInsurance = true; // Set insurance true
                    break; // Exit loop
                } // End of if
                else if (ins.Equals("N", StringComparison.OrdinalIgnoreCase)) // If no
                { // Start of else if
                    hasInsurance = false; // Set insurance false
                    break; // Exit loop
                } // End of else if
                else // If invalid
                { // Start of else
                    Console.WriteLine("Please enter Y or N."); // Print error
                } // End of else
            } // End of insurance loop
            decimal consultationFee = 0; // Variable for consultation fee
            while (true) // Start of consultation fee loop
            { // Start of loop
                Console.Write("Enter Consultation Fee: "); // Prompt for consultation fee
                if (decimal.TryParse(Console.ReadLine(), out consultationFee) && consultationFee > 0) // Parse and check
                    break; // Exit loop if valid
                Console.WriteLine("Consultation Fee must be a number greater than 0."); // Print error
            } // End of consultation fee loop
            decimal labCharges = 0; // Variable for lab charges
            while (true) // Start of lab charges loop
            { // Start of loop
                Console.Write("Enter Lab Charges: "); // Prompt for lab charges
                if (decimal.TryParse(Console.ReadLine(), out labCharges) && labCharges >= 0) // Parse and check
                    break; // Exit loop if valid
                Console.WriteLine("Lab Charges must be a number 0 or greater."); // Print error
            } // End of lab charges loop
            decimal medicineCharges = 0; // Variable for medicine charges
            while (true) // Start of medicine charges loop
            { // Start of loop
                Console.Write("Enter Medicine Charges: "); // Prompt for medicine charges
                if (decimal.TryParse(Console.ReadLine(), out medicineCharges) && medicineCharges >= 0) // Parse and check
                    break; // Exit loop if valid
                Console.WriteLine("Medicine Charges must be a number 0 or greater."); // Print error
            } // End of medicine charges loop
            LastBill = new PatientBill // Create new PatientBill object
            { // Start of object initializer
                BillId = billId, // Set BillId
                PatientName = patientName, // Set PatientName
                HasInsurance = hasInsurance, // Set HasInsurance
                ConsultationFee = consultationFee, // Set ConsultationFee
                LabCharges = labCharges, // Set LabCharges
                MedicineCharges = medicineCharges // Set MedicineCharges
            }; // End of object initializer
            HasLastBill = true; // Set HasLastBill to true
            Console.WriteLine("\nBill created successfully."); // Print success message
            Console.WriteLine("Gross Amount: {0:F2}", LastBill.GrossAmount); // Print gross amount
            Console.WriteLine("Discount Amount: {0:F2}", LastBill.DiscountAmount); // Print discount amount
            Console.WriteLine("Final Payable: {0:F2}", LastBill.FinalPayable); // Print final payable
            Console.WriteLine("------------------------------------------------------------\n"); // Print separator
        } // End of CreateNewBill

        /// <summary>
        /// Displays the last generated patient bill.
        /// </summary>
        public static void ViewLastBill() // Method to view last bill
        { // Start of ViewLastBill
            if (!HasLastBill || LastBill == null) // Check if last bill exists
            { // Start of if
                Console.WriteLine("\nNo bill available. Please create a new bill first.\n"); // Print error
                return; // Exit method
            } // End of if
            Console.WriteLine("\n----------- Last Bill -----------"); // Print header
            Console.WriteLine("BillId: {0}", LastBill.BillId); // Print BillId
            Console.WriteLine("Patient: {0}", LastBill.PatientName); // Print PatientName
            Console.WriteLine("Insured: {0}", LastBill.HasInsurance ? "Yes" : "No"); // Print insurance status
            Console.WriteLine("Consultation Fee: {0:F2}", LastBill.ConsultationFee); // Print consultation fee
            Console.WriteLine("Lab Charges: {0:F2}", LastBill.LabCharges); // Print lab charges
            Console.WriteLine("Medicine Charges: {0:F2}", LastBill.MedicineCharges); // Print medicine charges
            Console.WriteLine("Gross Amount: {0:F2}", LastBill.GrossAmount); // Print gross amount
            Console.WriteLine("Discount Amount: {0:F2}", LastBill.DiscountAmount); // Print discount amount
            Console.WriteLine("Final Payable: {0:F2}", LastBill.FinalPayable); // Print final payable
            Console.WriteLine("--------------------------------"); // Print separator
            Console.WriteLine("------------------------------------------------------------\n"); // Print separator
        } // End of ViewLastBill

        /// <summary>
        /// Clears the last generated patient bill.
        /// </summary>
        public static void ClearLastBill() // Method to clear last bill
        { // Start of ClearLastBill
            LastBill = null; // Set LastBill to null
            HasLastBill = false; // Set HasLastBill to false
            Console.WriteLine("\nLast bill cleared.\n"); // Print cleared message
        } // End of ClearLastBill
    } // End of Question1 class