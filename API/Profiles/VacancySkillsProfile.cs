using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class VacancySkillsProfile:Profile
    {
        public VacancySkillsProfile()
        {
            CreateMap<VacancySkill, VacancySkillReadDto>();
            CreateMap<VacancySkillCreateDto, VacancySkill>();
            CreateMap<VacancySkillUpdateDto, VacancySkill>();
            CreateMap<VacancySkill, VacancySkillUpdateDto>();
        }
    }
}
