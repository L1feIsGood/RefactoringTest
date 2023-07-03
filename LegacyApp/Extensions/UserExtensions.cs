namespace LegacyApp.Extensions
{
    public static class UserExtensions
    {
        public static int GetUserCreditLimit(this User user)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                return creditLimit;
            }
        }

        public static bool CheckUserCreditLimit(this User user)
        {
            bool flag = true;
            if (user.HasCreditLimit && user.CreditLimit < 500)
                flag = false;
            return flag;
        }
    }
}

