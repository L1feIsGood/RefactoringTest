namespace LegacyApp.UserCreditLimitFiller
{
    public class VeryImportantClientCreditLimitFiller : UserCreditLimitFillerBase
    {
        public override void FillUserCreditLimit(User user)
        {
            //Пропускаем проверку лимита и не уставнавливаем кредитный лимит
            user.HasCreditLimit = false;
        }
    }
}
