namespace LegacyApp.UserCreditLimitFiller
{
    public class DefaultUserCreditLimitFiller : UserCreditLimitFillerBase
    {
        public override void FillUserCreditLimit(User user)
        {
            DefaultFillUserCreditLimit(user);
        }
    }
}
