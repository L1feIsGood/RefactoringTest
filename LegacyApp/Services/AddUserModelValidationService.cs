using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class AddUserModelValidationService : IValidationService<AddUserModel>
    {
        public bool Validate(AddUserModel addUserModel)
        {
            if (string.IsNullOrEmpty(addUserModel.FirName) || string.IsNullOrEmpty(addUserModel.Surname))
            {
                return false;
            }

            if (!addUserModel.Email.Contains("@") && !addUserModel.Email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - addUserModel.DateOfBirth.Year;
            if (now.Month < addUserModel.DateOfBirth.Month || (now.Month == addUserModel.DateOfBirth.Month && now.Day < addUserModel.DateOfBirth.Day)) age--;

            const int minAge = 21;
            if (age < minAge)
            {
                return false;
            }

            return true;
        }
    }
}
