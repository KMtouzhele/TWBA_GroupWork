using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWeakestBankOfAntarctica.Model
{
    public class Account
    {
        public Account() { }
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public TypeOfAccount AccountType { get; set; }
        public List<string> AccountOwners = new List<string>(); //Gov Ids of the Owners

        private static readonly Random random = new Random();


        public Account(TypeOfAccount type, string customerId, double initialBalance)
        {
            AccountNumber = GenerateAccountNumber();
            AccountType = type;
            AccountOwners.Add(customerId);
            AccountBalance = initialBalance;
        }

        public string AddOwner(string customerId)
        {
            AccountOwners.Add(customerId);
            return customerId;
        }

        private string GenerateAccountNumber()
        {
            string accountNumber = "";

            for (int i = 0; i < 10; i++) // length of the account number is 10 digits
            {
                accountNumber += random.Next(10).ToString();
            }

            return accountNumber;
        }

        public bool Deposit(string accountNumber, double amount)
        {
            AccountBalance = AccountBalance + amount;
            return true;
        }

        public bool Withdraw(string accountNumber, double amount)
        {
            AccountBalance = AccountBalance - amount;
            return true;
        }

    }

    public enum TypeOfAccount
    {
        Savings, Checking
    }
}
