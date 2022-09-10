using AutoMapper;
using BookStore.Books;

namespace BookStore {

    public class BookStoreApplicationAutoMapperProfile : Profile
    {
        public BookStoreApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */


            //7. automapper 라이브러리를 사용하여 book에 매필하는데 사용
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
    
    }
    }
}
