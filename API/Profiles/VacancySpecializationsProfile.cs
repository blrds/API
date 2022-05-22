using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class VacancySpecializationsProfile:Profile
    {
        public VacancySpecializationsProfile()
        {
            CreateMap<VacancySpecialization, VacancySpecializationReadDto>();
            CreateMap<VacancySpecializationCreateDto, VacancySpecialization>();
            CreateMap<VacancySpecializationUpdateDto, VacancySpecialization>();
            CreateMap<VacancySpecialization, VacancySpecializationUpdateDto>();
        }
    }
}
