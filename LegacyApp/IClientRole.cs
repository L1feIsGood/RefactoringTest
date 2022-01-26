namespace LegacyApp
{
    interface IClientRole
    {
        string ClientType { get; set; }
        bool HasCreditLimit { get; set; }
        int CalculateCreditLimit(User user);
    }
}
