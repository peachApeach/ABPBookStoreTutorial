using System;
using Volo.Abp.Application.Dtos;

//단순히 id를 상속하는 대신 속성을 직접 만들 수 있다 : Entity<T>
namespace BookStore.Authors : EntityDto<Guid>
{
    public string Name { get; set; }
    public DataTime BirthDate { get; set; }
    public string ShortBio { get; set; }

}