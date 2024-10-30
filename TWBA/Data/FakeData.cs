using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Controller;
using TheWeakestBankOfAntarctica.Model;

namespace TheWeakestBankOfAntarctica.Data
{
    public static class FakeData
    {
      
        public static List<Account> FakeAccounts()
        {
            List<Account> accounts = new List<Account>();

            Account account1 = new Account(TypeOfAccount.Savings, "123-456-7890", 1200.00);
            account1.AddOwner("116-753-0261");
            accounts.Add(account1);
            

            Account account2 = new Account(TypeOfAccount.Savings, "111-476-5690", 2789.00);
            account2.AddOwner("116-753-0261");
            accounts.Add(account2);

            Account account3 = new Account(TypeOfAccount.Savings, "321-654-0594", 743.00);
            account3.AddOwner("116-753-0261");
            accounts.Add(account3);

            Account account4 = new Account(TypeOfAccount.Checking, "321-654-0594", 1876.00);
            accounts.Add(account4);

            Account account5 = new Account(TypeOfAccount.Checking, "611-345-1731", 9656.00);
            accounts.Add(account5);

            Account account6 = new Account(TypeOfAccount.Checking, "116-753-0261", 5476.00);
            accounts.Add(account6);

            return accounts;

        }
        public static List<Customer> CreateCustomers()
        {
            List<Customer> customers = new List<Customer>();
            
            Customer newCustomer = new Customer("123-456-7890", "June", "Coldplay", 
                "cold.june@coldmail.com","catchMeOnaSunnyDay",
                "-13 coldest Ave, Eastern Shore, Antarctica","098-765-4321");
            customers.Add(newCustomer);


            newCustomer = new Customer("116-753-0261", "James", "Coldplay",
                "cold.james@coldmail.com", "catchMeOnaFunnyDay",
                "-13 coldest Ave, Eastern Shore, Antarctica", "098-765-4321");
            customers.Add(newCustomer);

            newCustomer = new Customer("321-654-0594", "Jude", "Coldplay",
               "cold.jude@coldmail.com", "catchMeOnaRainyDay",
               "-13 coldest Ave, Eastern Shore, Antarctica", "098-765-4321");
            customers.Add(newCustomer);

            newCustomer = new Customer("611-345-1731", "Jeremy", "Coldplay",
                "cold.jeremy@coldmail.com", "catchMeOnaScaryDay",
                "-13 coldest Ave, Eastern Shore, Antarctica", "098-765-4321");
            customers.Add(newCustomer);

            newCustomer = new Customer("111-476-5690", "Jade", "Iceberg",
               "icy.jade@coldmail.com", "catchMeOnaGlacier",
               "-27 Frozen Ave, Glacier Top, Antarctica", "111-342-0989");
            customers.Add(newCustomer);

            newCustomer = new Customer("116-753-0261", "Julie", "Iceberg",
                "cold.james@coldmail.com", "iAmUnCatchable",
                "-27 Frozen Ave, Glacier Top, Antarctica", "451-121-7823");
            customers.Add(newCustomer);

            newCustomer = new Customer("321-654-0594", "Jermiah", "Iceberg",
               "cold.jude@coldmail.com", "catchMeOnaSlipperyDay",
               "-27 Frozen Ave, Glacier Top, Antarctica", "101-873-7119");
            customers.Add(newCustomer);
            
            newCustomer = new Customer("611-345-1731", "Joseph", "Iceberg",
                "cold.jeremy@coldmail.com", "catchMeOnaIceyDay",
                "-27 Frozen Ave, Glacier Top, Antarctica", "923-423-1174");
            customers.Add(newCustomer);

            return customers;
        }

        private static void CreateNewEmployees()
        {

        }
    }
}
