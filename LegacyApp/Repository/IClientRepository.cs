using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Repository
{
    internal interface IClientRepository
    {
        Client GetById(int id);
    }
}
