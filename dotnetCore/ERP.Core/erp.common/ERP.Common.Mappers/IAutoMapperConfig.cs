using System;
using AutoMapper;

namespace ERP.Common.Mappers
{
    public interface IAutoMapperConfig
    {
        void Map(IMapperConfigurationExpression config);
    }
}