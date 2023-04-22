using AutoMapper;
using PRAS.DataTransferObjects;
using PRAS.Models;

namespace PRAS.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.RoleName, opt => opt.MapFrom(e => e.Role.Name));

            CreateMap<NewsForCreationDto, News>();
            CreateMap<News, NewsDetailDto>()
                .ForMember(n => n.AuthorEmail, opt => opt.MapFrom(u => u.Author.Email));
            CreateMap<News, NewsDto>();
        }
    }
}
