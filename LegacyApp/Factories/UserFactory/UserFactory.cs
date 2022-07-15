using LegacyApp.Models;
using System;

namespace LegacyApp.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IClientRepository _clientRepository;

        private readonly ICreditLimitProviderFactory _creditLimitProviderFactory;

        public UserFactory(IClientRepository clientRepository, ICreditLimitProviderFactory creditLimitProviderFactory)
        {
            _clientRepository = clientRepository;

            _creditLimitProviderFactory = creditLimitProviderFactory;
        }

        /// <summary>
        /// Создает пользователя
        /// </summary>
        /// <param name="firstname">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="email">Эл. почта</param>
        /// <param name="dateOfBirth">Дата рождения</param>
        /// <param name="clientId">ID клиента</param>
        /// <returns></returns>
        public User CreateUser(string firstname,
                               string surname,
                               string email,
                               DateTime dateOfBirth,
                               int clientId)
        {
            UserParameters userParameters = new UserParameters
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstname,
                Surname = surname
            };


            if (!userParameters.IsValid())
            {
                return null;
            }

            userParameters.Client = _clientRepository.GetById(clientId);

            var creditLimitProvider = _creditLimitProviderFactory.GetCreditLimitProvider(userParameters.Client.Name);

            userParameters.HasCreditLimit = creditLimitProvider.HasCreditLimit();

            var creditLimit = creditLimitProvider.GetCreditLimit(userParameters);

            userParameters.CreditLimit = creditLimit ?? 0;


            if (userParameters.HasCreditLimit &&
                userParameters.CreditLimit < GlobalConfig.minGreditLimit)
            {
                return null;
            }

            User user = new User
            {
                Client = userParameters.Client,
                DateOfBirth = userParameters.DateOfBirth,
                EmailAddress = userParameters.EmailAddress,
                FirstName = userParameters.FirstName,
                Surname = userParameters.Surname,
                CreditLimit = userParameters.CreditLimit,
                HasCreditLimit = userParameters.HasCreditLimit
            };

            return user;
        }
    }
}