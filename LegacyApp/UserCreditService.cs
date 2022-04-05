using System;

namespace LegacyApp
{
    public interface IUserCreditService : IDisposable
    {
        int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth);
    }

    public class UserCreditServiceClientRegular : IUserCreditService
    {
        public UserCreditServiceClientRegular()
        {

        }

        public virtual int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth)
        {
            return 0;
        }

        public void Dispose()
        {

        }
    }

    public class UserCreditServiceClientImportant : UserCreditServiceClientRegular
    {
        public override int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth)
        {
            return base.GetCreditLimit(firstName, surname, dateOfBirth) * 2;
        }
    }
}