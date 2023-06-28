using System;

namespace LegacyApp
{
    public class UserParamsValidator : IUserParamsValidator
    {
        private readonly int _minUserAge;

        public UserParamsValidator(int minUserAge)
        {
            _minUserAge = minUserAge;
        }

        public bool UserNameValidate(string firstName, string surName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surName))
            {
                return false;
            }
            return true;
        }

        public bool UserMailValidate(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            return true;
        }

        public bool UserAgeValidate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            if (age < _minUserAge)
            {
                return false;
            }
            return true;
        }
    }
}
