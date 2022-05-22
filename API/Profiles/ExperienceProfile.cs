using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class ExperienceProfile:Profile
    {
        public ExperienceProfile()
        {
            CreateMap<Experience, ExperienceReadDto>();
            CreateMap<ExperienceCreateDto, Experience>();
            CreateMap<ExperienceUpdateDto, Experience>();
            CreateMap<Experience, ExperienceUpdateDto>();
        }
    }
}
