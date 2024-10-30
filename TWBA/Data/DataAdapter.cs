using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Model;
using TheWeakestBankOfAntarctica.View;

namespace TheWeakestBankOfAntarctica.Data
{
    public static class DataAdapter
    {
        private static TWBA twba = null;

        public static void Init(TWBA model)
        {
            twba = model;
        }

        static void ConnectToRemoteDB()
        {
            string server = "Bank.db";
            string database = "TWBA";
            //string username = "Bob";
            //string password = "Banana";   
            // The original hard-coded username and password have been removed and replaced with dynamic retrieval
            string username = AccessController.Username; 
            string password = AccessController.Password;
            // Use the login username and password, retrieved dynamically from AccessController
            // Credentials are input in Program.cs, validated in AccessController, and used here in DataAdapter


            /* CWE-798 : Use of Hard coded Credentials
             * Patched by : Fangzheng Zhao
             * Description : 
             * 1. In Program.cs:
             *    The program prompts the user to enter a login name and password. These credentials are stored in variables `login` and `password`.
             *    The credentials are then passed to AccessController.Login for validation. If validation is successful, they are securely stored for later use.
             * 
             * 2. In AccessController:
             *    Upon successful login, the entered login name and password are stored in static variables `Username` and `Password`.
             *    These variables make it possible for other classes, like DataAdapter, to access the validated credentials without hardcoding.
             * 
             * 3. In DataAdapter:
             *    The original hard-coded username and password have been removed and replaced with dynamic retrieval
             *    Use the login username and password, retrieved dynamically from AccessController
             *    Ensure that the username and password are not null to verify that the user is logged in
             */
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))  // Ensure that the username and password are not null to verify that the user is logged in
            {
                Console.WriteLine("User is not logged in.");
                return;
            }

            string connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
            try
            {

                Console.WriteLine("Connected to MySQL server!");

                // We arent using the Remote DB here; however, this is a valid code in the App

            }
            catch (Exception ex) { }
        }
        // END CWE-798

        public static Account GetAccountByAccountNumber(string accountNumber)
        {
            Account account = (from a in twba.GetAllAccounts()
                       where a.AccountNumber == accountNumber
                       select a).FirstOrDefault();

            return account; 
        }

        public static List<Account> GetAccountOwners(string customerId)
        {
            List<Account> customerAccounts = (from account in twba.GetAllAccounts()
                                    where account.AccountOwners.Contains(customerId)
                                    select account).ToList();

            return customerAccounts;
        }
        /* CWE-476 NULL Pointer Dereference
        * Patched by : Kaimo Li
        * Description: 1. As GetAccountByAccountNumber may not have a valid account, I have added a null check to ensure that the account is not null before removing it
        *              2. If the deleting accountNumber does not match an account object, I return "Account not found"
        *              3. If the account is found, I remove it from the list of accounts and return a success message.
        */
        public static string CloseAccount(string accountNumber)
        {
            Account account = GetAccountByAccountNumber(accountNumber);
            if (account == null)
            {
                return "Account not found";
            }
            twba.GetAllAccounts().Remove(account);
            return account.AccountNumber + " has been removed successfully!";
        }
    }
}
