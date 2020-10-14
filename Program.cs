using System;
using System.Collections.Generic;

namespace BankingApplication {
    class Program {

        static SavingsAccount savAccount;
        static Chequing cheAccount;
        static GlobalSavingsAccount gloAccount;
        static void Main(string[] args) {
            savAccount = new SavingsAccount(5, .10);
            cheAccount = new Chequing(5, .10);
            gloAccount = new GlobalSavingsAccount(5, .10);

            bool sheet;
            string ans = "";
            string temp = "";

            MenuTreeNode tree = 
                new MenuTreeNode("root") {
                    new MenuTreeNode("bank menu", (self) =>
                    {
                        Dictionary<string, string> optionToMenu = new Dictionary<string, string>() { { "a", "savings menu" }, { "b", "checking menu" }, { "c", "global savings menu" }, { "q", "quit" } };

                        GetInputFromConsole(
                            "Welcome to the Online Bank Service! \nSelect an option from the menu: \nA: Savings \nB: Checking \nC: GlobalSavings \nQ: Exit", 
                            "\nInvalid option, please choose valid option \n", 
                            input => optionToMenu.ContainsKey(input.ToLower()), 
                            out string choice
                        );
                        self.GetChild(optionToMenu[choice]);
                    })
                    {
                        new MenuTreeNode("savings menu", (self) => 
                        {
                             Dictionary<string, Action> optionToAction = new Dictionary<string, Action>() 
                             {
                                 { "a", () =>   { DepositScenario(savAccount); self.Execute(); } },
                                 { "b", () =>   { WithdrawalScenario(savAccount); self.Execute(); } },
                                 { "c", () =>   { CloseAndReportScenario(savAccount); self.Parent.Execute(); } },
                                 { "r",           self.Parent.Execute }
                             };
                            GetInputFromConsole(
                                "\nSavings Menu: \nA: Deposit \nB: Withdrawal \nC: Close + Report \nR: Return to Bank Menu",
                                "\nInvalid option, please choose valid option \n",
                                input => optionToAction.ContainsKey(input.ToLower()),
                                out string choice
                            );

                            optionToAction[choice]();
                        }),
                        new MenuTreeNode("checking menu", (self) =>
                        {
                            Dictionary<string, Action> optionToAction = new Dictionary<string, Action>()
                             {
                                 { "a", () =>   { DepositScenario(cheAccount); self.Execute(); } },
                                 { "b", () =>   { WithdrawalScenario(cheAccount); self.Execute(); } },
                                 { "c", () =>   { CloseAndReportScenario(cheAccount); self.Parent.Execute(); } },
                                 { "r",           self.Parent.Execute }
                             };
                            GetInputFromConsole(
                                "\nSavings Menu: \nA: Deposit \nB: Withdrawal \nC: Close + Report \nR: Return to Bank Menu",
                                "\nInvalid option, please choose valid option \n",
                                input => optionToAction.ContainsKey(input.ToLower()),
                                out string choice
                            );

                            optionToAction[choice]();
                        }),
                        new MenuTreeNode("global savings menu", (self) => 
                        {
                            Dictionary<string, Action> optionToAction = new Dictionary<string, Action>()
                             {
                                 { "a", () =>   { DepositScenario(gloAccount); self.Execute(); } },
                                 { "b", () =>   { WithdrawalScenario(gloAccount); self.Execute(); } },
                                 { "c", () =>   { CloseAndReportScenario(gloAccount); self.Parent.Execute(); } },
                                 { "d", () =>   { Console.WriteLine(gloAccount.USValue(0.76).ToNAMoneyFormat(true)); self.Execute(); } },
                                 { "r",           self.Parent.Execute }
                             };
                            GetInputFromConsole(
                                "\nSavings Menu: \nA: Deposit \nB: Withdrawal\nC: Close and Report \nD: Report Balance in USD\nR: Return to Bank Menu",
                                "\nInvalid option, please choose valid option \n",
                                input => optionToAction.ContainsKey(input.ToLower()),
                                out string choice
                            );

                            optionToAction[choice]();
                        }),
                        new MenuTreeNode("quit", _ => Console.WriteLine("Goodbye"))
                    }
                };


            tree.GetChild("bank menu");
            System.Threading.Thread.Sleep(1500);

        }

        private static void DepositScenario(Account acc)
        {
            GetInputFromConsole("\nHow much would you like to deposit?", "", _ => true, out string amount);
            acc.MakeDeposit(CheckMoney(amount, "reg"));
        }

        private static void WithdrawalScenario(Account acc)
        {
            GetInputFromConsole("\nHow much would you like to withdraw?", "", _ => true, out string amount);
            acc.MakeWithdraw(CheckMoney(amount, acc is Chequing ? "nreg" : "reg"));
        }

        private static void CloseAndReportScenario(Account acc)
        {
            Console.WriteLine(acc.CloseAndReport());
             Console.WriteLine($"Percentage Change: {acc.GetPercentageChange()}%");
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

        private static void GetInputFromConsole<T>(string question, string error, Predicate<string> predicate, out T output)
        {

            string temporaryInput;
            bool first = true;
            Console.WriteLine(question);
            do
            {
                if (!first)
                    Console.WriteLine(error);

                temporaryInput = Console.ReadLine();
                first = false;
            } while (!predicate.Invoke(temporaryInput));

            output = (T)Convert.ChangeType(temporaryInput, typeof(T));
        }

    }
}
