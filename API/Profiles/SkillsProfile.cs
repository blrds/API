using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class SkillsProfile:Profile
    {
        public SkillsProfile()
        {
            CreateMap<Skill, SkillReadDto>();
            CreateMap<SkillCreateDto, Skill>();
            CreateMap<SkillUpdateDto, Skill>();
            CreateMap<Skill, SkillUpdateDto>();
        }
    }
}
