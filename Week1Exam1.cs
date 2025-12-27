using System;
public class PatientBill
{
    public string BillId { get; set; }
    public string PatientName { get; set; }
    public bool HasInsurance { get; set; }
    public decimal ConsultationFee { get; set; }
    public decimal LabCharges { get; set; }
    public decimal MedicineCharges { get; set; }
    /// <summary>
    /// Gets the gross amount (total charges before discount).
    /// </summary>
    public decimal GrossAmount
    {
        get { return ConsultationFee + LabCharges + MedicineCharges; }
    }
    /// <summary>
    /// Gets the discount amount based on insurance status.
    /// </summary>
    public decimal DiscountAmount
    {
        get { return HasInsurance ? GrossAmount * 0.10m : 0m; }
    }
    /// <summary>
    /// Gets the final payable amount after applying discounts.
    /// </summary>
    public decimal FinalPayable
    {
        get { return GrossAmount - DiscountAmount; }
    }
}

public class Week1Exam1
{
    /// <summary>
    /// Represents the last generated patient bill.
    /// </summary>
    static PatientBill LastBill = null;
    /// <summary>
    /// Indicates whether there is a last bill available.
    /// </summary>
    static bool HasLastBill = false;
    public static void Run()
    {
        while (true)
        {
            Console.WriteLine("================== MediSure Clinic Billing ==================");
            Console.WriteLine("1. Create New Bill (Enter Patient Details)");
            Console.WriteLine("2. View Last Bill");
            Console.WriteLine("3. Clear Last Bill");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    CreateNewBill();
                    break;
                case "2":
                    ViewLastBill();
                    break;
                case "3":
                    ClearLastBill();
                    break;
                case "4":
                    Console.WriteLine("\nThank you. Application closed normally.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.\n");
                    break;
                }
            }
        }

    public static void CreateNewBill()
    {

        /// <summary>
        /// Creates a new patient bill.
        /// </summary>
        string billId;
        do
        {
            Console.Write("\nEnter Bill Id: ");
            billId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(billId))
                Console.WriteLine("Bill Id cannot be empty.");
        } while (string.IsNullOrWhiteSpace(billId));
        Console.Write("Enter Patient Name: ");
        string patientName = Console.ReadLine();
        bool hasInsurance = false;
        while (true)
        {
            Console.Write("Is the patient insured? (Y/N): ");
            string ins = Console.ReadLine();
            if (ins.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                hasInsurance = true;
                break;
            }
            else if (ins.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                hasInsurance = false;
                break;
            }
            else
            {
                Console.WriteLine("Please enter Y or N.");
            }
        }

        /// <summary>
        /// Gets the consultation fee.
        /// </summary>
        decimal consultationFee = 0;
        while (true)
        {
            Console.Write("Enter Consultation Fee: ");
            if (decimal.TryParse(Console.ReadLine(), out consultationFee) && consultationFee > 0)
                break;
            Console.WriteLine("Consultation Fee must be a number greater than 0.");
        }

        /// <summary>
        /// Gets the lab charges.
        /// </summary>
        decimal labCharges = 0;
        while (true)
        {
            Console.Write("Enter Lab Charges: ");
            if (decimal.TryParse(Console.ReadLine(), out labCharges) && labCharges >= 0)
                break;
            Console.WriteLine("Lab Charges must be a number 0 or greater.");
        }

        /// <summary>
        /// Gets the medicine charges.
        /// </summary>
        decimal medicineCharges = 0;
        while (true)
        {
            Console.Write("Enter Medicine Charges: ");
            if (decimal.TryParse(Console.ReadLine(), out medicineCharges) && medicineCharges >= 0)
                break;
            Console.WriteLine("Medicine Charges must be a number 0 or greater.");
        }

        /// <summary>
        /// Creates a new patient bill.
        /// </summary>
        LastBill = new PatientBill
        {
            BillId = billId,
            PatientName = patientName,
            HasInsurance = hasInsurance,
            ConsultationFee = consultationFee,
            LabCharges = labCharges,
            MedicineCharges = medicineCharges
        };
        HasLastBill = true;
        Console.WriteLine("\nBill created successfully.");
        Console.WriteLine("Gross Amount: {0:F2}", LastBill.GrossAmount);
        Console.WriteLine("Discount Amount: {0:F2}", LastBill.DiscountAmount);
        Console.WriteLine("Final Payable: {0:F2}", LastBill.FinalPayable);
        Console.WriteLine("------------------------------------------------------------\n");
    }

    /// <summary>
    /// Views the last generated patient bill.
    /// </summary>
    public static void ViewLastBill()
    {
        if (!HasLastBill || LastBill == null)
        {
            Console.WriteLine("\nNo bill available. Please create a new bill first.\n");
            return;
        }
        Console.WriteLine("\n----------- Last Bill -----------");
        Console.WriteLine("BillId: {0}", LastBill.BillId);
        Console.WriteLine("Patient: {0}", LastBill.PatientName);
        Console.WriteLine("Insured: {0}", LastBill.HasInsurance ? "Yes" : "No");
        Console.WriteLine("Consultation Fee: {0:F2}", LastBill.ConsultationFee);
        Console.WriteLine("Lab Charges: {0:F2}", LastBill.LabCharges);
        Console.WriteLine("Medicine Charges: {0:F2}", LastBill.MedicineCharges);
        Console.WriteLine("Gross Amount: {0:F2}", LastBill.GrossAmount);
        Console.WriteLine("Discount Amount: {0:F2}", LastBill.DiscountAmount);
        Console.WriteLine("Final Payable: {0:F2}", LastBill.FinalPayable);
        Console.WriteLine("--------------------------------");
        Console.WriteLine("------------------------------------------------------------\n");
    }

    /// <summary>
    /// Clears the last generated patient bill.
    /// </summary>
    public static void ClearLastBill()
    {
        LastBill = null;
        HasLastBill = false;
        Console.WriteLine("\nLast bill cleared.\n");
    }
}