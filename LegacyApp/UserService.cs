using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > now.AddYears(-age)) age--;

            var clientRepository = new ClientRepository();
            Client client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };


            if (client.Name != "VeryImportantClient")
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    user.CreditLimit = userCreditService.GetCreditLimit(
                            user.FirstName,
                            user.Surname,
                            user.DateOfBirth);

                    // Если просто важный клиент, удваиваем лимит
                    if (client.Name == "ImportantClient")
                        user.CreditLimit *= 2;
                }
            }
            // Если очень важный клиент, отключаем лимит
            else
                user.HasCreditLimit = false;

            if (string.IsNullOrEmpty(firName)
                || string.IsNullOrEmpty(surname)
                || !email.Contains("@")
                || !email.Contains(".")
                || age < 21
                || user.HasCreditLimit
                && user.CreditLimit < 500)
            {
                return false;
            }
            else
            {
                UserDataAccess.AddUser(user);

                return true;
            }
        }
    }
}