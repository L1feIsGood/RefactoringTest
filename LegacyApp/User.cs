using System;

namespace LegacyApp
{
    public class User
    {
        public User(Client client, DateTime dateOfBirth, string emailAddress, string firstName, string surname, int creditLimit = 0, bool hasCreditLimit = false)
        {
            Client = client;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            FirstName = firstName;
            Surname = surname;
            CreditLimit = creditLimit;
            HasCreditLimit = hasCreditLimit;
        }
        public Client Client { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CreditLimit { get; set; }
        public bool HasCreditLimit { get; set; }
    }
}
