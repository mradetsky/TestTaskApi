using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskApi.Dto;
using TestTaskApi.EF.Entities;

namespace TestTaskApi.Mapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration CreateAndRegisterMapper()
        {
            var config = new MapperConfiguration(Register);
            config.AssertConfigurationIsValid();

            return config;
        }
        private static void Register(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Resource, ResourceListDto>()
                .ForMember(dest => dest.Id,
                           opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                           opts => opts.MapFrom(src => src.Name));
            cfg.CreateMap<Resource, ResourceDto>()
                .ForMember(dest => dest.CreatedOnUtc, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ReverseMap();
                

            cfg.CreateMap<Resource, ResourceEditDto>()
                .ReverseMap();


        }
    }
}
