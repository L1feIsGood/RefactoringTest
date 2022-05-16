using System;

namespace LegacyApp
{
    public class CreateUserParams
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClientId { get; set; }

        public bool IsValid()
        {
            const int minAge = 21;
            
            var isNameValid = !string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(Surname);
            if (!isNameValid)
                return false;

            var isEmailValid = Email.Contains("@") && Email.Contains(".");
            if (!isEmailValid)
                return false;

            var now = DateTime.Now;
            var age = now.Year - DateOfBirth.Year;
            var hadBirthdayThisYear = now.Month < DateOfBirth.Month ||
                                      (now.Month == DateOfBirth.Month && now.Day < DateOfBirth.Day);
            if (!hadBirthdayThisYear)
                age--;

            if (age < minAge)
                return false;

            return true;
        }
    }
}