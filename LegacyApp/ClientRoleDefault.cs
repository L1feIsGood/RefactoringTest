namespace LegacyApp
{
    class ClientRoleDefault : IClientRole
    {
        public string ClientType { get; set; }
        public bool HasCreditLimit { get; set; }

        public ClientRoleDefault()
        {
            ClientType = "";
            HasCreditLimit = true;
        }
        public int CalculateCreditLimit(User user)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                return userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
            }
        }
    }
}
