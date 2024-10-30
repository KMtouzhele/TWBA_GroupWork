using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Controller;
using TheWeakestBankOfAntarctica.Model;

namespace TheWeakestBankOfAntarctica.View
{
    static class TransactionView
    {
        private static TWBA twba = null;
        public static void TransactionMenu(TWBA model)
        {
                twba = model;
                int choice;
                do
                {
                    Console.Clear();
                    Console.WriteLine("*****Account Menu******");
                    Console.WriteLine("1. Display All Transactions");
                    Console.WriteLine("2. Search Transactions by Transaction Number");
                    Console.WriteLine("3. Search Transaction by Account Number");
                    Console.WriteLine("0. Back to Previous Menu");
                    Console.WriteLine("-----------------------");
                    Console.Write("Enter your choice: ");

                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                Display();
                                break;
                            case 2:
                                Console.Clear();
                            //    Display();
                                //Search();
                                break;
                            case 3:
                                Console.Clear();
                              //  Display();
                              //  Deposit();
                                break;
                            case 4:
                                Console.Clear();
                              //  Display();
                                //Withdraw();
                                break;
                            case 5:
                                Console.Clear();
                              //  Display();
                              //  Transfer();
                                break;

                            case 0:
                                Console.WriteLine("Returning to Main Menu...");
                                break;
                            default:
                                Console.WriteLine("Invalid choice, please try again.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid number.");
                    }

                    if (choice != 0)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }
                while (choice != 0);

            }

        private static void Display()
        {
           List<Transaction> transactions = TransactionController.GetAllTransactions();

           foreach (Transaction transaction in transactions)
            {
                Console.WriteLine(transaction.TransactionDescription);
            }
        }

        }
}
