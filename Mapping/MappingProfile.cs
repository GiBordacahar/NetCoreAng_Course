using AutoMapper;
using NetCoreAng.Controllers.Resources;
using NetCoreAng.Models;

namespace NetCoreAng.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
        }
    }
}