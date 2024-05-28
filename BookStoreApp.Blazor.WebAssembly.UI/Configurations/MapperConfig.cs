using AutoMapper;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorUpdateDto, AuthorReadOnlyDto>().ReverseMap();
            CreateMap<AuthorUpdateDto, AuthorDetailsDto>().ReverseMap();
            CreateMap<BookUpdateDto, BookReadOnlyDto>().ReverseMap();
            CreateMap<BookUpdateDto, BookDetailsDto>().ReverseMap();
        }
    }
}
