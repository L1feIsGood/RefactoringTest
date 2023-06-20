using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetTodayAge(this DateTime date)
        {
            var now = DateTime.Now;
            int age = now.Year - date.Year;

            if (now.Month < date.Month || (now.Month == date.Month && now.Day < date.Day))
                age--;

            return age;
        }
    }
}
