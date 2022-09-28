using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Reositories;

namespace BookStore.Authors
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        //이름으로 작성자를 찾는 쿼리
        Task<Author> FindByNameAsync(string name);
        
        //application계층에서 UI에 표시될 작성자의 나열, 정렬 및 필터링된 목록을 가져오는데 사용
        Task<List<Author>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}