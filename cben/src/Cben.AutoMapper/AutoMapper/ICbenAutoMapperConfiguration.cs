using System;
using System.Collections.Generic;
using AutoMapper;

namespace Cben.AutoMapper
{
    public interface ICbenAutoMapperConfiguration
    {
        List<Action<IMapperConfigurationExpression>> Configurators { get; }
    }
}