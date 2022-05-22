using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class SpecializationsProfile:Profile
    {
        public SpecializationsProfile()
        {
            CreateMap<Specialization, SpecializationReadDto>();
            CreateMap<SpecializationCreateDto, Specialization>();
            CreateMap<SpecializationUpdateDto, Specialization>();
            CreateMap<Specialization, SpecializationUpdateDto>();
        }
    }
}
