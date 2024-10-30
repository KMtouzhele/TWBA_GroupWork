using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWeakestBankOfAntarctica.Model
{
    public class Customer : User
    {
        public Customer():base( ) { }
        // Property for Customer Id with private setter (read-only outside of the class)
        public string CustomerId { get; set; }

        // Property for Customer date when he openned their first account
        public DateTime DateOfJoining { get; set; }

        // Constructor for Customer class that calls the base User constructor
        public Customer(string govId, string name, string lName, string email, string password,
            string address, string phoneNumber) :
            base(govId, name, lName, email, password, address, phoneNumber)
        {
            base.GovId = govId;
            base.Name = name;
            base.LastName = lName;
            base.Email = email;
            base.Password = password;
            base.Address = address;
            base.PhoneNumber = phoneNumber;
            DateOfJoining = DateTime.Now;

            // Generate a unique Customer Id when a new Customer is created
            CustomerId = GenerateUserId(10);
        }

        

        // Override of the ToString() method to provide a custom string representation of a Customer
        public override string ToString()
        {
            return $"{GovId}-{LastName}, {Name}";
        }

        // Private method to generate a random user Id of a specified length = 10
        private string GenerateUserId(int maxLength)
        {
            var random = new Random();
            var userId = new System.Text.StringBuilder(maxLength);

            // Generate random digits for the user ID
            for (int i = 0; i < maxLength; i++)
            {
                userId.Append(random.Next(10)); // Append a random digit (0-9)
            }

            return userId.ToString(); // Return the generated Id as a string

        }

    }
}
