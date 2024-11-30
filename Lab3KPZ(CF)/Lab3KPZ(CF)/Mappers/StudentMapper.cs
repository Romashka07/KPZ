using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;

namespace Lab3KPZ_CF_.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))  // Мапимо ім'я
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.User.SecondName));    // Мапимо прізвище

            CreateMap<StudentViewModel, Student>();
        }
    }
}
