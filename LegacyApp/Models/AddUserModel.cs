using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class AddUserModel
    {
        public string FirName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClientId { get; set; }

        public AddUserModel(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            FirName = firName;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
            ClientId = clientId;
        }
    }
}
