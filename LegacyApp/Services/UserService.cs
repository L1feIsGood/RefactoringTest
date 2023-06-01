using System;

namespace LegacyApp
{
    public class UserService
    {
        private const int MINIMUM_AGE = 21;
        private const int MINIMUM_CREDIT_LIMIT = 500;

        IUserFactory userFactory;

        public UserService()
        {
            userFactory = new UserFactory();
        }

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
                return false;

            if (IsNotCorrectEmail(email))
                return false;

            if (AgeLessThanMinimum(dateOfBirth))
                return false;

            var user = userFactory.Create(firName, surname, email, dateOfBirth, clientId);

            if (HasCreditLimitLessThanMinimum(user))
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }

        private static bool IsNotCorrectEmail(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        private static bool AgeLessThanMinimum(DateTime dateOfBirth)
        {
            var age = GetAge(dateOfBirth);
            return age < MINIMUM_AGE;
        }

        private static int GetAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age;
        }

        private static bool HasCreditLimitLessThanMinimum(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < MINIMUM_CREDIT_LIMIT;
        }
    }
}