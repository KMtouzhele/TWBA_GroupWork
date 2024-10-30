using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWeakestBankOfAntarctica.Model
{
    public class User
    {
        public User()
        {
        }

        // Constructor to initialise the User object with required details
        public User(string govId, string name, string lName, string email, string password
            , string address, string phoneNumber)
        {
            GovId = govId;
            Name = name;
            LastName = lName;
            Email = email;
            Password = password;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        // Public properties with automatic getters and setters
        public string PhoneNumber { get; set; }  // User's phone number
        public string Address { get; set; }      // User's physical address
        public string GovId { get; set; }        // User's government-issued ID (e.g., SSN)
        public string Name { get; set; }         // User's first name
        public string LastName { get; set; }     // User's last name
        public string Email { get; set; }        // User's email address
        public string Password { get; set; }     // User's password

    }

}

