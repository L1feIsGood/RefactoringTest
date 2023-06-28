using System;

namespace LegacyApp
{
    public interface IUserParamsValidator
    {
        bool UserAgeValidate(DateTime dateOfBirth);
        bool UserMailValidate(string email);
        bool UserNameValidate(string firstName, string surName);
    }
}
