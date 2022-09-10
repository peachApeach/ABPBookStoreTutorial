//8. createUpdateDto 정의

using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Books
{
    public class CreateUpdateBookDto
    {
        [Required]              //속성 유효성 검사를 정의하기 위해 사용 (필수요소)
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }= DateTime.Now;

        [Required]
        public float Price { get; set; }

    }
}
