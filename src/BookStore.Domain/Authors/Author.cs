using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Enitities.Auditing;

namespace Acme.BookStore.Books
{
    //FullAuditedAggregateRoot<Guid> : 엔터티를 삭제하면 데이터베이스에서 삭제되지 않고 모든 감사 속성과 함께 삭제된 것으로 표시됨.
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        //Private Set : 이 클래스에서 이 속성을 설정하도록 제한
        //Name은 신규 Author를 생성할 때, ChangeName을 수행할 때 설정할 수 있음
        public string Name {get; private set;}
        public DateTime BirthDate{get;set;}
        public string ShortBio {get;set;}

        private Author(){
            //this constructor is for deserialization /ORM purpose

        }

        internal Author( Guid id, [NotNull]string name, DateTime birthDate, [CanBeNull]string shortBio = null) : base(id)
        {
            SetName(name);
            BirthDate = birthDate;
            ShortBio = shortBio;
        }

        internal Author ChangeName([NotNull]string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            //Check Class는 메서드 인수를 확인하는 동안 도움이 되는 ABP Framework 유틸리티 클래스 
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLenght: AuthorConsts.MaxNameLength);
        }

    }
}