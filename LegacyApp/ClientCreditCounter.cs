namespace LegacyApp
{
    public class ClientCreditCounter : IClientCreditCounter
    {
        public string ClientType { get; set; }
        private readonly UserCreditServiceClient _userCreditService;

        public ClientCreditCounter()
        {
            _userCreditService = new UserCreditServiceClient();
            ClientType = "";
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