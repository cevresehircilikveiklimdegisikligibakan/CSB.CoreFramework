using AutoMapper;

namespace CSB.Core.Mappings
{
    public abstract class MappingProfile : Profile
    {
        public MappingProfile() : base() { }
        public MappingProfile(string profileName) : base(profileName) { }
    }
}