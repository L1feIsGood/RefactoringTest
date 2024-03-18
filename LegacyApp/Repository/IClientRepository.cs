using LegacyApp.Models;
using System;

namespace LegacyApp.Repository
{
    public interface IClientRepository : IDisposable
    {
        Client GetClientById(int id);
    }
}