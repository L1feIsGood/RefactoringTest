using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var validator = new UserValidator();
            var configurator = new UserConfigurator();
            
            if (!validator.IsUserDataValid(firName, surname, email, dateOfBirth))
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };
            
            configurator.ConfigureUser(user,client);

            if (validator.IsUserCreditLimitValid(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}