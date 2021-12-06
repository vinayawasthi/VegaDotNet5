using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Web.ApiResources;
using Vega.Web.Models;

namespace Vega.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(v => v.Person.Name, o => o.MapFrom(x => x.PersonName))
                .ForMember(v => v.Person.Email, o => o.MapFrom(x => x.PersonEmail))
                .ForMember(v => v.Person.Phone, o => o.MapFrom(x => x.PersonPhone))
                .ForMember(v=>v.Features, o=> o.MapFrom(x=> x.Features.Select(x=> x.FeatureId)));

            // API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.PersonName, o => o.MapFrom(x => x.Person.Name))
                .ForMember(v => v.PersonEmail, o => o.MapFrom(x => x.Person.Email))
                .ForMember(v => v.PersonPhone, o => o.MapFrom(x => x.Person.Phone))
                .ForMember(v => v.Features, o => o.MapFrom(x => x.Features.Select(id => new VehicleFeature { FeatureId = id })));

        }
    }
}
