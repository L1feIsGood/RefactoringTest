using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditLimitChecker _userCreditLimitChecker;
        private readonly IUserParamsValidator _userParamsValidator;

        public UserService()
        {
            _clientRepository = ServiceCreator.Creator.CreateClientRepository();
            _userCreditLimitChecker = ServiceCreator.Creator.CreateUserCreditLimitChecker();
            _userParamsValidator = ServiceCreator.Creator.CreateUserParamsValidator();
        }
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            #region Валидация входных параметров
            if (!_userParamsValidator.UserNameValidate(firName, surname))
            {
                return false;
            }
            if (!_userParamsValidator.UserMailValidate(email))
            {
                return false;
            }
            if (!_userParamsValidator.UserAgeValidate(dateOfBirth))
            {
                return false;
            }
            #endregion

            var client = _clientRepository.GetById(clientId);
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };

            if (!_userCreditLimitChecker.CheckCreditLimit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}