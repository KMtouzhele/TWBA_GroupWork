using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TheWeakestBankOfAntarctica.Utility
{
    internal static class UtilityFunctions
    {
        public static bool IsAccountNumberValid(string accountNumber)
        {
            return accountNumber.Length <= 10 && accountNumber.All(char.IsDigit);
        }
        public static bool IsPathValid(string path)
        {
           if (string.IsNullOrEmpty(path))
           {
                    throw new ArgumentException("The provided path is null or empty.");
           }

           // Check if the path contains '*' or '?' wildcard characters, this prevents traversal in the directory
           return path.Contains('*') || path.Contains('?');
        }
        public static string CreateHash(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Login or Password values must not be null or empty.");
            }

            string credentials = login + password;

            // Create a SHA512 hash
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(credentials));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert byte to hexadecimal string
                }
                return builder.ToString();
            }
        }

        public static string GetValueFromAppConfig(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key must not be null or empty.");
            }

            // Load the XML configuration file
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppConfig.xml"); // Path to your XML file
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException("The configuration file was not found.");
            }

            XDocument configDocument = XDocument.Load(configFilePath);

            // Query the XML file for the value associated with the specified key
            var settingElement = configDocument.Descendants("add")
                .FirstOrDefault(element => element.Attribute("key")?.Value == key);

            return settingElement?.Attribute("value")?.Value;
        }

        public static bool IsUserAuthorizedToAddAdmin(string govId)
        {
            // It will not be always false in real world scenario
            return false;
        }




    }
}
