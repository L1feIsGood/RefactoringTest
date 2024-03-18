using System;
using LegacyApp.Models;

namespace LegacyApp.UserValidation
{
    public class DateOfBirthUserValidationRule : IUserValidationRule
    {
        private int _minimumAge;

        public DateOfBirthUserValidationRule(int minimumAge)
        {
            if (minimumAge < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumAge));
            }

            _minimumAge = minimumAge;
        }

        public bool IsUserDataValid(User user)
        {
            var now = DateTime.Now;
            int age = now.Year - user.DateOfBirth.Year;
            
            if (now.Month < user.DateOfBirth.Month || (now.Month == user.DateOfBirth.Month && now.Day < user.DateOfBirth.Day))
            {
                age--;
            }

            return age > _minimumAge;
        }
    }
}
