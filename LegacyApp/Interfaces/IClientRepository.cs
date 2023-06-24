using System;

namespace LegacyApp
{
    interface IClientRepository
    {
        Client GetById(int id);
    }
}