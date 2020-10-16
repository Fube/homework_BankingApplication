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

        public override void MakeWithdraw(double amount)
        {
            if(Status == Status.Inactive)
            {
                throw new AccountDisabledException("Cannot make withdrawal from disabled account", this);
            }
            else
            {
                base.MakeWithdraw(amount);

                if (CurrentBalance < 25)
                    Status = Status.Inactive;
            }
        }

        public override void MakeDeposit(double amount)
        {

            if(Status == Status.Inactive && CurrentBalance + amount > 25)
            {
                base.MakeDeposit(amount);
                Status = Status.Active;
            }
            else
            {
                base.MakeDeposit(amount);
            }
        }

        public override String CloseAndReport()
        {

            if(Withdrawals < 4)
            {
                ServiceCharge += Withdrawals - 4;
            }
            return base.CloseAndReport();
        }
    }
}
