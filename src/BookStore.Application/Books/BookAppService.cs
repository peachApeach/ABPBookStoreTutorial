//10. bookAppService는 Book Entity의 기본 저장소인 IRepository<Book, Guid>를 주입
//BookAppService는 IObjectMpper 서비스를 사용하여 Book 개체를 BookDto 개체에 매핑하고
//CreateUpdateBookDto개체를 Book 개체에 매핑한다.
//시작 템플릿은 AutoMapper 라이브러리를 개체 매핑 공급자로 사용


using BookStore.Permissions;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Books
{
    public class BookAppService : CrudAppService<
                                                    Book,
                                                    BookDto,
                                                    Guid,
                                                    PagedAndSortedResultRequestDto,
                                                    CreateUpdateBookDto>,
                                    IBookAppService
    {

        public BookAppService(IRepository<Book, Guid> repository) : base(repository)
        {
            //apb crud
            //안전한 http api 
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Edit;
            DeletePolicyName = BookStorePermissions.Books.Delete;
        }
    }
}
