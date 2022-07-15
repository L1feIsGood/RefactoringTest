using System;

namespace LegacyApp.Services
{
    /// <summary>
    /// Валидатор пользователя
    /// </summary>
    public class UserValidator
    {
        /// <summary>
        /// Валидирует часть имени
        /// </summary>
        /// <param name="namePart">Часть имени</param>
        /// <returns>True, если валидация успешна</returns>
        public bool ValidateNamePart(string namePart)
        {
            return !string.IsNullOrEmpty(namePart);
        }


        /// <summary>
        /// Валидирует email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True, если валидация успешна</returns>
        public bool ValidateEmail(string email)
        {
            bool output = true;

            output &= email.Contains("@");               // содержит собачку
            output &= email.Contains(".");               // содержит точку
            output &= !string.IsNullOrWhiteSpace(email);  // не пустая строка
            
            return output;
        }

        /// <summary>
        /// Валидирует возраст
        /// </summary>
        /// <param name="dateOfBirth">DateTime Возраст</param>
        /// <returns>True, если валидация успешна</returns>
        public bool ValidateAge(DateTime dateOfBirth)
        {
            bool output = true;

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;

            // вычисление полных лет
            if (now < dateOfBirth.AddYears(age))
            {
                age--;
            }

            if (age < GlobalConfig.minAge)
            {
                output = false;
            }

            return output;
        }
    }
}