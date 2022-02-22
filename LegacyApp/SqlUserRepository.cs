namespace LegacyApp
{
    public class SqlUserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }
}
