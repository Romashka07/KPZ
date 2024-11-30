using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;
using Stream = Lab3KPZ_CF_.Data.Stream;

namespace Lab3KPZ_CF_.Mappers
{
    public class StreamMapper : Profile
    {
        public StreamMapper()
        {
            CreateMap<StreamViewModel, Stream>()
            .ForMember(dest => dest.CourseID, opt => opt.MapFrom(src => src.CourseID));
            CreateMap<Stream, StreamViewModel>();
        }
    }
}
