sing System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Authors
{
    public class UpdateAuthorDto
    {
        [Reqired]
        [StringLength(AuthorConsts.MaxNameLenght)]
        public string Name { get; set; }

        [required]
        publc DataTime BirthDate { get;set; }

        public string ShortBio { get; set; }
    }
}