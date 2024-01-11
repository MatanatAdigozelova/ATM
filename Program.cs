using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{

    internal class Program
    {
        static void Main(string[] args)
        {
            double b= Convert.ToDouble(Console.ReadLine());
            ATM atm = new ATM(b);
            //1)Bu event bas veren zaman bir ATM-ə məlumat gedir
            atm.BalanceFinished += (n, m) => { Console.WriteLine($"ATM-de {n}AZN mebleginde pul yoxdur.Balans {m}AZN"); };
            //2)Musteriye məlumat verir
            atm.BalanceFinished += (n, m) => { Console.WriteLine($"Hormetli Mushteri balansda teleb olunan meblegde {n}AZN pul yoxdur"); };

            atm.TransferSuccessful += (n, m, time) => { Console.WriteLine($"{time} tarixinde {n}AZN meblegde emeliyyat ugurla basha chatdi. Balans {m}AZN "); };

            atm.WithDraw(100);
            atm.WithDraw(700);
        }
    }
    class ATM
    {

        public event Action<double, double> BalanceFinished;
        public event Action<double, double, DateTime> TransferSuccessful;
        private double _balance;

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public ATM(double balance)
        {
            Balance = balance;
        }
        public void WithDraw(double amount)
        {
            if (Balance > amount)
            {
                Balance -= amount;
                
                if (TransferSuccessful != null)
                {
                    TransferSuccessful.Invoke(amount, Balance, DateTime.Now);
                    return;
                }
            }
            else
            {
                if (BalanceFinished != null)
                {
                    BalanceFinished.Invoke(amount, Balance);
                }
            }

        }

    }
}
