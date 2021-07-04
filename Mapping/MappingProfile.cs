using System.Linq;
using AutoMapper;
using NetCoreAng.Controllers.Resources;
using NetCoreAng.Core.Models;

namespace NetCoreAng.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => 
                new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail})
            )
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
            .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => 
                new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail})
            )
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(
                vf => new KeyValuePairResource {Id = vf.Feature.Id, Name = vf.Feature.Name})));

            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Photo, PhotoResource>();

            //API Resource to Domain
            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email)) 
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(vr => vr.Features, opt => opt.Ignore())
            //.ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature {FeatureId = id})));  
            .AfterMap((vr, v) => {
                //Remove unselected features
                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));

                foreach (var rf in removedFeatures) {
                    v.Features.Remove(rf);
                }

                //Add new features
                var insertedFeatures =  vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id))
                .Select(id => new VehicleFeature { FeatureId = id});

                foreach (var f in insertedFeatures) {
                    v.Features.Add(f);
                }
            });
             CreateMap<VehicleQueryResource, VehicleQuery>();
        }
    }
}