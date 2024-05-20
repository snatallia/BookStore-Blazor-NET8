using AutoMapper;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorUpdateDto, AuthorReadOnlyDto>().ReverseMap();
            CreateMap<AuthorUpdateDto, AuthorDetailsDto>().ReverseMap();
        }
    }
}
