using System;

namespace LegacyApp
{
    using System.Collections.Generic;
    using System.Linq;
    using LegacyApp.ClientTypes;

    public interface IUserCreditService
    {
        int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth);
        int CalculateCreditLimit(User user);
        CreditModel GetByType(string clientType);
    }

    public class UserCreditServiceClient : IUserCreditService, IDisposable
    {
        private readonly List<IUserCreditProvider> _userCreditProviders;

        public UserCreditServiceClient()
        {
            _userCreditProviders = new List<IUserCreditProvider>
            {
                new DefaultClient(),
                new ImportantClient(),
                new VeryImportantClient()
            };
        }

        public int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth)
        {
            return 0;
        }

        public int CalculateCreditLimit(User user)
        {
            if (user.HasCreditLimit)
            {
                var creditLimit = GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                creditLimit = creditLimit * user.CreditLimit;
                return creditLimit;
            }

            return user.CreditLimit;
        }

        public CreditModel GetByType(string clientType)
        {
            var userCreditProvider = _userCreditProviders
                .FirstOrDefault(x => x.ClientType == clientType);
            if (userCreditProvider == null)
                return _userCreditProviders
                    .First(x => x.ClientType == null)
                    .GenerateClientModel();
            return userCreditProvider.GenerateClientModel();
        }

        public void Dispose()
        {
        }
    }
}
