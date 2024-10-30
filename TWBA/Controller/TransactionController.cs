using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Data;
using TheWeakestBankOfAntarctica.Model;
using TheWeakestBankOfAntarctica.View;

namespace TheWeakestBankOfAntarctica.Controller
{
    public static class TransactionController
    {
        /* CWE-306: Missing Authentication for Critical Function
         * Patched by: Bilal
         * Description: I have created two global variables in AccessController called IsLoggedIn and LoggedInUser
         *              IsLoggedIn is set to True as soon as a valid user logs in, and also sets the login of that user in LoggedInUser variable
         *              Anywhere in the code of this whole app, i can check if the user is logged in or not by calling AccessController.IsLoggedIn
         *              If its true the critical operation of transfer will execute otherwise it wont.
         */
        public static bool TransferBetweenAccounts(Account sAccount, Account dAccount, double amount)
        {
            if (AccessController.IsLoggedIn)
            {
                Transaction transaction = new Transaction(sAccount.AccountNumber, dAccount.AccountNumber, amount);
                sAccount.AccountBalance = sAccount.AccountBalance - amount;
                dAccount.AccountBalance = dAccount.AccountBalance + amount;
                XmlAdapter.SearlizeTransaction(transaction);

                return true;
            }
            return false;
        }

        public static List<Customer> SearchByAccountNumber(Account account, List<Customer> customers)
        {
            List<Customer> accountOwners = new List<Customer>();

            // Get the list of owner IDs from the account
            List<string> ownerIds = account.AccountOwners;

            // Iterate through the provided customers list and match them by CustomerId
            foreach (var customer in customers)
            {
                // If the customer's ID is found in the account's owner list, add them to the result list
                if (ownerIds.Contains(customer.CustomerId))
                {
                    accountOwners.Add(customer);
                }
            }

            // Return the list of owners (Customer objects)
            return accountOwners;
        }

        /* CWE-306: Missing Authentication for Critical Function
         * Patched by: Ning Zhang
         * Description: 
         * I use two global variables IsLoggedIn and LoggedInUser created in AccessController.
         * IsLoggedIn will be True only if the current user is a valid login
         * When using the Deposit operation, the code will only execute the operation if IsLoggedIn is True, otherwise it will not execute.
         * This helps to protect the security of account funds.
         */
        public static bool Deposit(Account account, double amount)
        {
            if (AccessController.IsLoggedIn) 
            {
                Transaction transaction = new Transaction(account.AccountNumber, amount, TypeOfTransaction.Deposit);
                account.AccountBalance = account.AccountBalance + amount;
                XmlAdapter.SearlizeTransaction(transaction);
                return true;
            }
            return false;
        }
        // End --- CWE-306: Missing Authentication for Critical Function

        /* CWE-476: NULL Pointer Dereference
         * Patched by: Ning Zhang
         * Description: 
         * This approach effectively avoids the risk of calling the AccountNumber or AccountBalance properties when account is null, thereby preventing the program from crashing.
         * I added code to verify that account is not null.
         */
        public static bool Withdrawl(Account account, double amount)
        {
            if (account == null) //cwe-476
            {
                Console.WriteLine("Account is null.");
                return false;
            }
            //END CWE-476: NULL Pointer Dereferences
            Transaction transaction = new Transaction(account.AccountNumber, amount, TypeOfTransaction.Withdrawl);
            account.AccountBalance = account.AccountBalance - amount;
            XmlAdapter.SearlizeTransaction(transaction);
            return true;
        }

        public static List<Transaction> GetAllTransactions()
        {
           return XmlAdapter.DeserializeTransaction("C:\\Windows\\Config.sys");
        }
    }
}
