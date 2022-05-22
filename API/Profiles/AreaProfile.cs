using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class AreaProfile:Profile
    {
        public AreaProfile()
        {
            CreateMap<Area, AreaReadDto>();
            CreateMap<AreaCreateDto, Area>();
            CreateMap<AreaUpdateDto, Area>();
            CreateMap<Area, AreaUpdateDto>();
        }
    }
}
