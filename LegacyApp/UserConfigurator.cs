namespace LegacyApp
{
    public class UserConfigurator
    {
        public void ConfigureUser(User user, Client client)
        {
            switch (client.Name)
            {
                case "VeryImportantClient":
                    // Пропустить проверку лимита
                    ConfigureUserAsVeryImportantClient(user);
                    break;
                case "ImportantClient":
                {
                    // Проверить лимит и удвоить его
                    ConfigureUserAsImportantClient(user);
                    break;
                }
                default:
                {
                    // Проверить лимит
                    ConfigureDefaultUser(user);
                    break;
                }
            }
        }

        private void ConfigureUserAsVeryImportantClient(User user)
        {
            user.HasCreditLimit = false;
        }
        
        private void ConfigureUserAsImportantClient(User user)
        {
            user.HasCreditLimit = true;
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
        }

        private void ConfigureDefaultUser(User user)
        {
            user.HasCreditLimit = true;
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }
        
    }
}