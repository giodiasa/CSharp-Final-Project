using System.Text.Json;

namespace ATMOperations
{
    internal class Logger : ILogger
    {
        string path = @"../../../transactionHistory.json";
        private List<Result> loggers;

        public Logger()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                loggers = JsonSerializer.Deserialize<List<Result>>(json) ?? new List<Result>();
            }
            else
            {
                loggers = new List<Result>();
            }
        }
        public void LogBalanceCheck(string firstName, string lastName, DateTime date, string personalNumber)
        {
            var transaction = new Result
            {
                PersonalNumber = personalNumber,
                Event = $"{firstName} {lastName} checked balance on {date}"
            };
            loggers.Add(transaction);
            string json = JsonSerializer.Serialize(loggers, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }

        public void LogDeposit(string firstName, string lastName, double amount, DateTime date, double balance, string personalNumber)
        {
            var transaction = new Result
            {
                PersonalNumber = personalNumber,
                Event = $"{firstName} {lastName} deposited {amount} GEL on {date}, remaining balance is {balance}"
            };
            loggers.Add(transaction);
            string json = JsonSerializer.Serialize(loggers, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }

        public void LogWithdrawal(string firstName, string lastName, double amount, DateTime date, double balance, string personalNumber)
        {
            var transaction = new Result
            {
                PersonalNumber = personalNumber,
                Event = $"{firstName} {lastName} withdrawn {amount} GEL on {date}, remaining balance is {balance}"
            };
            loggers.Add(transaction);
            string json = JsonSerializer.Serialize(loggers, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }
        public List<Result> GetTransactionHistory()
        {
            return loggers;
        }
    }
}
