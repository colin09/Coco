using AutoMapper;
using ERP.Domain.Contract.AmoebaModule.AmoebaReportModule;
using ERP.Domain.Model.AmoebaReportModule;

namespace ERP.Common.Mappers.AmoebaModule {

    public class AmoebaMapperConfig : IAutoMapperConfig {

        public void Map (IMapperConfigurationExpression config) {

            config.CreateMap<AmoebaReportDTO, AmoebaReport> ()
                .ForPath (m => m.Id, opt => opt.Ignore ())
                .ForPath (m => m.DataMonth, opt => opt.Ignore ())
                .ForPath (m => m.CreateTime, opt => opt.Ignore ())
                .ForPath (m => m.LastUpdateTime, opt => opt.Ignore ())
                .ForMember (m => m.City, opt => opt.Ignore ());

            config.CreateMap<AmoebaReport, AmoebaReportVO> ()
                .ForMember (m => m.CityName, opt => opt.MapFrom (o => o.City.Name));
        }

    }

    public class AmoebaMappingProfile : Profile {

        public AmoebaMappingProfile () {

            CreateMap<AmoebaReportDTO, AmoebaReport> ()
                .ForPath (m => m.Id, opt => opt.Ignore ())
                .ForPath (m => m.DataMonth, opt => opt.Ignore ())
                .ForPath (m => m.CreateTime, opt => opt.Ignore ())
                .ForPath (m => m.LastUpdateTime, opt => opt.Ignore ())
                .ForMember (m => m.City, opt => opt.Ignore ());

            CreateMap<AmoebaReport, AmoebaReportVO> ()
                .ForMember (m => m.CityName, opt => opt.MapFrom (o => o.City.Name));
            CreateMap<AmoebaReport, AmoebaReportSimpleVO> ()
                .ForMember (m => m.CityName, opt => opt.MapFrom (o => o.City.Name));
        }
    }

}