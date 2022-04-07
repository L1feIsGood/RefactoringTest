namespace LegacyApp
{
    public interface ICreditLimitsService
    {
        ICreditLimitsService SetCreditLimit(User user);
        bool ValidateCreditLimit(User user);
    }
}