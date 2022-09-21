using BookStore.Books;
using AutoMapper;

namespace BookStore.Blazor
{
    public class BookStoreBlazorAutoMapperProfile : Profile
    {
        public BookStoreBlazorAutoMapperProfile()
        {
            CreateMap<BookDto, CreateUpdateBookDto>();
            //automapper.profile을 상속함으로써 사용가능
        }


    }
}
