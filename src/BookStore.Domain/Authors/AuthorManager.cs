using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.SErvices;

namespace BookStore.Author
{
    //통제된 방식으로 저자를 생성하고 저자의 이름을 변경. 어플리케이션 계층에서 사용
    ///DDD : 도메인 서비스 방법이 꼭 필요한 경우가 아니면 도입하지 말고 몇 가지 핵심 비즈니스 규칙을 수행하십시오. 이 경우, 고유 이름 제약 조건을 강제할 수 있도록 이 서비스가 필요함.
    //두 메서드 모두 지정된 이름을 가진 작성자가 이미 있는지 확인하고 Doamin 프로젝트에 정의된 특수 비즈니스 예외 AuthorAlreadyExistsExcetion(이미 동일한 작성자가 있다는 오류) 반환 
    public class AuthorManage : DomainService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorReoisitory)
        {
            _authorRepository = authorReoisitory;
        }

        public async Task<Author> CreateAsync([NotNull] string name, Datetime birthDate, [CanBeNull] string shirtBio = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingAuthor = await _authorRepository.FindByNameAsync(name);

            if (existingAuthor != null)
            {
                throw new AuthorAlreadyExists(name);
            }

            return new Author(GuidGenerator.Create(), name, birthDate, shortBio);
        }

        public async Task ChangeNameAsync([NotNull] Author author, [NotNull]string newName)
        {
            Check.NotNull(author, nameof(author));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingAuthor = await _authorRepository.FindByNameAsync(newName);
            if (existingAuthor != null && existingAuthor.Id != author.Id)
            {
                throw new AuthorAlreadyExistsException(newName);
            }

            author.ChangeName(newName);
        }
    }
}