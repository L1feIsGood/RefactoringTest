namespace LegacyApp.UserCreditLimitFiller
{
    public class ImportantClientCreditLimitFiller : UserCreditLimitFillerBase
    {
        public override void FillUserCreditLimit(User user)
        {
            DefaultFillUserCreditLimit(user);
            user.CreditLimit *= 2;
        }
    }
}
