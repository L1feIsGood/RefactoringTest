using System;

namespace LegacyApp.DTO
{
    public class AddUserDTO
    {
        public Client Client { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CreditLimit { get; set; }
        public bool HasCreditLimit { get; set; }
    }
}