using System;

namespace LegacyApp
{
    public class UserService
    {
   
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (CheckName(firName, surname)) return false;

            if (CheckEmail(email)) return false;

            if (CheckAge(dateOfBirth, 21)) return false;

            ClientRepository clientRepository = new ClientRepository();
            Client client = clientRepository.GetById(clientId);

            User user = new User(client, dateOfBirth, email, firName, surname);

            SetCreditLimit(client, ref user);

            if (CheckCreditLimit(user, 500)) return false;

            UserDataAccess.AddUser(user);

            return true;
        }
        private bool CheckAge(DateTime dateOfBirth, int checkingAge)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age < checkingAge;
        }
        private bool CheckName(string firName, string surname)
        {
            return (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname));
        }
        private bool CheckEmail(string email)
        {
            return (!email.Contains("@") && !email.Contains("."));
        }
        private void SetCreditLimit(Client client, ref User user)
        {
            if (client.Name == "VeryImportantClient")
            {
                // Пропустить проверку лимита
                user.HasCreditLimit = false;
            }
            else if (client.Name == "ImportantClient")
            {
                // Проверить лимит и удвоить его
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Проверить лимит
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
        }
        private bool CheckCreditLimit(User user, int checkingLimit)
        {
            return (user.HasCreditLimit && user.CreditLimit < checkingLimit);
        }
    }
}
