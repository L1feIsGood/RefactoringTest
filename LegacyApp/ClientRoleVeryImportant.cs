namespace LegacyApp
{
    class ClientRoleVeryImportant : IClientRole
    {
        public string ClientType { get; set; }
        public bool HasCreditLimit { get; set; }
        public ClientRoleVeryImportant()
        {
            ClientType = "VeryImportantClient";
            HasCreditLimit = false;
        }
        public int CalculateCreditLimit(User user)
        {
            return 0;       
        }
    }
}
