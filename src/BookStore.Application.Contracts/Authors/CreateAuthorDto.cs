using System;
using System.ComponentModel.DataAnotations;

namespace BookStore.Authors
{
    public class createAuthorDto
    {
        [Required]
        [StringLength(AuthorConsts.MaxNameLenght)]
        public string Name { get; set; }

        [Required]
        public DataTime BirthDate { get; set; }

        public string ShortBio { get; set; }
    }
}