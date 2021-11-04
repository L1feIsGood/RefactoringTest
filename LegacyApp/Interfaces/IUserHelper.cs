namespace LegacyApp
{
    using System;

    public interface IUserHelper
    {
        bool IsStringEmptyOrNull(string firName);
        bool IsEmailCorrect(string email);
        bool IsAgeCorrect(DateTime dateBirthday);
        bool IsCreditLimitCorrect(bool hasCreditLimit, int creditLimit);

    }
}
