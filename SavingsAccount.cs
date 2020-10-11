using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{
    class SavingsAccount : Account
    {
        public SavingsAccount(double balance, double annualInterestRate) : base(balance, annualInterestRate) { }

        public void MakeWithdrawl()
        {
            if(_status == Status.False)
            {
                throw new AccountDisabledException("Cannot make withdrawl from disabled account", this);
            }
            else
            {
                MakeWithdrawl();
            }
        }
    }
}
