using System;

namespace LegacyApp
{
    public class PersonalDataValidationServiceClient : IPersonalDataValidationService
    {
        private readonly int mMinAge = 21;


        public static IPersonalDataValidationService Create()
        {
            return new PersonalDataValidationServiceClient();
        }


        public bool Validate(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("User has to be not null");
            }

            return CheckFirstName(user.FirstName)
                    && CheckSurname(user.Surname)
                    && CheckEMail(user.EmailAddress)
                    && CheckAge(user.DateOfBirth);
        }


        #region Private methods

        private bool CheckFirstName(string firstName)
        {
            return !string.IsNullOrEmpty(firstName);
        }

        private bool CheckSurname(string surname)
        {
            return !string.IsNullOrEmpty(surname);
        }

        private bool CheckEMail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool CheckAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if(now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            if(age < mMinAge)
            {
                return false;
            }

            return true;
        } 

        #endregion
    }
}
