//1. book entity 추가

using System;
using Volo.Abp.Domain.Entities.Auditing;            //직접 쿼리하고 작업하는 root entity

namespace BookStore.Books
{
    public class Book: AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }    
    }
}
