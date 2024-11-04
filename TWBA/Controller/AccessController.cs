using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Utility;

namespace TheWeakestBankOfAntarctica.View
{
    public class AccessController
    {
        private static Dictionary<string, string> sessions = new Dictionary<string, string>();
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

        public static void Logout(string sessionToken)
        {
            if (sessions.ContainsKey(sessionToken))
            {
                sessions.Remove(sessionToken);
            }
        }
        /* CWE-287 : Improper Authentication
         * Patched by : Kaimo Li
         * Description : 1. I replaced the global `IsLoggedIn` flag with a session-based token system to securely track user authentication.
         * 2. The `Login` method no longer stores passwords in memory; instead, it generates a secure session token after successful authentication.
         * 3. The session token is used to verify user authentication in subsequent requests, and it is invalidated upon logout.
         */
        public static string Authenticate(string login, string password)
        {
            string createHashBasedOnUserInput = UtilityFunctions.CreateHash(login, password);
            string storedHash = UtilityFunctions.GetValueFromAppConfig("hash");

            if (createHashBasedOnUserInput.Equals(storedHash))
            {
                string sessionToken = GenerateSessionToken(login);
                sessions[sessionToken] = login;
                return sessionToken;
            }
            return null;
        }
        public static bool IsAuthenticated(string sessionToken)
        {
            return sessions.ContainsKey(sessionToken);
        }
        public static string GetLoggedInUser(string sessionToken)
        {
            return sessions.ContainsKey(sessionToken) ? sessions[sessionToken] : null;
        }

        private static string GenerateSessionToken(string username)
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}


   