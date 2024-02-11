using AutoMapper;
using practise.DTO;
using practise.Entitys;

namespace practise.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book,BookDTO>();
            CreateMap<BookDTO, Book>();

        }
    
    }
}
