using System;

namespace LegacyApp
{
    public interface IUser
    {
        Client Client { get; set; }
        int CreditLimit { get; set; }
        DateTime DateOfBirth { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        bool HasCreditLimit { get; set; }
        string Surname { get; set; }
    }
}