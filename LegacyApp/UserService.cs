using System;

namespace LegacyApp
{
    /*
    Не успел реализовать все идеи, которые есть.
    Если бы я продолжил рефакторить, я бы применил тут Dependency Injection 
    и избавился бы от зависимостей на реализации (User, Client, ClientRepository и пр.)
     */

    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime birthDate, int clientId)
        {
            if (IsInvalidInput(firstName, lastName, email, birthDate)) return false;

            var client = GetClientById(clientId);
            var user = new User(client, birthDate, email, firstName, lastName);

            if (user.HasCreditLimit)
            {
                var creditLimit = GetUserCreditLimit(user);

                if (client.Name == "ImportantClient")
                {
                    creditLimit *= 2;
                }

                user.CreditLimit = creditLimit;

                if (creditLimit < 500)
                {
                    return false;
                }
            }

            UserDataAccess.AddUser(user);

            return true;
        }

        private Client GetClientById(int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            return client;
        }

        private int GetUserCreditLimit(User user)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.LastName, user.BirthDate);
                return creditLimit;
            }
        }

        private bool IsInvalidInput(string firstName, string lastName, string email, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return true;
            }

            if (IsInvalidEmail(email))
            {
                return true;
            }

            if (IsInvalidAge(birthDate))
            {
                return true;
            }

            return false;
        }

        private bool IsInvalidEmail(string email)
        {
            if (string.IsNullOrEmpty(email) ||
                !email.Contains("@") ||
                !email.Contains("."))
            {
                return true;
            }

            return false;
        }
        private bool IsInvalidAge(DateTime birthDate)
        {
            var clientAge = GetAge(birthDate);

            if (clientAge < 21)
            {
                return true;
            }

            return false;
        }


        private int GetAge(DateTime birthDate)
        {
            var currentTime = DateTime.Now;
            var age = CalculateAge(birthDate, currentTime);

            return age;
        }

        private int CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            var age = birthDate.Year - currentDate.Year;

            if (IsFutureBirthDate(birthDate, currentDate))
            {
                age--;
            }

            return age;
        }

        private bool IsFutureBirthDate(DateTime birthDate, DateTime currentDate)
        {
            return currentDate.Month < birthDate.Month ||
                   (currentDate.Month == birthDate.Month &&
                    currentDate.Day < birthDate.Day);
        }
    }
}