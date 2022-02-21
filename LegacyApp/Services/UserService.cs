using System;
using LegacyApp.DTO;
using LegacyApp.Services;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICreditLimitService _creditLimitService;
        private readonly IUserDataAccessService _userDataAccessService;
        public UserService(IClientRepository clientRepository, ICreditLimitService creditLimitService,
            IUserDataAccessService userDataAccessService)
        {
            _clientRepository = clientRepository;
            _creditLimitService = creditLimitService;
        }

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var newUser = new User
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };

            if (!ValidateUser(newUser))
                return false;

            var client = _clientRepository.GetById(clientId);

            newUser.Client = client;

            newUser = _creditLimitService.SetCreditLimit(newUser);

            if (newUser.HasCreditLimit && newUser.CreditLimit < 500)
            {
                return false;
            }

            _userDataAccessService.AddUser(newUser);

            return true;
        }

        bool ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.Surname))
                return false;

            if (!user.EmailAddress.Contains("@") && !user.EmailAddress.Contains("."))
                return false;

            return ValidateUserAge(user);
        }

        private bool ValidateUserAge(User user)
        {
            var now = DateTime.Now;
            int age = now.Year - user.DateOfBirth.Year;
            if (now.Month < user.DateOfBirth.Month
                || (now.Month == user.DateOfBirth.Month && now.Day < user.DateOfBirth.Day))
            {
                age--;
            }

            return !(age < 21);
        }
    }
}