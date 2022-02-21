using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Services
{
    public interface IUserDataAccessService
    {
        void AddUser(User user);
    }

    public class UserDataAccessService : IUserDataAccessService
    {
        public void AddUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }
}
