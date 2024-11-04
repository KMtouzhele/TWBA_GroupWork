using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Model;
using TheWeakestBankOfAntarctica.Controller;
using TheWeakestBankOfAntarctica.Data;
using System.Security.Principal;
using TheWeakestBankOfAntarctica.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TheWeakestBankOfAntarctica.View
{
    public static class AccountView
    {
        // Define headers for the DataView
        static string[] headers = { "Account #", "Owner Id", "Account Type", "Current Balance" };
        static TWBA twba;
        public static void AccountMenu(TWBA mainSystem)
        {
            twba = mainSystem;
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("*****Account Menu******");
                Console.WriteLine("1. Display All Accounts");
                Console.WriteLine("2. Search Account Details");
                Console.WriteLine("3. Deposit Money");
                Console.WriteLine("4. Withdraw Money");
                Console.WriteLine("5. Transfer between Accounts");
                Console.WriteLine("6. Close an Account");
                Console.WriteLine("0. Back to Previous Menu");
                Console.WriteLine("-----------------------");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Display();
                            break;
                        case 2:
                            Console.Clear();
                            Display();
                            Search();
                            break;
                        case 3:
                            Console.Clear();
                            Display();
                            Deposit();
                            break;
                        case 4:
                            Console.Clear();
                            Display();
                            Withdraw();
                            break;
                        case 5:
                            Console.Clear();
                            Display();
                            Transfer();
                            break;
                        case 6:
                            Console.Clear();
                            Display();
                            Close();
                            break;
                        case 0:
                            Console.WriteLine("Returning to Main Menu...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
            while (choice != 0);
        }

        public static void Close()
        {
            Console.WriteLine("Enter the Account Number");
            string accountNumber = Console.ReadLine();
            string response = AccountController.CloseAccount(accountNumber);
            Console.WriteLine(response);

        }
        public static void Search()
        {
            Console.WriteLine("Enter the Source Account Number");
            string accountNumber = Console.ReadLine();

            Account account = twba.GetAllAccounts().Find(a => a.AccountNumber == accountNumber);
            List<Customer> customers = UserController.SearchByAccount(account, twba.GetAllCustomers());
            DisplayCustomerDetails(customers);
            
        }

        /*
         * CWE-20: Improper Input Validation (Partly done - read description for details)
         * Patched By: Bilal
         * Desription: To ensure that the user enters a valid Account number and amount i have done the following checks
         *              1. Check if the account number is valid format wise. Like all digits and length <=10. This should be improved by checking if the account actually exists
         *              2. Check if the provided amount is double. it doesnt check the bounds whether it is valid and less than the amount in the account. This should also be improved.
         */
        public static void Transfer()
        {
            Console.WriteLine("Enter the Source Account Number");
            string sAccountNumber = Console.ReadLine();

            if (UtilityFunctions.IsAccountNumberValid(sAccountNumber)){

                Console.WriteLine("Enter the Amount to be withdrawn in Antarctic Dollars");
                double amount = 0; // Double.Parse(Console.ReadLine());

                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    Console.WriteLine("Enter the Destination Account Number");
                    string dAccountNumber = Console.ReadLine();
                    if (UtilityFunctions.IsAccountNumberValid(dAccountNumber))
                    {

                        Account sourceAccount = AccountController.GetAccountByAccountNumber(sAccountNumber);
                        Account destinationAccount = AccountController.GetAccountByAccountNumber(dAccountNumber);


                        TransactionController.TransferBetweenAccounts(sourceAccount, destinationAccount, amount);


                        Display(sAccountNumber);
                        Display(dAccountNumber);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Account Number provided");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Amount provided");
                }

            }
            else
            {
                Console.WriteLine("Invalid Account Number provided");
            }
        }

        /*
         * CWE-20: Improper Input Validation (Partly done - read description for details)
         * Patched By: Ning Zhang
         * Desription: To ensure the withdrawal process is successful, I made the following code modifications
         *             1. Use UtilityFunctions.IsAccountNumberValid() to verify whether the input account number meets the specifications.
         *             2. Check that the amount entered by the user must be a number and the number cannot be negative.
         */

        /*
         * CWE-190: Integer Overflow or Wraparound
         * Patched By: Ning Zhang
         * Desription: Protection against integer overflow or wraparound
         *             1. Check that the current account balance >= the amount to be transferred
         */
        public static void Withdraw()
        {
            Console.WriteLine("Enter the Account Number");
            string accountNumber = Console.ReadLine();

            // CWE-20:Verify that the account number is valid
            if (UtilityFunctions.IsAccountNumberValid(accountNumber))
            {
                Console.WriteLine("Enter the Amount to be withdrawn in Antarctic Dollars");
                double amount = 0;

                // CWE-20 and CWE-190: Verify that the input amount is a valid positive number
                if (double.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    Account account = AccountController.GetAccountByAccountNumber(accountNumber);

                    if (account != null)
                    {
                        // // CWE-190: Ensure the account balance is sufficient for the withdrawa
                        if (account.AccountBalance >= amount)
                        {
                            TransactionController.Withdrawl(account, amount);
                            Console.WriteLine("Withdrawal successful!");
                            Display(accountNumber);
                        }
                        else
                        {
                            Console.WriteLine("Insufficient balance."); //CWE-190
                        }
                    }
                    else
                    {
                        Console.WriteLine("Account not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Amount provided."); //CWE-20
                }
            }
            else
            {
                Console.WriteLine("Invalid Account Number provided.");
            }
        }
        // END CWE-20 & CWE190

        public static void Deposit()//cwe-20
        {
            Console.WriteLine("Enter the Account Number");
            string accountNumber = Console.ReadLine();
            Console.WriteLine("Enter the Amount to be deposited in Antarctic Dollars");
            double amount = Double.Parse(Console.ReadLine());

            Account account = AccountController.GetAccountByAccountNumber(accountNumber);

            TransactionController.Deposit(account, amount);
            Display(accountNumber);
            
        }

        public static void DisplayCustomerDetails(List<Customer> customers)
        {
               foreach (var account in twba.GetAllAccounts())
                {
                    // Display the account information
                    Console.WriteLine($"Account Number: {account.AccountNumber}");
                    Console.WriteLine("Owners:");

                    // Get the owners of the account
                    var accountOwners = customers
                .Where(customer => account.AccountOwners.Contains(customer.CustomerId))
                .ToList();

                if (accountOwners.Count > 0)
                    {
                        foreach (var customer in accountOwners)
                        {
                            // Display customer details
                            Console.WriteLine($"\tCustomer Id: {customer.CustomerId}");
                            Console.WriteLine($"\tGov Id: {customer.GovId}");
                            Console.WriteLine($"\tName: {customer.Name} {customer.LastName}");
                            Console.WriteLine($"\tEmail: {customer.Email}");
                            Console.WriteLine($"\tPhone Number: {customer.PhoneNumber}");
                            Console.WriteLine($"\tAddress: {customer.Address}");
                            Console.WriteLine($"\tDate of Joining: {customer.DateOfJoining.ToShortDateString()}");
                            Console.WriteLine(); // Blank line for spacing
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tNo owners found for this account.");
                    }
                    Console.WriteLine("---------------------------------");
                }
            }
        

        public static void Display(string accountNumber)
        {
            // Find the account by the given account number
           Account account = AccountController.GetAccountByAccountNumber(accountNumber);

            if (account == null)
            {
                Console.WriteLine($"Account with number {accountNumber} not found.");
                return;
            }

            // Calculate column widths for formatting
            int[] columnWidths = new int[headers.Length];

            // Set column widths based on header length
            for (int i = 0; i < headers.Length; i++)
            {
                columnWidths[i] = headers[i].Length;
            }

            // Adjust column widths based on account data
            columnWidths[0] = Math.Max(columnWidths[0], account.AccountNumber.Length);
            foreach (string accountOwnerId in account.AccountOwners)
            {
                columnWidths[1] = Math.Max(columnWidths[1], accountOwnerId.Length);
            }
            columnWidths[2] = Math.Max(columnWidths[2], account.AccountType.ToString().Length);
            columnWidths[3] = Math.Max(columnWidths[3], account.AccountBalance.ToString("F2").Length);

            // Print headers
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(columnWidths[i] + 2));
            }
            Console.WriteLine();

            // Print separator line
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(new string('-', columnWidths[i] + 2));
            }
            Console.WriteLine();

            // Print the account details
            bool isFirstOwner = true;

            foreach (string accountOwnerId in account.AccountOwners)
            {
                if (isFirstOwner)
                {
                    // Print full row for the first owner
                    Console.Write(account.AccountNumber.PadRight(columnWidths[0] + 2));
                    Console.Write(accountOwnerId.PadRight(columnWidths[1] + 2));
                    Console.Write(account.AccountType.ToString().PadRight(columnWidths[2] + 2));
                    Console.Write(account.AccountBalance.ToString("F2").PadRight(columnWidths[3] + 2)); // Format balance as float
                    Console.WriteLine();
                    isFirstOwner = false;
                }
                else
                {
                    // For subsequent owners, print only the Owner Id
                    Console.Write(new string(' ', columnWidths[0] + 2)); // Skip Account Number column
                    Console.Write(accountOwnerId.PadRight(columnWidths[1] + 2));
                    Console.Write(new string(' ', columnWidths[2] + 2)); // Skip Account Type column
                    Console.Write(new string(' ', columnWidths[3] + 2)); // Skip Account Balance column
                    Console.WriteLine();
                }
            }
        }
            public static void Display() //Set the width of the table
        {
            // Calculate column widths for formatting
            int[] columnWidths = new int[headers.Length];

            // Set column widths based on header length
            for (int i = 0; i < headers.Length; i++)
            {
                columnWidths[i] = headers[i].Length;
            }

            // Adjust column widths based on account data
            foreach (var account in twba.GetAllAccounts())
            {
                columnWidths[0] = Math.Max(columnWidths[0], account.AccountNumber.Length);

                // AccountOwners can have multiple entries
                foreach (string accountOwnerId in account.AccountOwners)
                {
                    columnWidths[1] = Math.Max(columnWidths[1], accountOwnerId.Length);
                }

                columnWidths[2] = Math.Max(columnWidths[2], account.AccountType.ToString().Length);
                columnWidths[3] = Math.Max(columnWidths[3], account.AccountBalance.ToString("F2").Length); // Format as currency or float
            }

            // Print headers
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(columnWidths[i] + 2));
            }
            Console.WriteLine();

            // Print separator line
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(new string('-', columnWidths[i] + 2));
            }
            Console.WriteLine();

            // Print rows
            foreach (var account in twba.GetAllAccounts())
            {
                bool isFirstOwner = true;

                // Print each account owner in its own row, but print the rest of the account details only once
                foreach (string accountOwnerId in account.AccountOwners)
                {
                    if (isFirstOwner)
                    {
                        // Print full row for the first owner
                        Console.Write(account.AccountNumber.PadRight(columnWidths[0] + 2));
                        Console.Write(accountOwnerId.PadRight(columnWidths[1] + 2));
                        Console.Write(account.AccountType.ToString().PadRight(columnWidths[2] + 2));
                        Console.Write(account.AccountBalance.ToString("F2").PadRight(columnWidths[3] + 2)); // Formatting balance as floating point
                        Console.WriteLine();
                        isFirstOwner = false;  // Only print full row once
                    }
                    else
                    {
                        // For subsequent owners, print only the Owner Id
                        Console.Write(new string(' ', columnWidths[0] + 2)); // Skip Account Number column
                        Console.Write(accountOwnerId.PadRight(columnWidths[1] + 2));
                        Console.Write(new string(' ', columnWidths[2] + 2)); // Skip Account Type column
                        Console.Write(new string(' ', columnWidths[3] + 2)); // Skip Account Balance column
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
