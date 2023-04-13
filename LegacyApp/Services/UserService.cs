using System;
using LegacyApp.Repositories;
using LegacyApp.Services;
using LegacyApp.Services.CreditLimit;

namespace LegacyApp
{
    public class UserService
    {
        private readonly UserFactory _userFactory;
        private readonly CreditLimiterFactory _creditLimiterFactory;

        public UserService()
        {
            var clientRepository = new ClientRepository();
            _userFactory = new UserFactory(clientRepository);
            _creditLimiterFactory = new CreditLimiterFactory();
        }

        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var userCreationParams = new UserCreationParams
            {
                FirstName = firstName,
                Surname = surname,
                Email = email,
                DateOfBirth = dateOfBirth,
                ClientId = clientId
            };

            var user = _userFactory.CreateUserOrDefault(userCreationParams);

            if (user == null)
                return false;

            var creditLimiter = _creditLimiterFactory.CreateCreditLimiter(user.Client.Name);

            const int minCreditLimit = 500;
            var hasUserCreditLimit = creditLimiter.HasCreditLimit;
            var creditLimit = creditLimiter.GetCreditLimit(user);

            if (hasUserCreditLimit && creditLimit < minCreditLimit)
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}