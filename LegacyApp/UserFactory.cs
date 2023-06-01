using System;

namespace LegacyApp
{
    class UserFactory : IUserFactory
    {
        public IUserCreditServiceDisposable creditService { get; set; }
        public IClientRepository clientRepository { get; set; }

        public UserFactory()
        {
            creditService = new UserCreditServiceClient();
            clientRepository = new ClientRepository();
        }

        public User Create(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var client = GetClientById(clientId);
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surname
            };

            FillCreditLimit(user);
            return user;
        }

        private Client GetClientById(int clientId)
        {
            return clientRepository.GetById(clientId);
        }

        private void FillCreditLimit(User user)
        {
            user.HasCreditLimit = true;

            switch (user.Client.Name)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    user.CreditLimit = GetCreditLimit(user) * 2;
                    break;
                default:
                    user.CreditLimit = GetCreditLimit(user);
                    break;
            }
        }

        private int GetCreditLimit(User user)
        {
            using (creditService)
            {
                return creditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
            }
        }
    }
}
