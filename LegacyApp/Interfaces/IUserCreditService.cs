using System;

namespace LegacyApp
{
    interface IUserCreditService : IDisposable
    {
        int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth);
    }
}