using System;

namespace LegacyApp.Services
{
    public interface IUserValidator
    {
        /// <summary>
        /// Возвращает результат валидации
        /// </summary>
        /// <returns>Результат валидации</returns>
        bool GetResult();

        /// <summary>
        /// Валидирует часть имени
        /// </summary>
        /// <param name="namePart">Часть имени</param>
        void ValidateNamePart(string namePart);


        /// <summary>
        /// Валидирует email
        /// </summary>
        /// <param name="email"></param>
        void ValidateEmail(string email);

        /// <summary>
        /// Валидирует возраст
        /// </summary>
        /// <param name="dateOfBirth">DateTime Возраст</param>
        void ValidateAge(DateTime dateOfBirth);

        /// <summary>
        /// Выполняет валидацию кредитоспособности пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        void ValidateCreditworthiness(User user);
    }
}