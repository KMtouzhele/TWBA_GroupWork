using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Data;
using TheWeakestBankOfAntarctica.Model;
using TheWeakestBankOfAntarctica.View;

namespace TheWeakestBankOfAntarctica
{
    public class Program
    {
        static TWBA mainSystem =null;
        static void Main(string[] args)
        {
            if (Login())
            {
                mainSystem = new TWBA();
                DataAdapter.Init(mainSystem);
                Program.MainMenu();
            }
            else
            {
                Console.WriteLine("Access Denied!!");
            }
        }
        /* CWE-522 : Insufficiently Protected Credentials
         * Patched by : Kaimo Li
         * Description :1. I use ConsoleKey in the method and set intercept to true to mask the password with “*” characters.
         *              2. The password is not displayed on the screen after patching.
         */
        private static bool Login()
        {
            Console.WriteLine("Please enter the login value:");
            string login = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            string sessionToken = AccessController.Authenticate(login, password);
            bool isAuthenticated = AccessController.IsAuthenticated(sessionToken);

            return isAuthenticated;  // CWE-798
            // This method prompts the user to enter a login name and password, then calls AccessController to validate credentials.
            // The input values are stored in the variables 'login' and 'password' for use in authentication.
        }

        //private static bool Login()
        //{
        //    Console.WriteLine("Please enter the login value");
        //    string login = Console.ReadLine();
        //    Console.WriteLine("Please enter your password");
        //    string password = Console.ReadLine();
        //    return AccessController.Login(login, password); // CWE-798 
            
        //}


        private static void MainMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to The Weakest Bank of Antarctica");
                Console.WriteLine("****Main Menu****");
                Console.WriteLine("1. Accounts");
                Console.WriteLine("2. Customers");
                Console.WriteLine("3. Transactions");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AccountView.AccountMenu(mainSystem);
                            //  OpenBankAccount();
                            break;
                        case 2:
                            AdminView.UserMenu();
                            break;
                        case 3:
                            TransactionView.TransactionMenu(mainSystem);
                            break;
                        case 4:
                            //  WithdrawMoney();
                            break;
                        case 5:
                            //  CheckAccountBalance();
                            break;
                        case 0:
                            Console.WriteLine("Exiting the program...");
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

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            while (choice != 0);
        }
    }
}
