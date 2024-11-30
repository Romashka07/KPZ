using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;


namespace Lab3KPZ_CF_.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Мапінг між моделями Course та CourseViewModel
            CreateMap<Course, CourseViewModel>();  // Для перетворення з Course в CourseViewModel
            CreateMap<CourseViewModel, Course>();  // Для перетворення з CourseViewModel в Course
        }

    }
}
