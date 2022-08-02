using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IValidationService<AddUserModel> _validationService;
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditLimitConfigurationService _creditLimitService;

        public UserService()
        {
            _validationService = new AddUserModelValidationService();
            _clientRepository = new ClientRepository();
            _creditLimitService = new UserCreditLimitConfigurationService();
        }

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var addUserModel = new AddUserModel(firName, surname, email, dateOfBirth, clientId);

            if (!_validationService.Validate(addUserModel))
            {
                return false;
            }

            var client = _clientRepository.GetById(addUserModel.ClientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = addUserModel.DateOfBirth,
                EmailAddress = addUserModel.Email,
                FirstName = addUserModel.FirName,
                Surname = addUserModel.Surname
            };

            var configureCreditLimitResult = _creditLimitService.ConfigureCreditLimit(user);
            if (!configureCreditLimitResult)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}