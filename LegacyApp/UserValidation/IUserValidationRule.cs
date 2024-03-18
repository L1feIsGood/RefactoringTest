
using LegacyApp.Models;

namespace LegacyApp.UserValidation
{
    public interface IUserValidationRule
    {
        bool IsUserDataValid(User user);
    }
}
