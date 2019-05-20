using System;
using System.Threading;

namespace Parking
{
    class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime TimeOfTransaction { get; set; }
        public string CarID { get; set; }
        public double TransactionAmount { get; set; }

        public static int globalTransactionID;

        public Transaction(DateTime time, string id, double amount)

        {
            this.TransactionID = Interlocked.Increment(ref globalTransactionID);
            this.TransactionAmount = amount;
            this.CarID = id;
            this.TimeOfTransaction = time;
        }
    }
}
