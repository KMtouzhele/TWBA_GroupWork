using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWeakestBankOfAntarctica.Model
{
    public class Transaction
    {
        public Transaction() { }
        public string DestinationAccountNumber { get; set; }
        public string SourceAccountNumber { get; set; }
        public string TransactionDescription { get; set; }
        public TypeOfTransaction TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionId { get; set; }

        public Transaction(string dAccount, double amount, TypeOfTransaction transactionType)
        {
            if (transactionType == TypeOfTransaction.Deposit)
            {
                TransactionType = TypeOfTransaction.Deposit;
                DestinationAccountNumber = dAccount;
                TransactionAmount = amount;
                TransactionDate = DateTime.Now;
                TransactionId = GenerateTransactionId();
                TransactionDescription = "Deposit on:"+TransactionDate + 
                    "- Destination Account Number" + dAccount +
                    ", Total Amount: " + TransactionAmount;
            }
            else
            {
                TransactionType = TypeOfTransaction.Withdrawl;
                DestinationAccountNumber = dAccount;
                TransactionAmount = amount;
                TransactionDate = DateTime.Now;
                TransactionId = GenerateTransactionId();
                TransactionDescription = "Withdrawl on:"+TransactionDate + 
                    "- Destination Account Number" + dAccount +
                    ", Total Amount: " + TransactionAmount;

            }
        }

        //Transaction for transfer
        public Transaction(string sAccount, string dAccount, double amount)
        {
            TransactionType = TypeOfTransaction.BankTransfer;
            SourceAccountNumber = sAccount;
            DestinationAccountNumber = dAccount;
            TransactionAmount = amount;
            TransactionDate = DateTime.Now;
            TransactionId = GenerateTransactionId();
            TransactionDescription = "Transfer on:"+TransactionDate + 
                "- Source Account Number:"+SourceAccountNumber+
                " - Destination Account Number:" + dAccount+" , Total Amount:"+TransactionAmount;
        }

        private string GenerateTransactionId()
        {
            Random random = new Random();
            string tNumber = "";

            for (int i = 0; i < 10; i++) // maximum length of a trasaction number
            {
                tNumber += random.Next(10).ToString();
            }

            return tNumber;

        }
    }
    public enum TypeOfTransaction
    {
        Withdrawl, Deposit, BankTransfer
    }
}
