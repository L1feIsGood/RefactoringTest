namespace LegacyApp.UserCreditLimitFiller
{

    public abstract class UserCreditLimitFillerBase
    {
        public abstract void FillUserCreditLimit(User user);

        protected void DefaultFillUserCreditLimit(User user)
        {
            user.HasCreditLimit = true;

            using (var userCreditService = new UserCreditServiceClient())
            {
                user.CreditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
            }
        }
    }
}
