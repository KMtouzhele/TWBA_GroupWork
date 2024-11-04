using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Data;
using TheWeakestBankOfAntarctica.Model;

namespace TheWeakestBankOfAntarctica.Controller
{
    public static class AccountController
    {
        public static void CreateNewSavingsAccount(Customer customer, double balance)
        {
            Account account = new Account(TypeOfAccount.Savings, customer.CustomerId, balance);
        }

        public static void CreateNewCheckingAccount(Customer customer, double balanace)
        {
            Account account = new Account(TypeOfAccount.Checking, customer.CustomerId, balanace);
        }

        public static List<Account> GetAllAccountsByCustomerOfficialId(string govId)
        {
            List<Account> accounts = DataAdapter.GetAccountOwners(govId);
            return accounts;
        }


        public static Account GetAccountByAccountNumber(string accountNumber)
        {
           return DataAdapter.GetAccountByAccountNumber(accountNumber);
        }

        public static string CloseAccount(string accountNumber)
        {
            string response = DataAdapter.CloseAccount(accountNumber);
            return response;
        }
    }
}
