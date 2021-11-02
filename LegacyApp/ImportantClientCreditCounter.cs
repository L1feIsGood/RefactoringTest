namespace LegacyApp
{
    public class ImportantClientCreditCounter : IClientCreditCounter
    {
        private readonly UserCreditServiceClient _userCreditService;

        public ImportantClientCreditCounter(UserCreditServiceClient userCreditService)
        {
            _userCreditService = userCreditService;
        }
        
        public CreditLimitModel GetCreditLimit(User user)
        {
            return new CreditLimitModel
            {
                HasCreditLimit = true,
                CreditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth) * 2
            };
        }
    }
}