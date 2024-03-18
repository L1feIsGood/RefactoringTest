using LegacyApp.Models;
using System;

namespace LegacyApp.Repository
{
    public interface IUserRepository : IDisposable
    {
        void AddUser(User user);
    }
}
