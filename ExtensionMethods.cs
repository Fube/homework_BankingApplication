using System;

namespace BankingApplication {
    public static class ExtensionMethods {
        public static double GetPercentageChange(this Account exAcc) => (exAcc.CurrentBalance - exAcc.StartingBalance) / exAcc.StartingBalance * 100;

        public static string ToNAMoneyFormat(this Double doo, bool boo) {
            string naFormat;
            bool sheet = false;
            
            if (doo < 0) {
                doo = Math.Abs(doo);
                sheet = true;
            }

            if (boo) {
                naFormat = Math.Round(doo, 2).ToString("C2");
            }
            else {
                naFormat = (Math.Floor((doo * 0.1) * 100) / 100).ToString("C2");
            }

            return !sheet ? naFormat : $"({naFormat})";
        }
    }
}
