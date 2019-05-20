using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking myParking = new Parking();

            Console.WriteLine("Hello in my parking lot!'\n" +
                "1) Find out the current balance of parking\n" +
                "2) The amount of money earned in the last minute\n" +
                "3) Find out the number of free / busy places in the parking lot\n" +
                "4) Show all transactions in the last minute\n" +
                "5) Print the entire Transaction history (after reading the data from the Transactions.log file)\n" +
                "6) Display the list of all vehicles\n" +
                "7) Createthe vehicle\n" +
                "8) Remove parking vehicle\n" +
                "9) Refill the balance of a particular vehicle\n");

            string input;
            
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine(myParking.Balance);
                        break;
                    case "2":
                        myParking.EarnedMoneyForLastMinute();
                        break;
                    case "3":
                        myParking.ShowPlaces();
                        break;
                    case "4":
                        myParking.ShowLastTransactions();
                        break;
                    case "5":
                        myParking.ShowAllTransactions();
                        break;
                    case "6":
                        myParking.ShowCar();
                        break;
                    case "7":
                        myParking.addCar();
                        break;
                    case "8":
                        myParking.RemoveCar();
                        
                        break;
                    case "9":
                        myParking.AddMoney();
                        break;
                }
            } while (true);
        }
    }
}
