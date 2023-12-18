using AutoMapper;
using WebLibrary.DAL.Models;
using WebLibrary.Logic.DTOs;
using WebLibrary.Logic.Helpers;

namespace WebLibrary.Logic.MappingProfiles
{
    public class DatabaseObjectsToDTOs : Profile
    {
        public DatabaseObjectsToDTOs()
        {
            CreateMap<AuthorDto, Author>().ReverseMap();
            CreateMap<BookDto, Book>().ForMember(dest => dest.Authors, opt => opt.MapFrom(_ => _.AuthorsIds.ToList()))
                .ReverseMap();

            CreateMap<Guid, Author>().ForMember(dest => dest.Id, opt => opt.MapFrom(_ => _));
            CreateMap<string, byte[]>().ConvertUsing<Base64Converter>();
            CreateMap<byte[], string>().ConvertUsing<Base64Converter>();

        }

    }
}
