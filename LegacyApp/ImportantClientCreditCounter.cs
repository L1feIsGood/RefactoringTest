namespace LegacyApp
{
    public class ImportantClientCreditCounter : IClientCreditCounter
    {
        private readonly UserCreditServiceClient _userCreditService;
        public string ClientType { get; set; }

        public ImportantClientCreditCounter()
        {
            _userCreditService = new UserCreditServiceClient();
            ClientType = "ImportantClient";
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