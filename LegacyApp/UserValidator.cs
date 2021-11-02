namespace LegacyApp
{
    public class UserValidator
    {
        public bool IsNameValid(string firstName, string surname)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(surname);
        }

        public bool IsEmailValid(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        public bool IsAgeValid(int age)
        {
            return age > 21 ? true : false;
        }

        public bool IsCreditLimitValid(User user)
        {
            return !user.HasCreditLimit && !(user.CreditLimit < 500);
        }
    }
}