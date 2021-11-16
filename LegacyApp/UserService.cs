using System;

namespace LegacyApp
{
    public class UserService
    {
        private const int MinimalGrantAge = 21; // минимальный возраст заемщика
        private const int BaseOfCreditLimit = 500; // ставка кредита, ниже которой отказывать в выдаче кредита 

        private bool IsNameValid(string firName, string surname)
        {
            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
            {
                return false;
            }
            return true;
        }

        private bool IsEmailValid(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            return true;
        }

        private bool IsEnoughAgeForCredit(DateTime dateOfBirth, int minimalGrantAge)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < minimalGrantAge)
            {
                return false;
            }
            return true;
        }

        /*
        * Класс имеет один единственный метод AddUser. Однако содержит в себе несколько обязанностей: валидация данных, получение статуса клиента, создание юзера, разбор типов клиента.
        * Рефакторинг данного метода заключается в том, чтобы разгрузить метод AddUser. Поместить валидацию данных в отдельные методы. Создание Юзера и разбор типа клиента поместить в класс User. Возраст заемщика, ставку кредита вынести в константы. "Предположим, что в коде нет проблем с точки зрения бизнес логики...." - По ТЗ, логика и код рабочий. Поэтому не стал менять валидацию данных firName, email и т.д 
        */

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsNameValid(firName, surname) && IsEmailValid(email) && IsEnoughAgeForCredit(dateOfBirth, MinimalGrantAge))
            {
                var clientRepository = new ClientRepository();
                var client = clientRepository.GetById(clientId);

                var user = new User();
                user = user.CreateUser(client, dateOfBirth, email, firName, surname);

                if (user.HasCreditLimit && user.CreditLimit < BaseOfCreditLimit)
                {
                    return false;
                }

                UserDataAccess.AddUser(user);

                return true;

            }
            else
            {
                return false;
            }

        }
    }
}