using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public interface IValidationService<T>
    {
        bool Validate(T obj);
    }
}
