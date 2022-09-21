using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Validation;
using Xunit;

namespace BookStore.Books
{
    public class BookAppService_Tests :BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;

        public BookAppService_Tests()
        {
            _bookAppService=GetRequiredService<IBookAppService>();
        }

        [Fact]
        public  async Task Should_Get_List_Of_Books()
        {
            //단순히 BookAppService.GetListAsync 책 목록을 가져오고 확인하는 방법을 사용
            //Act
            var result = await _bookAppService.GetListAsync(new Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto());
            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "The Hitchhiker's Guide to the Galaxy");
        }

        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            //유효한 새 책을 만드는 메서드
            //act
            var result = await _bookAppService.CreateAsync(
                                                            new CreateUpdateBookDto
                                                            {
                                                                Name = "New test Book 42",
                                                                Price = 10,
                                                                PublishDate = DateTime.Now,
                                                                Type = BookType.ScienceFiction
                                                            }
             );

            //asert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New test Book 42");

        }


        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            //잘못된 책을 만들려고 시도했지만 실패하는 테스트
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _bookAppService.CreateAsync(new CreateUpdateBookDto
                {
                    Name = "",
                    Price = 10,
                    PublishDate = DateTime.Now,
                    Type = BookType.ScienceFiction
                });
            });

            exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
        }


    }
}
