//6. 기본 DTO 정의
//DTO는 프레젠테이션 계층과 application 계층 간의 데이터를 전송하는데 사용
//Book을 프레젠테이션 레이어로 반환하는 동안 개체를 'Book'에 맵핑하는데 사용

using System;
using Volo.Abp.Application.Dtos;

namespace BookStore.Books
{
    public class BookDto:AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }

    }
}
