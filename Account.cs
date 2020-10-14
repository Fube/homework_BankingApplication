using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        private double
            _startingBalance,
            _currBalance,
            _amountWithdrawn,
            _annualInterestRate,
            _serviceCharge;

        private int
            _deposits,
            _withdrawals;

        private Status _status;

        public double StartingBalance { get { return _startingBalance; } protected set { _startingBalance = value; } }
        public double CurrentBalance { get { return _currBalance; } protected set { _currBalance = value; } }

        protected double AmountWithdrawn { get { return _amountWithdrawn; } set { _amountWithdrawn = value; } }
        protected double AnnualInterestCharge { get { return _annualInterestRate; } set { _annualInterestRate = value; } }
        protected double ServiceCharge { get { return _serviceCharge; } set { _serviceCharge = value; } }
        
        protected int Depostis { get { return _deposits; } set { _deposits = value; } }
        protected int Withdrawals { get { return _withdrawals; } set { _withdrawals = value; } }
        
        protected Status Status { get { return _status; } set { _status = value; } }

        public Account(double balance, double annualInterestRate)
        {
            _startingBalance = balance;
            _currBalance = _startingBalance;
            _annualInterestRate = annualInterestRate;
            _status = balance < 25 ? Status.Inactive : Status.Active;
        }


        public void CalculateInterest()
        {
            _currBalance += (_annualInterestRate / 12.0) * _currBalance;
        }

        public virtual string CloseAndReport()
        {
            string monthlyReport = $"\n======= \nMONTHLY REPORT \nStarting Balance: {_startingBalance.ToNAMoneyFormat(true)} \nTotal Deposits: {_deposits} \nTotal Withdrawals: {_withdrawals} \nService Charges {_serviceCharge.ToNAMoneyFormat(true)} \nCurrent Balance: {_currBalance.ToNAMoneyFormat(true)} \nAccount Status {_status} \n=======\n";
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
