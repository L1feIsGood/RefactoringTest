using LegacyApp.Models;

namespace LegacyApp.Repository
{
    internal class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            UserDataAccess.AddUser(user);
        }

        public void Dispose()
        {
        }
    }
}
