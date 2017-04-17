using System;
using System.Collections.Generic;
using AutoMapper;

namespace Cben.AutoMapper
{
    public class CbenAutoMapperConfiguration : ICbenAutoMapperConfiguration
    {
        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public CbenAutoMapperConfiguration()
        {
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}