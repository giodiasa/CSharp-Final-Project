using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ATMOperations
{
    internal class Operations
    {
        List<Customer>? customers;
        string customersFilePath = @"../../../customers.json";
        private readonly ILogger _logger;
        public Operations(ILogger logger)
        {
            _logger = logger;
            if (File.Exists(customersFilePath))
            {
                string json = File.ReadAllText(customersFilePath);
                customers = JsonSerializer.Deserialize<List<Customer>>(json);
            }
            else
            {
                customers = new List<Customer>();
            }
            _logger = logger;

        }
        public void ATMMenu()
        {
            while (true)
            {
                Console.WriteLine("1 - Login, 2 - Register, 3 - Exit");
                string choise = Console.ReadLine()!;
                while (choise != "1" && choise != "2" && choise != "3")
                {
                    Console.WriteLine("Invalid input, try again..");
                    choise = Console.ReadLine()!;
                }
                switch (choise)
                {
                    case "1":
                        Console.WriteLine("Enter your password");
                        string pass = Console.ReadLine()!;
                        if (!customers!.Any(x => x.Password == pass))
                        {
                            Console.WriteLine("User not found");
                        }
                        else
                        {
                            var customer = customers!.FirstOrDefault(x => x.Password == pass);
                            Console.WriteLine($"Welcome, {customer!.FirstName}");
                            Login(customer!);
                        }
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        return;
                }
            }            
        }
        private void Login(Customer customer)
        {
            while (true)
            {
                Console.WriteLine("1 - Check Balance, 2 - Deposit Money, 3 - Withdraw Money, 4 - See Transaction History, 5 - Exit");
                string choise = Console.ReadLine()!;
                while (choise != "1" && choise != "2" && choise != "3" && choise != "4" && choise != "5")
                {
                    Console.WriteLine("Invalid input, try again..");
                    choise = Console.ReadLine()!;
                }
                switch (choise)
                {
                    case "1":
                        Console.WriteLine($"{customer.Balance} GEL");
                        _logger.LogBalanceCheck(customer.FirstName, customer.LastName, DateTime.Now, customer.PersonalNumber);
                        break;
                    case "2":
                        Deposit(customer);                        
                        break;
                    case "3":
                        Withdraw(customer);
                        break;
                    case "4":
                        var history = _logger.GetTransactionHistory().Where(x => x.PersonalNumber == customer.PersonalNumber);
                        if (!history.Any())
                        {
                            Console.WriteLine("Transaction history is empty.");
                        }
                        foreach (var item in history)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case "5":
                        return;
                }
            }
        }
        private void Register()
        {
            Console.WriteLine("Enter your first name");
            string firstName = Console.ReadLine()!;
            Console.WriteLine("Enter your last name");
            string lastName = Console.ReadLine()!;
            Console.WriteLine("Enter your Personal Number");
            string personalNumber = Console.ReadLine()!;
            while(!personalNumber.All(char.IsDigit) || personalNumber.Length != 11 || customers!.Any(c => c.PersonalNumber == personalNumber)) 
            {
                if (customers!.Any(c => c.PersonalNumber == personalNumber))
                {
                    Console.WriteLine("Customer with this personal number is already registered");
                    return;
                }
                Console.WriteLine("The correct format for the personal number is 11 digits. Please verify and try again");
                personalNumber = Console.ReadLine()!;
            }
            string password = GenerateUniquePassword(customers!);
            int id = GetNextCustomerId(customers!);
            var newCustomer = new Customer(id, firstName, lastName, personalNumber, password);            
            customers!.Add(newCustomer);
            string updatedJson = JsonSerializer.Serialize(customers, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(customersFilePath, updatedJson);
            Console.WriteLine($"You have successfully registered, Your password is {password}");
        }
        private void Deposit(Customer customer)
        {
            Console.WriteLine("Enter amount to deposit");
            double amount = 0;
            bool input = false;
            while(!input)
            {
                input = double.TryParse(Console.ReadLine(), out amount);
                if (!input) { Console.WriteLine("Enter correct amount"); }
            }            
            customer.Balance += amount;
            Console.WriteLine($"{amount} GEL added to {customer.FirstName} {customer.LastName}'s account");
            _logger.LogDeposit(customer.FirstName, customer.LastName, amount, DateTime.Now, customer.Balance, customer.PersonalNumber);
            string updatedJson = JsonSerializer.Serialize(customers, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(customersFilePath, updatedJson);
        }
        private void Withdraw(Customer customer)
        {
            Console.WriteLine("Enter amount to withdraw");
            double amount = 0;
            bool input = false;
            while (!input)
            {
                input = double.TryParse(Console.ReadLine(), out amount);
                if (!input) { Console.WriteLine("Enter correct amount"); }
            }
            if (amount > customer.Balance)
            {
                Console.WriteLine("Insufficient Funds");
            }
            else
            {
                customer.Balance -= amount;
                Console.WriteLine($"{amount} GEL withdrawn from {customer.FirstName} {customer.LastName}'s account");
                _logger.LogDeposit(customer.FirstName, customer.LastName, amount, DateTime.Now, customer.Balance, customer.PersonalNumber);
                string updatedJson = JsonSerializer.Serialize(customers, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(customersFilePath, updatedJson);
            }
        }
        static string GenerateUniquePassword(List<Customer> existingCustomers)
        {
            Random random = new Random();
            int password;
            do
            {
                password = random.Next(1000, 10000);
            } while (existingCustomers.Any(c => c.Password == password.ToString()));

            return password.ToString();
        }
        static int GetNextCustomerId(List<Customer> existingCustomers)
        {
            if(existingCustomers.Count == 0) return 1;
            int maxId = existingCustomers.Max(c => c.Id);
            return maxId + 1;
        }
    }
}
