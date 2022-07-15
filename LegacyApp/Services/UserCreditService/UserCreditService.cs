using System;

namespace LegacyApp
{
    public class UserCreditServiceClient : IUserCreditService, IDisposable
    {
        public int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth)
        {
            return 0;
        }

        public void Dispose()
        {
            
        }
    }
}