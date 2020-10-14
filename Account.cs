using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{

    public enum Status
    {
        Active,
        Inactive
    }
    public abstract class Account : IAccount
    {

        // MAKE PRIVATE WITH FORWARDING PROPERTY
        protected double
            _startingBalance,
            _currBalance,
            _amountWithdrawn,
            _annualInterestRate,
            _serviceCharge;

        protected int
            _deposits,
            _withdrawals;

        protected Status _status;

        public double StartingBalance => _startingBalance;
        public double CurrentBalance => _currBalance;

        public Account(double balance, double annualInterestRate)
        {
            _startingBalance = balance;
            _currBalance = _startingBalance;
            _annualInterestRate = annualInterestRate;
            _status = balance < 25 ? Status.Inactive : Status.Active;
        }


        public void CalculateInterest()
        {
            _currBalance += (_annualInterestRate / 12) * _currBalance;
        }

        public virtual string CloseAndReport()
        {
            string monthlyReport = $"\n======= \nMONTHLY REPORT \nStarting Balance: {_startingBalance.ToNAMoneyFormat(true)} \nTotal Deposits: {_deposits} \nTotal Withdrawals: {_withdrawals} \nService Charges {_serviceCharge.ToNAMoneyFormat(true)} \nCurrent Balance: {_currBalance.ToNAMoneyFormat(true)} \nAccount Status {_status} \n=======\n";

            double previousBalance = _currBalance;
            _currBalance -= _serviceCharge;
            CalculateInterest();

            _deposits = 0;
            _withdrawals = 0;
            _amountWithdrawn = 0;

            return monthlyReport;
        }

        public virtual void MakeDeposit(double amount)
        {
            _currBalance += amount;
            _deposits++;
        }

        public virtual void MakeWithdraw(double amount)
        {
            _currBalance -= amount;
            _amountWithdrawn += amount;
            _withdrawals++;
        }
    }
}
