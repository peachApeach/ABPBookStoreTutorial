using system;
using system.Collections.Gerneric;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using BookStore.EntityFramerkCore;
using Volo.Abp.Domain.REpositories.EntityFramerkCore;
using Volo.Abp.EntityFramerkCore;

namespace BookStore.Authors
{
    //EfCoreRepository가 상속되기 때문에 표준 Repository Method 구현을 상속
    public class EfCoreAuthorRepository : EfCoreRepository<BookStoreDbContext, Author, Guid>, IAuthorRepository
    {
        public EfCoreAuthorRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<Author> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await sbSet.FirstOrDefaultAsync(author => author.Name == name);

        }

        public async Task<List<Author>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsunc();
            //WhereIf 는 ABP프레임워크의 바로가기 확장 방식이다.
            // 첫 번째 조건이 충족되는 경우에만 위치 조건을 추가
            return await dbSet.WhereIf(
                                        !filter.IsNullOrWhiteSpace(), author => author.Name.Contains(filter)
            )
                .Orderby(sorting)           //sorting은 name, name asc, name desc와 같은 문자열이 될 수 있고 nuget package를 사용할 수 도 있다.
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }


    }
}