using LegacyApp.UserCreditLimitFiller;
using System;

namespace LegacyApp
{
    public class UserCreator : IUserCreator
    {
        private readonly IUserCreditLimitFillerFactory userCreditLimitFillerFactory;

        public UserCreator()
        {
            userCreditLimitFillerFactory = new UserCreditLimitFillerFactory();
        }

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
            var creditFiller = userCreditLimitFillerFactory.GetUserCreditLimitFiller(user.Client.Name);
            creditFiller.FillUserCreditLimit(user);
        }
    }
}
