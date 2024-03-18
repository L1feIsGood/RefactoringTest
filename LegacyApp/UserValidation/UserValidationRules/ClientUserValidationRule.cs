
using LegacyApp.Models;

namespace LegacyApp.UserValidation
{
    internal class ClientUserValidationRule : IUserValidationRule
    {
        public bool IsUserDataValid(User user)
        {
            if (user.Client == null)
            {
                return false;
            }

            return !string.IsNullOrEmpty(user.Client.Name);
        }
    }
}
