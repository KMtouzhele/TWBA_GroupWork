using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Data;

namespace TheWeakestBankOfAntarctica.Model
{
    public class TWBA
    {
        private List<Customer> listOfCustomers = null;
        private List<Account> listOfAccounts = null;
        private List<Transaction> listOfTransactions = null;

        public TWBA()
        {
            listOfCustomers = Data.FakeData.CreateCustomers();
            XmlAdapter.SerializeCustomerDataToXml(listOfCustomers);

            listOfAccounts = Data.FakeData.FakeAccounts();
            bool isDone = XmlAdapter.SerializeAccountDataToXml(listOfAccounts);
        }

        public List<Account> GetAllAccounts()
        {
            return listOfAccounts;
        }

        public List<Customer> GetAllCustomers()
        {
            return listOfCustomers;
        }

        internal void AddCustomers(Customer customer)
        {
            listOfCustomers.Add(customer);
        }

        internal void AddAdmin(Admin admin)
        {
           // listOfAdmins.Add(admin);
        }

    }
}
