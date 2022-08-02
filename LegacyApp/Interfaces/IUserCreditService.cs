using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public interface IUserCreditService
    {
        int GetCreditLimit(string firstName, string surname, DateTime dateOfBirth);
    }
}
