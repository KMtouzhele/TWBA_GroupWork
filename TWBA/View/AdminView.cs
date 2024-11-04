using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Controller;
using TheWeakestBankOfAntarctica.Model;
using TheWeakestBankOfAntarctica.Utility;

namespace TheWeakestBankOfAntarctica.View
{
    public static class AdminView
    {
        public static void UserMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Customer Accounts");
                Console.WriteLine("2. Account Balance");
                Console.WriteLine("3. Account Menu");
                Console.WriteLine("0. Go Back to Main Menu");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            GetCustomerAccounts();
                            break;
                        case 2:
                            GetCustomerBalance();
                            break;
                        case 3:
                            AccountMenu();
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

        private static void GetCustomerAccounts()
        {
            Console.WriteLine("Enter Customer Official Id");
            string govId = Console.ReadLine();
            List<Account> accounts = AccountController.GetAllAccountsByCustomerOfficialId(govId);
            foreach(Account account in accounts)
            {
                AccountView.Display(account.AccountNumber);
            }
        }

        private static void GetCustomerBalance()
        {
            Console.WriteLine("Enter Customer Official Id");
            string govId = Console.ReadLine();
            List<Account> accounts = AccountController.GetAllAccountsByCustomerOfficialId(govId);
            double balance = 0;
            for (int i = 0; i <= accounts.Count; i++)
            {
                balance = balance + accounts[i].AccountBalance;
            }
        }

        private static void CreateNewCustomer()
        {
            Console.WriteLine("Please enter the First name");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter the Last name");
            string lName = Console.ReadLine();
            Console.WriteLine("Please enter the government provided identification number");
            string govId = Console.ReadLine();
            Console.WriteLine("Please enter your first time password");
            string password = Console.ReadLine();
            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine();
            Console.WriteLine("Please provide your home address");
            string homeAddress = Console.ReadLine();
            Console.WriteLine("Please provide your phone number");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("What is your Initial Balance?");
            double initialBalance = Console.Read();

            UserController.CreateCustomer(govId, name, lName, email, password, homeAddress, phoneNumber, initialBalance);
        }
        /* CWE-862: Missing Authorization
         * Patched by : Kaimo Li
         * Description: 1. I have implemented a method in UtilityFunctions.cs -> IsUserAuthorizedToAddAdmin that checks if the user is authorized to add an admin
         *              2. I only allow the user to add an admin if they are authorized, otherwise I return "You are not authorized"
         */
        private static void AddNewEmployee()
        {
            bool isAuthorized = UtilityFunctions.IsUserAuthorizedToAddAdmin("");
            if (!isAuthorized)
            {
                Console.WriteLine("You are not authorized to add an admin");
                return;
            }
            Console.WriteLine("Please enter the First name");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter the Last name");
            string lName = Console.ReadLine();
            Console.WriteLine("Please enter the government provided identification number");
            string govId = Console.ReadLine();
            Console.WriteLine("Please enter your first time password");
            string password = Console.ReadLine();
            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine();
            Console.WriteLine("Please provide your home address");
            string homeAddress = Console.ReadLine();
            Console.WriteLine("Please provide your phone number");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Please provide the Branch Name");
            string branchName = Console.ReadLine();
            Console.WriteLine("Please provode the Branch Id");
            string branchId = Console.ReadLine();

            UserController.CreateAdminUser(govId, name, lName, email, password, branchName
                , branchId, homeAddress, phoneNumber);
        }

        private static void AccountMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("********Account Menu********");
                Console.WriteLine("1. Create a New Account");
                Console.WriteLine("2. Deposit in an Account");
                Console.WriteLine("3. Withdraw from an Account");
                Console.WriteLine("4. Transfer between Accounts");
                Console.WriteLine("5. Add a customer to an Account");
                Console.WriteLine("6. Remove a customer from an Account");
                Console.WriteLine("7. Close an Account");
                Console.WriteLine("0. Previous menu");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddNewEmployee();
                            break;
                        case 2:
                            AddNewEmployee();
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
    }
}

