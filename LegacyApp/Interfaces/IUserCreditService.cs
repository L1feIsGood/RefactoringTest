using System;

namespace LegacyApp
{
    public interface IUserCreditService: IDisposable
    {
        int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth);
    }
}
