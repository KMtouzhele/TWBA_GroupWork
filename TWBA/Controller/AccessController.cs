using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Utility;

namespace TheWeakestBankOfAntarctica.View
{
    public class AccessController
    {

        public static bool IsLoggedIn { get; private set; } // A global state that tells us if the user is authenticated or not
        public static string LoggedInUser { get; private set; } // username of the authenticated used if they are authenticated otherwise null;
        public static string Username { get; private set; }  // Stores the username of the user upon successful login.
        public static string Password { get; private set; }  //  // Stores the password of the user upon successful login.

        /* CWE-798 : Use of Hard coded Credentials
          * Patched by : Bilal
          * Description : 1. I have stored the login and password (which is Bob and Banana for this example respectively) in AppConfig.xml file lets call it storedHash
          *               2. I have created a method in UtilityFunctions.cs -> CreateHash that takes login and password, creates a strong hash for both lets call it createHashBasedOnUserInput
          *               3. I have created another method in UtilityFunctions.cs -> GetValueFromAppConfig that takes "hash" key and returns the strong hash stored in the App.xml (storedHash)
          *               4. If storedHash == createHashBasedOnUserInput, the user is Authenticated without hardcoding the login password anywhere in the system. 
          *               5. For example if you dont know the login is Bob and password is Banana, you cant access the system.
          */
        public static bool Login(string login, string password)
        {
            string createHashBasedOnUserInput = UtilityFunctions.CreateHash(login, password);
            string storedHash = UtilityFunctions.GetValueFromAppConfig("hash");

            if (createHashBasedOnUserInput.Equals(storedHash))
            {
                IsLoggedIn = true;
                LoggedInUser = login;
                // Set the login state and store the authenticated username.
                Username = login;   
                Password = password;
                // Store the username and password in Username and Password variables.
                return true; // Login successful
            }
            IsLoggedIn = false;
            LoggedInUser = null;
            return false;
        }

        public static bool Logout()
        {
            IsLoggedIn = false ;
            LoggedInUser = null;
            Username = null;   // Clear stored username
            Password = null;   // Clear stored password
            return true;
        }
    }
}


   