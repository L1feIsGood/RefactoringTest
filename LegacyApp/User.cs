using System;

namespace LegacyApp
{
    public class User
    {
        public Client Client { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CreditLimit { get; set; }
        public bool HasCreditLimit
        {
            get
            {
                if (Client.Name == "VeryImportantClient") return false;
                return true;
            }
        }
        public User(Client client, DateTime birthDate, string emailAddress, string firstName, string lastName)
        {
            Client = client;
            BirthDate = birthDate;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}