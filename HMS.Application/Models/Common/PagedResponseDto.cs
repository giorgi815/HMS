
namespace HMS.Application.Models.Common
{
    public class PagedResponseDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    
}
