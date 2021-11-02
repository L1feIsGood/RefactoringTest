namespace LegacyApp
{
    public class ClientCreditCounter : IClientCreditCounter
    {
        private readonly UserCreditServiceClient _userCreditService;

        public ClientCreditCounter(UserCreditServiceClient userCreditService)
        {
            _userCreditService = userCreditService;
        }
        
        public CreditLimitModel GetCreditLimit(User user)
        {
            return new CreditLimitModel
            {
                HasCreditLimit = true,
                CreditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth)
            };
        }
    }
}