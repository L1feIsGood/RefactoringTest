namespace LegacyApp
{
    public interface ICreditLimitService
    {
        User SetCreditLimit(User user);
    }

    public class CreditLimitService : ICreditLimitService
    {
        public User SetCreditLimit(User user)
        {
            if (user.Client.Name == "VeryImportantClient")
            {
                // Пропустить проверку лимита
                user.HasCreditLimit = false;
            }
            else if (user.Client.Name == "ImportantClient")
            {
                // Проверить лимит и удвоить его
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Проверить лимит
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            return user;
        }
    }
}