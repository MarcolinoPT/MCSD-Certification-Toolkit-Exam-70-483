namespace ConsoleApplication16.Chapter_6_Working_with_Delegates_Events_and_Exceptions
{
    using System;
    public class Working_with_Events
    {
        class OverdrawnEventArgs : EventArgs
        {
            public decimal CurrentBalance, DebitAmount;
            public OverdrawnEventArgs(decimal currentBalance, decimal debitAmount)
            {
                CurrentBalance = currentBalance;
                DebitAmount = debitAmount;
            }
        }

        class BankAccount
        {
            public event EventHandler<OverdrawnEventArgs> Overdrawn;
            // The account balance.
            public decimal Balance { get; set; }
            // Add money to the account.
            public void Credit(decimal amount)
            {
                Balance += amount;
            }

            // Raise the Overdrawn event.
            protected virtual void OnOverdrawn(OverdrawnEventArgs args)
            {
                if (Overdrawn != null) Overdrawn(this, args);
            }

            // Remove money from the account.
            public void Debit(decimal amount)
            {
                // See if there is this much money in the account.
                if (Balance >= amount)
                {
                    // Remove the money.
                    Balance -= amount;
                }
                else
                {
                    // Raise the Overdrawn event.
                    OnOverdrawn(new OverdrawnEventArgs(Balance, amount));
                }
            }
        }

        class MoneyMarketAccount : BankAccount
        {
            public void DebitFee(decimal amount)
            {
                // See if there is this much money in the account.
                if (Balance >= amount)
                {
                    // Remove the money.
                    Balance -= amount;
                }
                else
                {
                    // Raise the Overdrawn event.
                    OnOverdrawn(new OverdrawnEventArgs(Balance, amount));
                }
            }
        }

        public static void RunBankAccountExample()
        {
            var bankAccount = new BankAccount();
            bankAccount.Overdrawn += (sender, args) => { Console.WriteLine("Overdrawn event raised! Amount: {0} Balance: {1}", args.DebitAmount, args.CurrentBalance); };
            bankAccount.Balance = 100;
            bankAccount.Debit(105);
        }


    }
}