using Volo.Abp.Application.Dtos;

namespace BookStore.Authors
{
    //Author를 검색하는데 사용
    public class GetAuthorListDto : PagedAndSortedResultRequestDto
    {
        //PagedAndSortedResultRequestDto에는 표준 페이징 및 정렬 속성이 포함되어 있음
        //int MaxReultCount, int SkipCount, String Sorting
        public string Filter { get; set; }
    }
}