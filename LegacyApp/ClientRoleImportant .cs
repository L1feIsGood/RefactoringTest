namespace LegacyApp
{
    class ClientRoleImportant : IClientRole
    {
        public string ClientType { get; set; }
        public bool HasCreditLimit { get; set; }
        public ClientRoleImportant()
        {
            ClientType = "ImportantClient";
            HasCreditLimit = true;
        }
        public int CalculateCreditLimit(User user)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                return creditLimit = creditLimit * 2;
            }
        }
    }
}
