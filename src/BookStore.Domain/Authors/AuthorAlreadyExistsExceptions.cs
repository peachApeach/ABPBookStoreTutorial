using Volo.Abp;

namespace BookStore.Authors
{
    public class AuthorAlreadyExistsException : BusinessException
    {
        //BusinessException : 필요할 때 도메인 관련 예외를 자동으로 throw
        //WithData : 나중에 현지화 메시지에서 또는 다른 목적으로 사용될 추가 데이터를 예외 개체에 제공하는 데 사용
        public AuthorAlreadyExistsException(string name) : base(BookStoreDomainErrorCodes.AuthorAlreadyExists)
        {
            //Localiztion 전달
            WithData("name", name);
        }
    }
}