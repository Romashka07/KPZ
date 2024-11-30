using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;
using Stream = Lab3KPZ_CF_.Data.Stream;

namespace Lab3KPZ_CF_.Mappers
{
    public class GroupMapper : Profile
    {
        public GroupMapper()
        {
            CreateMap<Group, GroupViewModel>()
               .ForMember(dest => dest.StreamID, opt => opt.MapFrom(src => src.StreamID));
            CreateMap<GroupViewModel, Group>()
               .ForMember(dest => dest.StreamID, opt => opt.MapFrom(src => src.StreamID));

        }
    }
}
