using System;

namespace LegacyApp
{
    public interface IUserCreditService
    {
        int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth);
    }
}