using System;

namespace LegacyApp
{
    public class UserService
    {
        bool Validate(string firstName, string surname, string userEmail, int clientAge, User user)
        {
            if (
                (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname)) || // Проверка фио
                !(userEmail.Contains("@") || userEmail.Contains(".")) || // Проверка на присутствие '@' и '.'
                clientAge<21 || // Проверка на возраст
                user.HasCreditLimit && user.CreditLimit < 500 // Проверка на малый кредитный лимит
            )
            {
                return false;
            }
            return true;
        }
        
        public bool AddUser(string firstName, string surname, string userEmail, DateTime dateOfBirth, int clientId)
        {
            #region подготовка данных для валидации
            var today = DateTime.Now;
            int clientAge = today.Year - dateOfBirth.Year;
            if (today < dateOfBirth) clientAge--;
            
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetClientById(clientId);

            var userToAdd = new User
            {
                UserClient = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = userEmail,
                FirstName = firstName,
                Surname = surname
            };
            #endregion
            
            // Валидация
            if (!Validate(firstName, surname, userEmail, clientAge, userToAdd)){return false;}
            #region проверить, насколько важен клиент
            if (client.Name == "VeryImportantClient")
            {
                userToAdd.HasCreditLimit = false;
            }
            
            else if (client.Name == "ImportantClient")
            {
                userToAdd.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    userToAdd.CreditLimit = 2*userCreditService.GetCreditLimit(userToAdd.FirstName, userToAdd.Surname, userToAdd.DateOfBirth);
                }
            }
            
            else
            {
                userToAdd.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    userToAdd.CreditLimit = userCreditService.GetCreditLimit(userToAdd.FirstName, userToAdd.Surname, userToAdd.DateOfBirth);
                }
            }
            #endregion

            UserDataAccess.AddUser(userToAdd);
            return true;
        }
    }
}