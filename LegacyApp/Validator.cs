using System;

namespace LegacyApp
{
    class Validator
    {
        const int CreditLimit = 500;
        public bool IsNameValid(string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName);
        }
        public bool IsEmailValid(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }
        public bool IsAgeValid(DateTime dateOfBirth)
        {         
            return User.CalculateAge(dateOfBirth) >= 21;
        }
        public bool IsCreditLimitValid(int creditLimit)
        {
            return creditLimit < CreditLimit;
        }
    }
}
