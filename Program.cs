using System;

namespace BankingApplication {
    class Program {
        private static SavingsAccount savAccount;
        private static Chequing cheAccount;
        private static GlobalSavingsAccount gloAccount;
        static void Main(string[] args) {
            savAccount = new SavingsAccount(5, .10);
            cheAccount = new Chequing(5, .10);
            gloAccount = new GlobalSavingsAccount(5, .10);

            bool sheet;
            string ans = "";
            string temp = "";

            try {
                do {
                    Console.WriteLine("Welcome to the Online Bank Service! \nSelect an option from the menu: \nA: Savings \nB: Checking \nC: GlobalSavings \nQ: Exit");
                    ans = Console.ReadLine().ToUpper();
                    // Bank Menu
                    switch (ans) {
                        // Savings Menu
                        case "A":
                            do {
                                Console.WriteLine("\nSavings Menu: \nA: Deposit \nB: Withdrawal \nC: Close + Report \nR: Return to Bank Menu");
                                ans = Console.ReadLine().ToUpper();
                                switch (ans) {
                                    case "A":
                                        Console.WriteLine("\nHow much would you like to deposit?");
                                        temp = Console.ReadLine();
                                        savAccount.MakeDeposit(CheckMoney(temp, "reg"));
                                        sheet = false;
                                        break;
                                    case "B":
                                        Console.WriteLine("\nHow much would you like to withdraw?");
                                        temp = Console.ReadLine();
                                        savAccount.MakeWithdraw(CheckMoney(temp, "reg"));
                                        sheet = false;
                                        break;
                                    case "C":
                                        Console.WriteLine(savAccount.CloseAndReport());
                                        Console.WriteLine($"Percentage Change: {savAccount.GetPercentageChange()}%");
                                        sheet = true;
                                        break;
                                    case "R":
                                        sheet = true;
                                        break;
                                    default:
                                        Console.WriteLine("\nInvalid option, please choose valid option \n");
                                        sheet = false;
                                        break;
                                }
                            } while (!sheet);
                            sheet = false;
                            break;
                        // Chequing Menu
                        case "B":
                            do {
                                Console.WriteLine("\nChequing Menu: \nA: Deposit \nB: Withdrawal \nC: Close + Report \nR: Return to Bank Menu");
                                ans = Console.ReadLine().ToUpper();
                                switch (ans) {
                                    case "A":
                                        Console.WriteLine("\nHow much would you like to deposit?");
                                        temp = Console.ReadLine();
                                        cheAccount.MakeDeposit(CheckMoney(temp, "reg"));
                                        sheet = false;
                                        break;
                                    case "B":
                                        Console.WriteLine("\nHow much would you like to withdraw?");
                                        temp = Console.ReadLine();
                                        cheAccount.MakeWithdraw(CheckMoney(temp, "nreg"));
                                        sheet = false;
                                        break;
                                    case "C":
                                        Console.WriteLine(cheAccount.CloseAndReport());
                                        Console.WriteLine($"Percentage Change: {cheAccount.GetPercentageChange()}%");
                                        sheet = true;
                                        break;
                                    case "R":
                                        sheet = true;
                                        break;
                                    default:
                                        Console.WriteLine("\nInvalid option, please choose valid option \n");
                                        sheet = false;
                                        break;
                                }
                            } while (!sheet);
                            sheet = false;
                            break;
                        // Global Savings Menu
                        case "C":
                            do {
                                Console.WriteLine("\nGlobal Savings Menu: \nA: Deposit \nB: Withdrawal \nC: Close + Report \nD: Report Balance in USD \nR: Return to Bank Menu");
                                ans = Console.ReadLine().ToUpper();
                                switch (ans) {
                                    case "A":
                                        Console.WriteLine("\nHow much would you like to deposit?");
                                        temp = Console.ReadLine();
                                        gloAccount.MakeDeposit(CheckMoney(temp, "reg"));
                                        sheet = false;
                                        break;
                                    case "B":
                                        Console.WriteLine("\nHow much would you like to withdraw?");
                                        temp = Console.ReadLine();
                                        gloAccount.MakeWithdraw(CheckMoney(temp, "reg"));
                                        sheet = false;
                                        break;
                                    case "C":
                                        Console.WriteLine(gloAccount.CloseAndReport());
                                        Console.WriteLine($"Percentage Change: {gloAccount.GetPercentageChange()}%");
                                        sheet = true;
                                        break;
                                    case "D":
                                        Console.WriteLine(gloAccount.USValue(0.76).ToNAMoneyFormat(true));
                                        sheet = false;
                                        break;
                                    case "R":
                                        sheet = true;
                                        break;
                                    default:
                                        Console.WriteLine("\nInvalid option, please choose valid option");
                                        ans = Console.ReadLine().ToUpper();
                                        sheet = false;
                                        break;
                                }
                            } while (!sheet);
                            sheet = false;
                            break;
                        // Back to Bank Menu
                        case "Q":
                            sheet = true;
                            break;
                        default:
                            Console.WriteLine("\nInvalid option, please choose valid option \n");
                            sheet = false;
                            break;
                    }
                } while (!sheet);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        private static double CheckMoney(string temp, string type) {
            bool sheet;

            do {
                var isNumeric = double.TryParse(temp, out double money);
                if (isNumeric) {
                    if (type == "reg")
                        return money;
                    else {
                        if (money <= cheAccount.CurrentBalance)
                            return money;
                        else {
                            Console.WriteLine("Not enough funds, please enter valid amount");
                            temp = Console.ReadLine();
                            sheet = false;
                        }
                    }
                }
                else {
                    Console.WriteLine("Enter valid numeric amount");
                    temp = Console.ReadLine();
                    sheet = false;
                }
            } while (!sheet);

            return 0;
        }

    }
}
