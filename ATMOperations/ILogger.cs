namespace ATMOperations
{
    internal interface ILogger
    {
        void LogDeposit(string firstName, string lastName, double amount, DateTime date, double balance, string personalNumber);
        void LogWithdrawal(string firstName, string lastName, double amount, DateTime date, double balance, string personalNumber);
        void LogBalanceCheck(string firstName, string lastName, DateTime date, string personalNumber);
        List<Result> GetTransactionHistory();
    }
}
