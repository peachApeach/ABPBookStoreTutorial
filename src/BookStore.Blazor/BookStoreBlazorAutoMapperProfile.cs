using BookStore.Books;
using AutoMapper;
using BookStore.Authors;

namespace BookStore.Blazor
{
    public class BookStoreBlazorAutoMapperProfile : Profile
    {
        public BookStoreBlazorAutoMapperProfile()
        {
            //automapper.profile을 상속함으로써 사용가능
            CreateMap<BookDto, CreateUpdateBookDto>();
            CreateMap<AuthorDto, UpdateAuthorDto>();
            
        }


    }
}
