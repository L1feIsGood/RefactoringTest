using System;

namespace LegacyApp
{
    public interface IUserCreditServiceDisposable : IUserCreditService, IDisposable
    {
    }
}