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
            if(_status == Status.Inactive)
            {
                throw new AccountDisabledException("Cannot make withdrawal from disabled account", this);
            }
            else
            {
                base.MakeWithdraw(amount);

                if (this._currBalance < 25)
                    this._status = Status.Inactive;
            }
        }

        public override void MakeDeposit(double amount)
        {

            if(_status == Status.Inactive && _currBalance + amount > 25)
            {
                base.MakeDeposit(amount);
                _status = Status.Active;
            }
            else
            {
                base.MakeDeposit(amount);
            }
        }

        public new String CloseAndReport()
        {

            if(_withdrawals < 4)
            {
                _serviceCharge += _withdrawals - 4;
            }
            return base.CloseAndReport();
        }
    }
}
