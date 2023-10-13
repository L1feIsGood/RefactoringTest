using System;

namespace LegacyApp
{
    public interface IUserValidator
    {
        bool IsValid(string firstName, string surname, string email, DateTime dateOfBirth);
    }
}
