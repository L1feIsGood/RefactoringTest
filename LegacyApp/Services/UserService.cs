using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IUserFactory _userFactory;

        public UserService()
        {
            var clientRepository = new ClientRepository();
            var creditLimitProviderFactory = new CreditLimitProviderFactory(new UserCreditServiceClient());
            _userFactory = new UserFactory(clientRepository, creditLimitProviderFactory);
        }

        public UserService(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var user = _userFactory.Create(new CreateUserParams
            {
                FirstName = firName,
                Surname = surname,
                Email = email,
                DateOfBirth = dateOfBirth,
                ClientId = clientId
            });
            if (user == null)
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}