using AutoMapper;
using University.Database.Entities;
using University.DTOs;

namespace University.AutoMapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Career, CareerDTO>().ReverseMap();
        CreateMap<Faculty, FacultyDTO>().ReverseMap();
        CreateMap<Career, CareerCreationDTO>().ReverseMap();
        CreateMap<Faculty, FacultyCreationDTO>().ReverseMap();
        CreateMap<Inherent, InherentDTO>().ReverseMap();
    }
}

