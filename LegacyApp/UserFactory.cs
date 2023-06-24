using System;

namespace LegacyApp
{
    class UserFactory : IUserFactory
    {
        public IUserCreditService CreateUserCreditService() {
            return new UserCreditServiceClient();
        }

        public IClientRepository CreateClientRepository() {
            return new ClientRepository();
        }
    }
}