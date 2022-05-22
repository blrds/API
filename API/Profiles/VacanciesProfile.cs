using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class VacanciesProfile:Profile
    {
        public VacanciesProfile()
        {
            CreateMap<Vacancy, VacancyReadDto>();
            CreateMap<VacancyCreateDto, Vacancy>();
            CreateMap<VacancyUpdateDto, Vacancy>();
            CreateMap<Vacancy, VacancyUpdateDto>();
        }
    }
}
