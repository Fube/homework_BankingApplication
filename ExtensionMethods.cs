using System;

namespace BankingApplication {
    public static class ExtensionMethods {
        public static double GetPercentageChange(this Account instance) => (instance.CurrentBalance - instance.StartingBalance) / instance.StartingBalance * 100;

        public static string ToNAMoneyFormat(this Double instance, bool roundDown) {
            string naFormat;
            bool sheet = false;
            
            if (instance < 0) {
                instance = Math.Abs(instance);
                sheet = true;
            }

            if (roundDown) {
                naFormat = Math.Round(instance, 2).ToString("C2");
            }
            else {
                naFormat = (Math.Floor((instance * 0.1) * 100) / 100).ToString("C2");
            }

            return !sheet ? naFormat : $"({naFormat})";
        }
    }
}
