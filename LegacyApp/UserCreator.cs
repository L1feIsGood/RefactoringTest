using System;

namespace LegacyApp
{
    public class UserCreator : IUserCreator
    {
        private const string veryImportantClientName = "VeryImportantClient";
        private const string importantClient = "ImportantClient";

        public User CreateUserFromClient(Client client, string firstName, string surname, string email, DateTime dateOfBirth)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surname
            };

            FillUserCreditLimit(user);

            return user;
        }

        private void FillUserCreditLimit(User user)
        {
            if (user.Client.Name == veryImportantClientName)
            {
                //Пропускаем проверку лимита и не уставнавливаем кредитный лимит
                user.HasCreditLimit = false;
            }
            else
            {
                user.HasCreditLimit = true;

                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    creditLimit =
                        user.Client.Name == importantClient
                        ? creditLimit * 2
                        : creditLimit;
                    user.CreditLimit = creditLimit;
                }
            }
        }
    }
}
