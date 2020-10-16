using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{
    class Chequing : Account
    {

        public Chequing(double balance, double annualInterestRate) : base(balance, annualInterestRate) { }
    
        
        public override void MakeWithdraw(double amount)
        {

            if(CurrentBalance - amount < 0) // User does not have money, so they get charged
            {
                ServiceCharge += 15;
            }
            else // If the user has enough for withdrawal
            {
                base.MakeWithdraw(amount);
            }
        }

        public override void MakeDeposit(double amount)
        {
            base.MakeDeposit(amount);
        }

        public override string CloseAndReport()
        {
            ServiceCharge += 5 + (0.1 * Withdrawals);
            return base.CloseAndReport();
        }
    }

}
