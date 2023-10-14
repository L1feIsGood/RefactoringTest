using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IUserValidator userValidator;
        private readonly IUserCreator userCreator;

        public UserService()
        {
            userCreator = new UserCreator();
            userValidator = new UserValidator();
        }
        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var isUserValid = userValidator.IsValid(firstName, surname, email, dateOfBirth);
            if (!isUserValid)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = userCreator.CreateUserFromClient(client, firstName, surname, email, dateOfBirth);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}