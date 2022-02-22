using System;

namespace LegacyApp
{
    public class DefaultDateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
