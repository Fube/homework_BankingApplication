using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{
    class GlobalSavingsAccount : SavingsAccount
    {

        public GlobalSavingsAccount(double balance, double annualInterestRate) : base(balance, annualInterestRate) { }

        public double USValue(double rate)
        {
            return _currBalance * rate;
        }
    }
}
