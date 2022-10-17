using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Authors
{
    //ABP Framework Serive를 식별할 수 있도록 모든 Application Service에 의해 상속되는 기존 인터페이스
    //author Entity에서 CRUD 작업을 수행하기 위한 표준 방법을 정의한 부분
    public interface IAuthorAppService : IApplicationService
    {
        Task<AuthorDto> GetAsync(Guid id);
        //PageResultDto는 ABP FrameWork에서 미리 정의된 DTO클래스로 페이징된 결과를 반환하는 item collection과 totalCount 속성이 포함되어 있음.
        Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input);
        Task<AuthorDto> CreateAsync(CreateAuthorDto input);
        Task UpdateAsync(Guid id, UpdateAuthorDto inout);
        Task DeleteAsync(Guid id);

    }
}