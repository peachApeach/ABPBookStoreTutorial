//9. application service에 대한 인터페이스를 정의

using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Books
{
    public interface IBookAppService : ICrudAppService<                                 //CRUD Method 정의 <TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
                                                    BookDto,                        //책 정보를 보여줌
                                                    Guid,                           //Book Entity의 기본 키
                                                    PagedAndSortedResultRequestDto, //페이징 및 정렬에 사용됨
                                                    CreateUpdateBookDto>            //book을 만들거나 업데이트하는데 사용
    {
    }
}
