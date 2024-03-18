
using LegacyApp.Models;

namespace LegacyApp.UserValidation
{
    public class EmailUserValidationRule : IUserValidationRule
    {
        public bool IsUserDataValid(User user)
        {
            if (string.IsNullOrEmpty(user.EmailAddress))
            {
                return false;
            }

            return user.EmailAddress.Contains("@") && user.EmailAddress.Contains(".");
        }
    }
}
