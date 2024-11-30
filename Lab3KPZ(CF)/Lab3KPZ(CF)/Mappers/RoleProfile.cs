using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;

namespace Lab3KPZ_CF_.Mappers
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleViewModel, Role>();
        }
    }
}
