using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWeakestBankOfAntarctica.Model
{
    public class BalanceRecord
    {
        public string AccountNumber { get; }
        public List<Customer> AccountOwners = new List<Customer>();
        public double Balance { get; }

        public BalanceRecord(string accountNumber, List<Customer> customers, double balance)
        {
            AccountNumber = accountNumber;
            AccountOwners = customers;
            Balance = balance; 
        }
    }
}
