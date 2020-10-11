using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{
    [Serializable]
    class AccountDisabledException :Exception
    {

        public Account Account { get; }

        public AccountDisabledException(){ }
        public AccountDisabledException(string message):base(message)
        { }
        public AccountDisabledException(string message, Exception inner) : base(message, inner)
        { }

        public AccountDisabledException(string message, Account acc):this(message)
        {
            Account = acc;
        }
    }
}
