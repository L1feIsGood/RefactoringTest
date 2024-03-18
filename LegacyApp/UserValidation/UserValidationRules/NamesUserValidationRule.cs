
using LegacyApp.Models;

namespace LegacyApp.UserValidation
{
    public class NamesUserValidationRule : IUserValidationRule
    {
        public bool IsUserDataValid(User user)
        {
            return !(string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.Surname));
        }
    }
}
