using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    abstract class CarType
    {
        double balance = 0;
        string carNumber = string.Empty;
        double _Tarify;

        public string CarNumber { get => carNumber; set => carNumber = value; }
        public double Balance { get => balance; set => balance = value; }

        public string GetCarNumber() { return carNumber; }

        public void AddBalance(double sum)
        {
            Balance += sum;
        }

        public CarType(string number, double tarify)
        {
            CarNumber = number;
            _Tarify = tarify;
        }

        public double GetTarify()
        {
            return _Tarify;
        }
    }

    class Car : CarType
    {
        public Car (string str) : base(str, 2)
        {
            AddBalance(20);
        }
    }

    class Truck : CarType
    {
        public Truck(string str) : base(str, 5)
        {
            AddBalance(50);
        }
    }

    class Bus : CarType
    {
        public Bus(string str) : base(str, 3.5)
        {
            AddBalance(35);
        }
    }

    class Motorcycle : CarType
    {
        public Motorcycle(string str) : base(str, 1)
        {
            AddBalance(10);
        }
    }
}
