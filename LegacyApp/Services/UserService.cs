using System;
using LegacyApp.DTO;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICreditLimitService _creditLimitService;
        public UserService(IClientRepository clientRepository, ICreditLimitService creditLimitService)
        {
            _clientRepository = clientRepository;
            _creditLimitService = creditLimitService;
        }

        public bool AddUser(AddUserDTO newUserDto)
        {
            if (!ValidateUser(newUserDto))
                return false;

            var client = _clientRepository.GetById(newUserDto.Client.Id);

            var newUser = new User
            {
                Client = client,
                DateOfBirth = newUserDto.DateOfBirth,
                EmailAddress = newUserDto.EmailAddress,
                FirstName = newUserDto.FirstName,
                Surname = newUserDto.Surname
            };

            newUser = _creditLimitService.SetCreditLimit(newUser);

            if (newUser.HasCreditLimit && newUser.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(newUser);

            return true;
        }

        bool ValidateUser(AddUserDTO newUser)
        {
            if (string.IsNullOrEmpty(newUser.FirstName) || string.IsNullOrEmpty(newUser.Surname))
                return false;

            if (!newUser.EmailAddress.Contains("@") && !newUser.EmailAddress.Contains("."))
                return false;

            return ValidateUserAge(newUser);
        }

        private bool ValidateUserAge(AddUserDTO user)
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