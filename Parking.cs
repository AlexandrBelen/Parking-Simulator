using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Parking
    {
        double _Balance;
        int _MaxAmount;
        int _Periodicity;
        double _CoefficientOfFine;
        int _TimeReport;

        System.Threading.Timer Timer;
        public Parking()
        {

            _Balance = 0;
            _MaxAmount = 10;
            _Periodicity = 5;
            _CoefficientOfFine = 2.5;
            _TimeReport = 10;
            Timer = new System.Threading.Timer(withdrawMmoney, null, 0, _Periodicity * 1000);
            Timer = new System.Threading.Timer(report, null, 0, _TimeReport * 1000);
            Timer = new System.Threading.Timer(WriteData, null, 0, 60000);
        }

        List<string> reportTransfer = new List<string>();
        List<CarType> listCar = new List<CarType>();
        List<Transaction> transactions = new List<Transaction>();

        public double Balance { get => _Balance; set => _Balance += value; }
        public int MaxAmount { get => _MaxAmount; set => _MaxAmount = value; }
        public int Periodicity { get => _Periodicity; set => _Periodicity = value; }
        public double CoefficientOfFine { get => _CoefficientOfFine; set => _CoefficientOfFine = value; }

        public void ShowPlaces()
        {
            Console.Write("Enter what places you want to know:\n1) free\n2) busy\n");
            try
            {
                int ind = Convert.ToInt32(Console.ReadLine());
                if (ind == 1)
                {
                    Console.WriteLine(MaxAmount - listCar.Count);
                }
                else if (ind == 2)
                {
                    Console.WriteLine(listCar.Count);
                }
                else
                {
                    Console.WriteLine("You must enter 1 or 2!!!");
                }

            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void addCar()
        {
            Console.Write("Enter the type of vehicle:\n\t1)Car;\n\t2)Truck;\n\t3)Bus;\n\t4)Motorcycle\n\t\t");
            try
            {
                int type = Convert.ToInt32(Console.ReadLine());
                switch (type)
                {
                    case 1:
                        Console.Write("Enter the number of vehicle: ");
                        string number = Console.ReadLine();
                        listCar.Add(new Car(number));
                        break;
                    case 2:
                        Console.Write("Enter the number of vehicle: ");
                        string number1 = Console.ReadLine();
                        listCar.Add(new Truck(number1));
                        break;
                    case 3:
                        Console.Write("Enter the number of vehicle: ");
                        string number2 = Console.ReadLine();
                        listCar.Add(new Motorcycle(number2));
                        break;
                    case 4:
                        Console.Write("Enter the number of vehicle: ");
                        string number0 = Console.ReadLine();
                        listCar.Add(new Bus(number0));
                        break;
                    default:
                        Console.WriteLine("Please enter valide value");
                        break;
                }
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void AddMoney()
        {

            Console.WriteLine("Select the transport:");
            for (int i = 0; i < listCar.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + listCar[i].GetCarNumber());
            }
            try
            {
                string ind = Console.ReadLine();
                for (int i = 0; i < listCar.Count; i++)
                {
                    if (listCar[i].CarNumber == ind)
                    {
                        Console.Write("Enter the amount you wish to replenish the account:");
                        double sum;
                        try
                        {
                            sum = Convert.ToDouble(Console.ReadLine());
                            if (sum <= 0)
                            {
                                Console.WriteLine("Amount can't be negative!!");
                            }
                            else
                            {
                                listCar[i].AddBalance(sum);
                                Console.WriteLine("Account succesfully updated\n");
                            }
                        }
                        catch(FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                }
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void ShowCar()
        {
            for (int i = 0; i < listCar.Count(); i++)
            {
                Console.WriteLine("Vehicle: " + listCar[i].GetCarNumber());
            }
        }

        public void RemoveCar()
        {
            Console.Write("Enter the vehicle number you want to delete:  ");
            try
            {
                string number = Console.ReadLine();
                int index = listCar.FindIndex(vehicle => vehicle.CarNumber.Equals(number, StringComparison.Ordinal));
                if (listCar[index].Balance > 0)
                {
                    listCar.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("The car '" + listCar[index].CarNumber + "' must pay a fine: " + Math.Abs(listCar[index].Balance) + " $");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            
        }

        public void timeCount()
        {
            Timer = new System.Threading.Timer(withdrawMmoney, null, 0, _Periodicity * 1000);
            Timer = new System.Threading.Timer(report, null, 0, _TimeReport * 1000);
        }

        private void withdrawMmoney(object state)
        {
            for (int i = 0; i < listCar.Count; i++)
            {
                if (listCar[i].Balance - listCar[i].GetTarify() > 0)
                {
                    listCar[i].Balance -= listCar[i].GetTarify();
                    _Balance += listCar[i].GetTarify();
                    Transaction transaction = new Transaction(DateTime.Now, listCar[i].CarNumber, listCar[i].GetTarify());
                    transactions.Add(transaction);
                }
                else
                {
                    listCar[i].Balance -= listCar[i].GetTarify() * _CoefficientOfFine;
                    _Balance += listCar[i].GetTarify() * _CoefficientOfFine;
                    Transaction transaction = new Transaction(DateTime.Now, listCar[i].CarNumber, listCar[i].GetTarify() * _CoefficientOfFine);
                    transactions.Add(transaction);
                }
            }

        }

        public void ShowLastTransactions()
        {
            //FileStream file = new FileStream("Transactions.log", FileMode.Open);
            foreach (var tr in transactions)
            {
                if (tr.TimeOfTransaction > (DateTime.Now - TimeSpan.FromMinutes(1)))
                    Console.WriteLine(" {0} from vehicle № {1} {2} written off {3} $", tr.TransactionID, tr.CarID, tr.TimeOfTransaction, tr.TransactionAmount);
            }
        }
        
        public void EarnedMoneyForLastMinute()
        {
            double sum = 0;
            foreach (var tr in transactions)
            {
                if (tr.TimeOfTransaction > (DateTime.Now - TimeSpan.FromMinutes(1)))
                {
                    sum += tr.TransactionAmount;
                    //Console.WriteLine(tr.TransactionAmount);
                }
            }
            Console.WriteLine("Earned money for last minute: {0}", sum);
        }

        public void ShowAllTransactions()
        {
            FileStream file = File.OpenRead("Transactions.log");
            byte[] array = new byte[file.Length];
            file.Read(array, 0, array.Length);
            string textFromFile = System.Text.Encoding.Default.GetString(array);
            Console.WriteLine("Last transactions:\n {0}", textFromFile);
            file.Close();
        }

        private void report(object state)
        {
            //Console.Clear();
            for (int i = 0; i < reportTransfer.Count; i++)
            {
                //Console.WriteLine(reportTtransfer[i]);
            }
        }

        public void WriteData(object obj)
        {
            FileStream file = new FileStream("Transactions.log", FileMode.OpenOrCreate);
            for (int i = 0; i < reportTransfer.Count; i++)
            {
                byte[] array = Encoding.Default.GetBytes(reportTransfer[i]);
                file.Write(array, 0, array.Length);
            }
            file.Close();
        }


    }
}
