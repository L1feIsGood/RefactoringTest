using LegacyApp.Factories;
using System;

namespace LegacyApp
{
    public class UserService
    {
        IUserFactory userFactory;

        public UserService()
        {
            IClientRepository clientRepository = new ClientRepository();

            ICreditLimitProviderFactory creditLimitProviderFactory = 
                new CreditLimitProviderFactory(new UserCreditServiceClient());

            userFactory = new UserFactory(clientRepository, creditLimitProviderFactory);
        }


        public bool AddUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId)
        {

            // создание пользователя
            User user = userFactory.CreateUser(firstname, surname, email, dateOfBirth, clientId);

            if (user == null)
            {
                return false;
            }


            UserDataAccess.AddUser(user);

            return true;
        }
    }
}