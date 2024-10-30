using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWeakestBankOfAntarctica.Model
{
    class Admin : User
    {
        public DateTime StartDate { get; set; }
        public Position Position { get; set; }
        public Role Role { get; set; }
        public string BranchName { get; set; }
        public string BranchId { get; set; }
        public string AdminId { get; private set; }

        public Admin(string govId, string name, string lName, string email, string password,
            Position position, Role role, string branchName, string branchId,
            string address, string phoneNumber) :
            base(govId, name, lName, email, password, address, phoneNumber)
        {
            base.GovId = govId;
            base.Name = name;
            base.LastName = lName;
            base.Email = email;
            base.Password = password;
            base.Address = Address;
            base.PhoneNumber = phoneNumber;

            StartDate = DateTime.Now;
            Position = position;
            Role = role;
            BranchName = branchName;
            BranchId = branchId;
            AdminId = GenerateAdminId(8); // maximum length of an Id must be 8 characters
        }

        public string GenerateAdminId(int max)
        {
            Random random = new Random();
            string id = "";

            for (int i = 0; i < max; i++)
            {
                id += random.Next(10).ToString();
            }

            return BranchId + "-" + id + "A";
        }
    }

    public enum Position
    {
        manager, specialist, attendent, representative, none
    }

    public enum Role
    {
        Admin, Teller, none
    }

}
