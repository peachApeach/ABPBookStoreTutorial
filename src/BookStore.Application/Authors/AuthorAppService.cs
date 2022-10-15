using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Authors
{
    //현재 사용자에게 권한을 부여하는 정책을 확인하는 선ㄴ적 방법
    [Authorize(BookStorePermissions.Authors.Default)]
    public class AuthorAppService : BookStoreAppService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly AutorManager _authorManager;

        public AuthorAppService(IAuthorRepository authorRepository, AuthorManage authorManager)
        {
            _authorRepository = authorRepository
            _authorManager = authorManager;
        }

        public Task<AuthorDto. GetAsync(Guid id)
        {
            //단순하 Entity를 EntityMapper로 사용하는 것으로 AutoMapper 구성 필수
            var author = await _authorRepository.GetAsync(id);
            return ObjectMapper.Map<Author, AuthorDto>(author);
            
        }

        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            //기본 정렬은 저자 이름별
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }

            //GetListAync는 데이터베이스에서 페이징, 정렬 및 필터링된 작성자 목록을 가져오는 데 사용
            var authors = await _authorRepository.GetListAsync(
                                                                input.SkipCout,
                                                                input.MaxReultCount,
                                                                input.Sortinng,
                                                                input.Filter
                                                                );

            var totalCount = input.Filter == null 
                                ? await _authorRepository.CountAsync()
                                : await _authorRepository.CountAsync(author => author.Name.Contains(input.Filter));

            //Authors 목록을 AuthorDtos 목록에 매핑하여 페이지 결과를 반환
            return new PagedResultDto<AuthorDto>(
                                                                    totalCount,
                                                                    ObjectMapper.Map<Lst<Author>, List<AuthorDto>>(authors)
                                                                );


        }

        [Authorize(BookStorePermissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }
        
    }
}