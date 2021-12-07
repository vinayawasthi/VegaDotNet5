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
                .ForMember(v => v.Person, o => o.MapFrom(x => new Person() { Name = x.PersonName, Email = x.PersonEmail, Phone = x.PersonPhone }))
                .ForMember(v => v.Features, o => o.MapFrom(x => x.Features.Select(x => x.FeatureId)));


            // API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.PersonName, o => o.MapFrom(x => x.Person.Name))
                .ForMember(v => v.PersonEmail, o => o.MapFrom(x => x.Person.Email))
                .ForMember(v => v.PersonPhone, o => o.MapFrom(x => x.Person.Phone))
                .ForMember(v => v.Features, o => o.Ignore())
                .AfterMap((vr, v) =>
                {
                    // // REMOVE 
                    // var removeFeatures = new List<VehicleFeature>();
                    // foreach (var f in v.Features)
                    //     if (!vr.Features.Contains(f.FeatureId))
                    //         removeFeatures.Add(f);

                    // foreach (var f in removeFeatures)
                    //     v.Features.Remove(f);

                    // // ADD
                    // foreach(var id in vr.Features){
                    //     if(!v.Features.Any(x=>x.FeatureId==id))
                    //         v.Features.Add(new VehicleFeature(){
                    //             FeatureId = id
                    //         });
                    // }

                    // REMVOE
                    var removeFeatures = new List<VehicleFeature>();
                    removeFeatures = v.Features.Where(x => !vr.Features.Contains(x.FeatureId)).ToList();
                    foreach (var f in removeFeatures)
                        v.Features.Remove(f);

                    // ADD
                    var addFeatures = vr.Features.Where(x => !v.Features.Any(y => y.FeatureId == x))
                            .Select(id => new VehicleFeature() { FeatureId = id }).ToList();

                    foreach (var f in addFeatures)
                        v.Features.Add(f);
                });
        }
    }
}
