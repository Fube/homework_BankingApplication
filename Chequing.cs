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
    
        
        public new void MakeWithdraw(double amount)
        {
            /**
             *  The instructions are unclear. 
             *  I do not understand what I'm supposed to do here. 
             *  I will write what I interpreted I guess...
             **/

            if(_currBalance - amount < 0 && _currBalance < 15) // If account does not have sufficient funds for withdrawal AND does not have sufficient funds to pay the service fee (this is what I interpreted), we subtract 15 from balance, making it negative.
            {
                _serviceCharge += 15;
                _currBalance -= 15;
            }else if(_currBalance - amount < 0 && _currBalance > 15)// If account does not have sufficient funds for withdrawal AND does have sufficient funds to pay the service fee, we subtract 15 from balance, but it doesn't become negative.
            {
                _serviceCharge += 15;
                _currBalance -= 15;
            }
            else // If the user has enough for withdrawal
            {
                base.MakeWithdraw(amount);
            }
        }

        public new void MakeDeposit(double amount)
        {
            base.MakeDeposit(amount);
        }

        public new string CloseAndReport()
        {
            _serviceCharge += 5 + (0.1 * _withdrawals);
            return base.CloseAndReport();
        }
    }

}
