using System;
using System.Collections.Generic;

namespace Cben.Configuration.Startup
{
    public interface IValidationConfiguration
    {
        List<Type> IgnoredTypes { get; }
    }
}