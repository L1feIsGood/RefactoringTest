using System;
using LegacyApp.UserValidation;
using LegacyApp.Models;
using LegacyApp.Repository;
using LegacyApp.UserCredit;
using LegacyApp.Extensions;

namespace LegacyApp
{
    public class UserService : IDisposable
    {
        public UserValidator UserBaseValidator { get; set; }
        public UserValidator UserCreditValidator { get; set; }
        public IClientRepository ClientRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IUserCreditService UserCreditService { get; set; }

        public UserService()
        {
            UserBaseValidator = new UserValidator
            {
                new NamesUserValidationRule(),
                new EmailUserValidationRule(),
                new DateOfBirthUserValidationRule(minimumAge: 21),
                new ClientUserValidationRule()
            };

            UserCreditValidator = new UserValidator
            {
                new DefaultCreditUserValidationRule(minCreditLimit: 500)
            };

            ClientRepository = new ClientRepository();
            UserRepository = new UserRepository();
            UserCreditService = new UserCreditService();
        }

        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            CheckForNull(UserBaseValidator);
            CheckForNull(UserCreditValidator);
            CheckForNull(ClientRepository);
            CheckForNull(UserRepository);
            CheckForNull(UserCreditService);

            if (UserRepository == null)
            {
                throw new ArgumentNullException(nameof(UserRepository));
            }

            var client = ClientRepository.GetClientById(clientId);

            var user = new User
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surname,
                Client = client
            };

            bool userBaseDataIsNotValid = !UserBaseValidator.Validate(user);

            if (userBaseDataIsNotValid)
            {
                return false;
            }

            user.CreditLimit = UserCreditService.GetUserCreditLimit(user);
            var clientCreditLimitInfo = UserCreditService.GetClientCreditLimitInfo(user.Client);
            user.UpdateCreditLimit(clientCreditLimitInfo);

            bool userCreditDataIsNotValid = !UserCreditValidator.Validate(user);

            if (userCreditDataIsNotValid)
            {
                return false;
            }

            UserRepository.AddUser(user);

            return true;
        }

        public void Dispose()
        {
            ClientRepository?.Dispose();
            UserRepository?.Dispose();
            UserCreditService?.Dispose();
        }

        private void CheckForNull<T>(T t) where T : class
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}