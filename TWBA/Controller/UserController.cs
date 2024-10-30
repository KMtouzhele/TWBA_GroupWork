using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Data;
using TheWeakestBankOfAntarctica.Model;
using TheWeakestBankOfAntarctica.Utility;

namespace TheWeakestBankOfAntarctica.Controller
{
    static class UserController
    {
        public static string CreateCustomer(string govId, string name, string lName,
           string email, string password, string address, string phoneNumber, double initialBalance)
        {
           // SQLiteDB db = new SQLiteDB();
            Customer customer = new Customer(govId, name, lName, email, password,
                address, phoneNumber);
           // XmlAdapter.SerializeCustomerDataToXml(customer);
            // Account account = new Account(initialBalance, customer.CustomerId);
            //db.AddNewCustomer(customer.CustomerId, govId, name, lName, email,
             //   password, address, phoneNumber);
            return customer.CustomerId;
        }

        public static List<Customer> SearchByAccount(Account account, List<Customer> customers)
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

        /* CWE-862: Missing Authorization
         * Patched by : Kaimo Li
         * Description: 1. I have implemented a method in UtilityFunctions.cs -> IsUserAuthorizedToAddAdmin that checks if the user is authorized to add an admin
         *              2. I only allow the user to add an admin if they are authorized, otherwise I return "You are not authorized"
         */
        public static string CreateAdminUser(string govId, string name, string lName, string email,
            string password, string branchName, string branchId, string address,
            string phoneNumber)
        {
            bool isAuthoorized = UtilityFunctions.IsUserAuthorizedToAddAdmin(govId);
            if (!isAuthoorized)
            {
                return "You are not authorized";
            }
            //  SQLiteDB db = new SQLiteDB();
            Admin admin = new Admin(govId, name, lName, email, password, Position.manager,
                Role.Admin, branchName, branchId, address, phoneNumber);
            return admin.AdminId;
        }

    }
}
