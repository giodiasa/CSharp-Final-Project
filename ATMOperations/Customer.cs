namespace ATMOperations
{
    internal class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PersonalNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public double Balance { get; set; }
        public Customer(int id, string firstName, string lastName, string personalNumber, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PersonalNumber = personalNumber;
            Password = password;
            Balance = 0;
        }
    }
}
