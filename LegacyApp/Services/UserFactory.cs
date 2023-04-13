using LegacyApp.Models;
using LegacyApp.Repositories;

namespace LegacyApp.Services
{
    internal class UserFactory
    {
        private readonly IClientRepository _clientRepository;
        private readonly UserCreationParamsValidator _userCreationParamsValidator;

        public UserFactory(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _userCreationParamsValidator = new UserCreationParamsValidator();
        }

        public User CreateUserOrDefault(UserCreationParams userCreationParams)
        {
            var isParamsValid = _userCreationParamsValidator.ValidateParams(userCreationParams);
            if (!isParamsValid)
                return null;

            var client = _clientRepository.GetById(userCreationParams.ClientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = userCreationParams.DateOfBirth,
                EmailAddress = userCreationParams.Email,
                FirstName = userCreationParams.FirstName,
                Surname = userCreationParams.Surname
            };

            return user;
        }
    }
}